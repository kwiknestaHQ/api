using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.ServiceCommands.Identity;
using KwikNesta.Shared.ServiceDTOs.Identity;
using KwikNestaIdentity.Application.Validations;
using KwikNestaIdentity.Domain.Entities;

namespace KwikNestaIdentity.Application
{
    internal class ObjectFactory
    {
        public static OtpEntry InitializeOtp(string userId,
                                string otpHash,
                                EOtpType otpType,
                                string? tokenHash = null,
                                int expirationMinutes = 10)
        {
            return new OtpEntry
            {
                UserId = userId,
                OtpHash = otpHash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Type = otpType,
                TokenHash = tokenHash
            };
        }

        public static User InitializeUser(RegistrationCommand command)
        {
            return new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                OtherName = command.MiddleName,
                Email = command.Email,
                PhoneNumber = ValidationHelper.NormalizeNumber(command.PhoneNumber),
                UserName = command.Email,
                Gender = command.Gender
            };
        }

        public static CurrentUserDto Map(User user)
        {
            return new CurrentUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.OtherName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                Gender = user.Gender,
                Status = user.Status
            };
        }
    }
}