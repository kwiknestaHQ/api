using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNesta.Shared.Responses;
using Microsoft.AspNetCore.Http;

namespace KwikNestaProperty.Domain.Entities
{
    public class TenancyAgreement : BaseEntity
    {
        public Guid RentalPaymentId { get; private set; }
        public Guid RentalIntentId { get; private set; }
        public string TenantId { get; private set; } = null!;
        public string LandlordId { get; private set; } = null!;

        public string PropertyTitle { get; private set; } = null!;
        public string PropertyAddress { get; private set; } = null!;
        public decimal MonthlyRent { get; private set; }
        public decimal CautionDeposit { get; private set; }
        public decimal PlatformFee { get; private set; }
        public DateTime MoveInDate { get; private set; }
        public int LeaseDurationMonths { get; private set; }
        public DateTime LeaseEndDate { get; private set; }

        public string? UnsignedDocumentUrl { get; private set; }
        public string? SignedDocumentUrl { get; private set; }

        public bool TenantSigned { get; private set; }
        public bool LandlordSigned { get; private set; }
        public DateTime? TenantSignedAt { get; private set; }
        public DateTime? LandlordSignedAt { get; private set; }
        public string? TenantSignatureRef { get; private set; }    // OTP reference
        public string? LandlordSignatureRef { get; private set; }

        public DateTime LandlordSignDeadline { get; private set; }

        public ETenancyAgreementStatus Status { get; private set; }

        public RentalPayment Payment { get; private set; } = null!;
        public RentalIntent Intent { get; private set; } = null!;

        private TenancyAgreement() { }

        public static TenancyAgreement Create(
            RentalPayment payment,
            RentalIntent intent,
            string propertyTitle,
            string propertyAddress)
        {
            return new TenancyAgreement
            {
                Id = Guid.NewGuid(),
                RentalPaymentId = payment.Id,
                RentalIntentId = intent.Id,
                TenantId = intent.TenantId,
                LandlordId = payment.LandlordId,
                PropertyTitle = propertyTitle,
                PropertyAddress = propertyAddress,
                MonthlyRent = intent.MonthlyRent,
                CautionDeposit = intent.CautionDeposit,
                PlatformFee = intent.PlatformFee,
                MoveInDate = intent.ProposedMoveInDate,
                LeaseDurationMonths = intent.LeaseDurationMonths,
                LeaseEndDate = intent.ProposedMoveInDate
                    .AddMonths(intent.LeaseDurationMonths),
                LandlordSignDeadline = DateTime.UtcNow.AddHours(48),
                Status = ETenancyAgreementStatus.PendingDocumentGeneration
            };
        }

        public Response<string> AttachUnsignedDocument(string documentUrl)
        {
            if (string.IsNullOrWhiteSpace(documentUrl))
            {
                return Response<string>.Fail("Document URL is required.",
                    StatusCodes.Status400BadRequest);
            }

            UnsignedDocumentUrl = documentUrl;
            Status = ETenancyAgreementStatus.AwaitingTenantSignature;
            return Response<string>.Ok("Attached.");
        }

        public Response<string> SignAsTenant(string otpReference)
        {
            if (Status != ETenancyAgreementStatus.AwaitingTenantSignature)
            {
                return Response<string>.Fail("Agreement is not awaiting tenant signature.",
                    StatusCodes.Status403Forbidden);
            }

            TenantSigned = true;
            TenantSignedAt = DateTime.UtcNow;
            TenantSignatureRef = otpReference;
            Status = ETenancyAgreementStatus.AwaitingLandlordSignature;
            return Response<string>.Ok("Signed.");
        }

        public Response<string> SignAsLandlord(string otpReference)
        {
            if (Status != ETenancyAgreementStatus.AwaitingLandlordSignature)
            {
                return Response<string>.Fail("Agreement is not awaiting landlord signature.",
                    StatusCodes.Status403Forbidden);
            }

            if (DateTime.UtcNow > LandlordSignDeadline)
            {
                return Response<string>.Fail("Signing deadline has passed.",
                    StatusCodes.Status403Forbidden);
            }

            LandlordSigned = true;
            LandlordSignedAt = DateTime.UtcNow;
            LandlordSignatureRef = otpReference;
            Status = ETenancyAgreementStatus.AwaitingSignedDocument;
            return Response<string>.Ok("Signed.");
        }

        public Response<string> AttachSignedDocument(string signedDocumentUrl)
        {
            if (string.IsNullOrWhiteSpace(signedDocumentUrl))
            {
                return Response<string>.Fail("Signed document URL is required.",
                    StatusCodes.Status400BadRequest);
            }

            SignedDocumentUrl = signedDocumentUrl;
            Status = ETenancyAgreementStatus.FullySigned;
            return Response<string>.Ok("Signed.");
        }

        public Response<string> Expire()
        {
            if (Status != ETenancyAgreementStatus.AwaitingLandlordSignature)
            {
                return Response<string>.Fail("Only agreements awaiting landlord signature can expire.",
                    StatusCodes.Status403Forbidden);
            }

            Status = ETenancyAgreementStatus.Expired;
            return Response<string>.Ok("Attached.");
        }
    }
}