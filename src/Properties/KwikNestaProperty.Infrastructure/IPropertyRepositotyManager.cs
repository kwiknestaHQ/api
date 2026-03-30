using KwikNestaProperty.Infrastructure.Contracts;

namespace KwikNestaProperty.Infrastructure
{
    public interface IPropertyRepositotyManager
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

        Task BeginTransaction(Func<Task> action);
        Task SaveAsync();
    }
}