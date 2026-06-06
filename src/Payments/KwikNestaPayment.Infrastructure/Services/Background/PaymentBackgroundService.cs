using Hangfire.Console;
using Hangfire.Server;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNesta.Shared.ServiceCommands.Property;
using KwikNesta.Shared.ServiceQueries.Payment;
using KwikNesta.Shared.ServiceQueries.Property;
using KwikNestaPayment.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KwikNestaPayment.Infrastructure.Services.Background
{
    public class PaymentBackgroundService(IPaymentRepositoryManager repository,
                            IKNMediator mediator,
                            PaymentRouter router) : IPaymentBackgroundService
    {
        private readonly IPaymentRepositoryManager _repository = repository;
        private readonly IKNMediator _mediator = mediator;
        private readonly PaymentRouter _router = router;

        public async Task RunPaymentVerificationsAsync(PerformContext context, 
            CancellationToken cancellationToken)
        {
            context.WriteLine("===[RunPaymentVerifications] Running job===");

            var payments = await _repository.Payment
                .Get(p => !p.IsDeprecated &&
                    !p.PaidAt.HasValue &&
                    (p.Status == EPaymentStatus.Pending || p.Status == EPaymentStatus.Failed))
                .OrderBy(p => p.CreatedOn)
                .Take(20)
                .ToListAsync(cancellationToken);

            foreach (var payment in payments.WithProgress(context))
            {
                var verify = await _mediator.SendAsync(new GetPaystackVerificationQuery
                {
                    Reference = payment.Reference
                }, cancellationToken);

                if (!verify.Success)
                {
                    context.WriteLine($"===[RunPaymentVerifications] Payment verification failed for: {payment.Reference}===");
                    return;
                }

                var verification = verify.Data;
                if (!verification.Status || verification.Data.Status != "success")
                {
                    context.WriteLine($"===[RunPaymentVerifications] Payment verification failed for: {payment.Reference}===");
                    return;
                }

                if (verification.Data.Amount != payment.NetAmount.ToMinorUnits())
                {
                    context.WriteLine($"===[RunPaymentVerifications] Amount mismatch for: {payment.Reference}===");
                    return;
                }

                if (verification.Data.Currency != payment.Currency)
                {
                    context.WriteLine($"===[RunPaymentVerifications] Currency mismatch for: {payment.Reference}===");
                    return;
                }

                payment.Status = EPaymentStatus.Successful;
                payment.PaidAt = DateTime.UtcNow;
                await _repository.SaveAsync();

                await _router.Route(payment);
            }
        }

        public async Task ProcessSettlementsAsync(PerformContext context, CancellationToken cancellationToken)
        {
            context.WriteLine($"[ProcessSettlementsAsync] Starting processing at {DateTime.UtcNow}");

            var settlementsDue = await _repository.Settlement
                .Get(s => !s.IsDeprecated && 
                        s.Status == ESettlementStatus.Pending && 
                        !s.SettledAt.HasValue && 
                        s.StartAfter >= DateTime.UtcNow)
                .Take(100)
                .ToListAsync(cancellationToken);

            foreach( var settlement in settlementsDue.WithProgress(context))
            {
                context.WriteLine($"[ProcessSettlementsAsync] Processing {settlement.Type} in favour of {settlement.BeneficiaryId} for {settlement.Purpose}");
                if (settlement.Type == EPayoutType.Refund)
                {
                    switch (settlement.Purpose)
                    {
                        case EPaymentPurpose.Viewing:
                            await ProcessViewRefundSettlement(settlement.Id, context, cancellationToken);
                            break;
                        case EPaymentPurpose.Rent:
                            break;
                    }
                }
                else
                {
                    switch (settlement.Purpose)
                    {
                        case EPaymentPurpose.Viewing:
                            await ProcessViewTransferSettlement(settlement.Id, context, cancellationToken);
                            break;
                        case EPaymentPurpose.Rent:
                            break;
                    }
                } 
            }
        }

        #region Private Methods
        private async Task ProcessViewTransferSettlement(Guid settlementId, 
                                        PerformContext context, 
                                        CancellationToken cancellationToken)
        {
            context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewTransferSettlement] Processing Viewing Refund for {settlementId}");
            var settlement = await _repository.Settlement
                .FirstOrDefault(s => s.Id == settlementId, true);
            if (settlement == null)
            {
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewTransferSettlement] Settlement is null for Id: {settlementId}");
                return;
            }

            var viewRequestResponse = await _mediator.SendAsync(new GetViewRequestByIdQuery
            {
                Id = settlement.ReferenceId,
            }, cancellationToken);

            if (!viewRequestResponse.Success)
            {
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewTransferSettlement] Getting view request errored out: {viewRequestResponse.Message}");
                return;
            }

            var viewRequest = viewRequestResponse.Data;
            if (viewRequest.Status != EViewingStatus.Completed || viewRequest.Status != EViewingStatus.NoShow)
            {
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewTransferSettlement] Unprocessable view request status: {viewRequest.Status}");
                return;
            }

            var bankAccountResponse = await _mediator.SendAsync(new GetUserBankAccountQuery
            {
                UserId = settlement.BeneficiaryId
            }, cancellationToken);

            if (!bankAccountResponse.Success)
            {
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] Getting payout account errored out: {bankAccountResponse.Message}");
                return;
            }

            var bankAccount = bankAccountResponse.Data;
            if (string.IsNullOrWhiteSpace(bankAccount.RecipientCode))
            {
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] Recipient Code can not be null or empty");
                return;
            }

            settlement.MarkAsSettled();
            await _repository.SaveAsync();

            var transferResponse = await _mediator.SendAsync(new InitiateTransferCommand
            {
                Amount = settlement.Amount,
                RecipientCode = bankAccount.RecipientCode,
                Narration = $"Kwik Nesta Landlord View Request Fee for ({viewRequest.Property?.Title})"[..255]
            }, cancellationToken);

            if (!transferResponse.Success)
            {
                settlement.MarkAsFailed();
                await _repository.SaveAsync();
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] Transfer errored out: {transferResponse.Message}");
                return;
            }

            var viewRequestUpdateResult = await _mediator.SendAsync(new UpdateViewRequestAndSessionOnSettlementCommand
            {
                ViewRequestId = viewRequest.Id
            }, cancellationToken);

            context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] View Request & Session Update: {viewRequestUpdateResult.Message}");
            return;
        }

        private async Task ProcessViewRefundSettlement(Guid settlementId, 
                                        PerformContext context, 
                                        CancellationToken cancellationToken)
        {
            context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] Processing Viewing Refund for {settlementId}");
            var settlement = await _repository.Settlement
                .FirstOrDefault(s => s.Id == settlementId, true);
            if (settlement == null)
            {
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] Settlement is null for Id: {settlementId}");
                return;
            }

            var viewRequestResponse = await _mediator.SendAsync(new GetViewRequestByIdQuery
            {
                Id = settlement.ReferenceId,
            }, cancellationToken);

            if (!viewRequestResponse.Success)
            {
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] Getting view request errored out: {viewRequestResponse.Message}");
                return;
            }

            var viewRequest = viewRequestResponse.Data;
            if (viewRequest.Status != EViewingStatus.Completed || viewRequest.Status != EViewingStatus.NoShow)
            {
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] Unprocessable view request status: {viewRequest.Status}");
                return;
            }

            var paymentResponse = await _mediator.SendAsync(new GetPaymentByReferenceIdQuery
            {
                ReferenceId = viewRequest.Id
            }, cancellationToken);

            if (!paymentResponse.Success)
            {
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] Getting payment errored out: {paymentResponse.Message}");
                return;
            }

            var payment = paymentResponse.Data;
            if(!payment.PaidAt.HasValue || payment.Status != EPaymentStatus.Successful)
            {
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] Cannot refune an unsucessful payment {payment.Reference}");
                return;
            }

            settlement.MarkAsSettled();
            await _repository.SaveAsync();

            var refundResponse = await _mediator.SendAsync(new InitiatePaystackRefundCommand
            {
                Amount = settlement.Amount,
                PaymentReference = payment.Reference,
                UserId = settlement.BeneficiaryId
            }, cancellationToken);

            if (!refundResponse.Success)
            {
                settlement.MarkAsFailed();
                await _repository.SaveAsync();
                context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] Refund errored out: {refundResponse.Message}");
                return;
            }

            var viewRequestUpdateResult = await _mediator.SendAsync(new UpdateViewRequestAndSessionOnSettlementCommand
            {
                ViewRequestId = viewRequest.Id
            }, cancellationToken);

            context.WriteLine($"[ProcessSettlementsAsync] -> [ProcessViewRefundSettlement] View Request & Session Update: {viewRequestUpdateResult.Message}");
            return;
        }
        #endregion
    }
}