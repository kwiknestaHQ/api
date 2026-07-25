using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using Microsoft.AspNetCore.Http;

namespace KwikNestaProperty.Domain.Entities
{
    public class RentalPayment : BaseEntity
    {
        public Guid RentalIntentId { get; private set; }
        public string TenantId { get; private set; } = null!;
        public Guid PropertyId { get; private set; }
        public string LandlordId { get; private set; } = null!;

        // Amounts: copied from intent, never recalculated
        public decimal MonthlyRent { get; private set; }
        public decimal CautionDeposit { get; private set; }
        public decimal PlatformFee { get; private set; }
        public decimal TotalAmount { get; private set; }

        public string PaymentReference { get; private set; } = null!;
        public string? PaymentTransactionId { get; private set; }

        public ERentalPaymentStatus Status { get; private set; }

        // Escrow
        public DateTime? PaidAt { get; private set; }
        public DateTime? EscrowStartedAt { get; private set; }
        public DateTime? EscrowReleasesAt { get; private set; }
        public DateTime? DisbursedAt { get; private set; }
        public DateTime? RefundedAt { get; private set; }

        public RentalIntent Intent { get; private set; } = null!;
        public KNProperty Property { get; set; } = null!;

        private RentalPayment() { }

        public static RentalPayment Create(RentalIntent intent, string landlordId)
        {
            return new RentalPayment
            {
                Id = Guid.NewGuid(),
                RentalIntentId = intent.Id,
                TenantId = intent.TenantId,
                PropertyId = intent.PropertyId,
                LandlordId = landlordId,
                MonthlyRent = intent.MonthlyRent,
                CautionDeposit = intent.CautionDeposit,
                PlatformFee = intent.PlatformFee,
                TotalAmount = intent.TotalAmountDue,
                PaymentReference = PaymentExtensions
                    .GenerateReference(EPaymentCode.Rent),
                Status = ERentalPaymentStatus.Pending
            };
        }

        public Response<string> ConfirmPayment(string paymentTransactionId)
        {
            if (Status != ERentalPaymentStatus.Pending)
            {
                return Response<string>.Fail("Payment is not in a confirmable state.",
                    StatusCodes.Status403Forbidden);
            }

            Status = ERentalPaymentStatus.Paid;
            PaymentTransactionId = paymentTransactionId;
            PaidAt = DateTime.UtcNow;
            EscrowStartedAt = DateTime.UtcNow;
            EscrowReleasesAt = DateTime.UtcNow.AddHours(72);

            return Response<string>.Ok("Confirmed");
        }

        public Response<string> StartEscrow()
        {
            if (Status != ERentalPaymentStatus.Paid)
            {
                return Response<string>.Fail("Payment must be confirmed before escrow can start.",
                    StatusCodes.Status403Forbidden);
            }

            Status = ERentalPaymentStatus.InEscrow;
            return Response<string>.Ok("Escrow Started");
        }

        public Response<string> Disburse()
        {
            if (Status != ERentalPaymentStatus.InEscrow)
            {
                return Response<string>.Fail("Only escrowed payments can be disbursed.",
                    StatusCodes.Status403Forbidden);
            }

            if (DateTime.UtcNow < EscrowReleasesAt)
            {
                return Response<string>.Fail("Escrow window has not elapsed yet.",
                    StatusCodes.Status403Forbidden);
            }

            Status = ERentalPaymentStatus.Disbursed;
            DisbursedAt = DateTime.UtcNow;
            return Response<string>.Ok("Escrow Started");
        }

        public Response<string> Refund()
        {
            if (Status != ERentalPaymentStatus.InEscrow)
            {
                return Response<string>.Fail("Only escrowed payments can be refunded.",
                    StatusCodes.Status403Forbidden);
            }

            Status = ERentalPaymentStatus.Refunded;
            RefundedAt = DateTime.UtcNow;
            return Response<string>.Ok("Escrow Started");
        }
    }
}