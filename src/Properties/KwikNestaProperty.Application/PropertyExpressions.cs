using KwikNesta.Shared.Models.Enumerations.Property;
using KwikNestaProperty.Domain.Entities;
using System.Linq.Expressions;

namespace KwikNestaProperty.Application
{
    public static class PropertyExpressions
    {
        public static Expression<Func<KNProperty, bool>> IsVerified()
        {
            return p =>
                p.Status == EListingStatus.Available && 
                !p.IsDeprecated &&
                p.Location.VerificationStatus == ELocationVerificationStatus.Verified &&
                p.OwnershipVerificationRequests
                    .OrderByDescending(o => o.CreatedOn)
                    .Select(o => o.Status)
                    .FirstOrDefault() == EVerificationStatus.Approved;
        }
    }
}