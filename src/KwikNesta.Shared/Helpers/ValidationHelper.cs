using KwikNesta.Shared.Models;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Models.Enumerations.Property;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

namespace KwikNesta.Shared.Helpers
{
    public class ValidationHelper
    {
        private static readonly Regex E164Regex =
            new(@"^\+[1-9]\d{1,14}$", RegexOptions.Compiled);

        private static readonly Regex E164NoPlusRegex =
            new(@"^[1-9]\d{7,14}$", RegexOptions.Compiled);

        private static readonly Regex EmailRegex = 
            new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled);

        public static bool ValidForListingType(EListingType listingType, EPriceFrequency priceFrequency)
        {
            return ListingRules.AllowedFrequencies[listingType]
                .Contains(priceFrequency);
        }

        public static bool ValidUserContext(UserContext context)
        {
            return context != null &&
                !string.IsNullOrWhiteSpace(context.Email) && 
                EmailRegex.IsMatch(context.Email) &&
                !string.IsNullOrWhiteSpace(context.Id);
        }

        public static bool IsValidCoordinates(double latitude, double longitude)
        {
            if (double.IsNaN(latitude) || double.IsInfinity(latitude)) return false;
            if (double.IsNaN(longitude) || double.IsInfinity(longitude)) return false;

            return latitude >= -90 && latitude <= 90
                && longitude >= -180 && longitude <= 180;
        }

        public static bool IsPasswordMatch(string password, string comparePassword)
        {
            return password.Equals(comparePassword);
        }

        public static bool ValidUserId(string userId)
        {
            return !string.IsNullOrWhiteSpace(userId) && userId.Length == 32;
        }

        public static bool IsValidE164(string phone)
        {
            return IsValid(phone);
        }

        public static bool ValidSuspensionReason(SuspensionReasons reason, string? otherReason)
        {
            if (reason != SuspensionReasons.Other)
            {
                return true;
            }

            return !string.IsNullOrWhiteSpace(otherReason);
        }

        private static bool IsValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            phoneNumber = NormalizeNumber(phoneNumber);
            return E164Regex.IsMatch(phoneNumber);
        }

        public static string NormalizeNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return phone;

            return phone
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");
        }
    }
}