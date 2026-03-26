using KwikNestaProperty.Infrastructure.Contracts;
using KwikNestaProperty.Infrastructure.Data;
using KwikNestaProperty.Infrastructure.Repositories;

namespace KwikNestaProperty.Infrastructure
{
    public class PropertyRepositotyManager(PropertyServiceDbContext context) : IPropertyRepositotyManager
    {
        private readonly PropertyServiceDbContext _context = context;
        private readonly Lazy<IKNPropertyRepository> _propertyRepository =
            new(() => new KNPropertyRepository(context));
        private readonly Lazy<IOwnershipVerificationRepository> _ownershipVerificationRepository =
            new(() => new OwnershipVerificationRepository(context));
        private readonly Lazy<IPropertyFeatureRepository> _propertyFeatureRepository =
           new(() => new PropertyFeatureRepository(context));
        private readonly Lazy<IPropertyFeatureLinkRepository> _propertyFeatureLinkRepository =
           new(() => new PropertyFeatureLinkRepository(context));
        private readonly Lazy<IPropertyLocationRepository> _propertyLocationRepository =
           new(() => new PropertyLocationRepository(context));
        private readonly Lazy<IPropertyMediaRepository> _propertyMediaRepository =
           new(() => new PropertyMediaRepository(context));
        private readonly Lazy<IViewingRequestRepository> _viewngRequestRepository =
           new(() => new ViewingRequestRepository(context));

        public IKNPropertyRepository Property => _propertyRepository.Value;
        public IOwnershipVerificationRepository OwnershipVerification => _ownershipVerificationRepository.Value;
        public IPropertyFeatureLinkRepository PropertyFeatureLink => _propertyFeatureLinkRepository.Value;
        public IPropertyFeatureRepository PropertyFeature => _propertyFeatureRepository.Value;
        public IPropertyLocationRepository PropertyLocation => _propertyLocationRepository.Value;
        public IPropertyMediaRepository PropertyMedia => _propertyMediaRepository.Value;
        public IViewingRequestRepository ViewingRequest => _viewngRequestRepository.Value;

        public async Task BeginTransaction(Func<Task> action)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();

                await SaveAsync();
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}