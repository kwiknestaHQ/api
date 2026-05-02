using Amazon.Runtime.Internal;
using KwikNesta.Shared.ServiceCommands.Payment;
using KwikNesta.Shared.ServiceDTOs.Payment;
using KwikNestaPayment.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KwikNestaPayment.Application
{
    internal static class PaymentObjectFactory
    {
        public static KNPayment Map(this CreatePaymentCommand command)
        {
            return new KNPayment
            {
                Amount = command.Amount,
                NetAmount = command.NetAmount,
                PlatformFee = command.PlatformFee,
                Purpose = command.Purpose,
                Reference = command.Reference,
                ReferenceId = command.ReferenceId,
                UserId = command.Context.Id,
            };
        }

        public static PaymentDto Map(this KNPayment payment)
        {
            return new PaymentDto
            {
                Id = payment.Id,
                UserId = payment.UserId,
                Reference = payment.Reference,
                Amount = payment.Amount,
                NetAmount = payment.NetAmount,
                PaidAt = payment.PaidAt,
                CreatedOn = payment.CreatedOn,
                Currency = payment.Currency,
                PlatformFee = payment.PlatformFee,
                Purpose = payment.Purpose,
                ReferenceId = payment.ReferenceId,
                Status = payment.Status
            };
        }

        public static KNPayoutAccount Map(this CreatePaystackTransferResponseDataDetails details, 
                                        string userId, 
                                        string recipientCode)
        {
            return new KNPayoutAccount
            {
                AccountName = details.AccountName,
                AccountNumber = details.AccountNumber,
                BankCode = details.BankCode,
                BankName = details.BankName,
                RecipientCode = recipientCode,
                UserId = userId
            };
        }

        public static PayoutAccountDto Map(this KNPayoutAccount account)
        {
            return new PayoutAccountDto
            {
                Id = account.Id,
                CreatedOn = account.CreatedOn,
                LastUpdatedOn = account.LastUpdatedOn,
                AccountName = account.AccountName,
                AccountNumber = account.AccountNumber,
                BankCode = account.BankCode,
                BankName = account.BankName,
                RecipientCode = account.RecipientCode,
                UserId = account.UserId,
                IsActive = account.IsActive
            };
        }
    }
}