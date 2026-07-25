using KwikNesta.Shared.Contracts;
using KwikNestaProperty.Infrastructure.Contracts;

namespace KwikNestaProperty.Infrastructure
{
    public interface IPropertyRepositoryManager : IBaseRepositoryManager
    {
        IKNPropertyRepository Property { get; }
        IOwnershipVerificationRepository OwnershipVerification {  get; }
        IPropertyFeatureLinkRepository PropertyFeatureLink { get; }
        IPropertyFeatureRepository PropertyFeature {  get; }
        IPropertyLocationRepository PropertyLocation { get; }
        IPropertyMediaRepository PropertyMedia { get; }
        IViewingRequestRepository ViewingRequest { get; }
        IOwnershipDocumentRepository OwnershipDocument { get; }
        IPropertyPriceHistoryRepository PropertyPriceHistory { get; }
        IPropertyInquiryRepository PropertyInquiry { get; }
        IPropertyViewRepository PropertyView { get; }
        ISessionParticipantRepository SessionParticipant { get; }
        IViewingSessionRepository ViewingSession { get; }
        IViewingCheckInRepository ViewingCheckIn {  get; }
        IRentalIntentRepository RentalIntent {  get; }
        IRentalPaymentRepository RentalPayment { get; }
        ITenancyAgreementRepository TenancyAgreement { get; }
    }
}