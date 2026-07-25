using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using Microsoft.AspNetCore.Http;

namespace KwikNestaProperty.Domain.Entities
{
    public class RentalIntent : BaseEntity
    {
        public Guid PropertyId { get; private set; }
        public string TenantId { get; private set; } = null!;

        public decimal MonthlyRent { get; private set; }
        public decimal CautionDeposit { get; private set; }
        public decimal PlatformFee { get; private set; }
        public decimal TotalAmountDue { get; private set; }

        public DateTime ProposedMoveInDate { get; private set; }
        public int LeaseDurationMonths { get; private set; }

        public ERentalIntentStatus Status { get; private set; } = ERentalIntentStatus.Pending;
        public string? DeclineReason { get; private set; }

        public DateTime? RespondedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        public KNProperty Property { get; private set; } = null!;

        public RentalIntent() { }

        public static RentalIntent Create(Guid propertyId,
                string tenantId,
                decimal rentAmount,
                DateTime proposedMoveInDate,
                int leaseDurationMonths,
                decimal platformFeePercentage,
                int expirationHours = 48)
        {
            var monthlyRent = rentAmount / leaseDurationMonths;
            var platformFee = Math.Round(rentAmount * platformFeePercentage, 2);
            var caution = monthlyRent;

            return new RentalIntent
            {
                PropertyId = propertyId,
                TenantId = tenantId,
                MonthlyRent = monthlyRent,
                CautionDeposit = caution,
                PlatformFee = platformFee,
                TotalAmountDue = monthlyRent + caution + platformFee,
                ProposedMoveInDate = proposedMoveInDate,
                LeaseDurationMonths = leaseDurationMonths,
                ExpiresAt = DateTime.UtcNow.AddHours(expirationHours)
            };
        }

        public Response<string> Accept()
        {
            if (Status != ERentalIntentStatus.Pending)
            {
                return Response<string>.Fail("Only pending intents can be accepted.",
                    StatusCodes.Status403Forbidden);
            }

            if (DateTime.UtcNow > ExpiresAt)
            {
                return Response<string>.Fail("This intent has expired.",
                    StatusCodes.Status403Forbidden);
            }

            Status = ERentalIntentStatus.Accepted;
            RespondedAt = DateTime.UtcNow;
            return Response<string>.Ok("Accepted");
        }

        public Response<string> Decline(string? reason = null)
        {
            if (Status != ERentalIntentStatus.Pending)
            {
                return Response<string>.Fail("Only pending intents can be declined.",
                    StatusCodes.Status403Forbidden);
            }

            Status = ERentalIntentStatus.Declined;
            DeclineReason = reason;
            RespondedAt = DateTime.UtcNow;
            return Response<string>.Ok("Declined.");
        }

        public void Expire()
        {
            if (Status == ERentalIntentStatus.Pending)
                Status = ERentalIntentStatus.Expired;
        }
    }
}