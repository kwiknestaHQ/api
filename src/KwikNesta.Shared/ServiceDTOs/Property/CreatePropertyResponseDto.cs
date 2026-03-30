using KwikNesta.Shared.Models.Enumerations.Property;

namespace KwikNesta.Shared.ServiceDTOs.Property
{
    public record CreatePropertyResponseDto(Guid Id, EListingStatus Status);
}