using KwikNesta.Shared.Models.Enumerations.Identity;
using System.Text.RegularExpressions;

namespace KwikNestaIdentity.Application.Validations
{
    internal class ValidationHelper
    {
        private static readonly Regex E164Regex =
            new(@"^\+[1-9]\d{1,14}$", RegexOptions.Compiled);

        private static readonly Regex E164NoPlusRegex =
            new(@"^[1-9]\d{7,14}$", RegexOptions.Compiled);

        internal static bool IsPasswordMatch(string password, string comparePassword)
        {
            return password.Equals(comparePassword);
        }

        internal static bool ValidUserId(string userId)
        {
            return !string.IsNullOrWhiteSpace(userId) && userId.Length == 32;
        }

        public static bool IsValidE164(string phone)
        {
            return IsValid(phone);
        }

        public static bool ValidSuspensionReason(SuspensionReasons reason, string? otherReason)
        {
            if(reason != SuspensionReasons.Other)
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

        private static bool IsValidWithoutPlus(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            phoneNumber = NormalizeNumber(phoneNumber);
            phoneNumber = DigitOnlyNumber(phoneNumber);
            return E164NoPlusRegex.IsMatch(phoneNumber);
        }

        private static string DigitOnlyNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return phone;
            var digitsOnly = Regex.Replace(phone, @"\D", "");
            return digitsOnly;
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