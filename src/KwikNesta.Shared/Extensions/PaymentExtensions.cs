using KwikNesta.Shared.Models.Enumerations.Payments;
using KwikNesta.Shared.ServiceDTOs.Payment;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace KwikNesta.Shared.Extensions
{
    public static class PaymentExtensions
    {
        /// <summary>
        /// converts amount to minor digits
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static decimal ToMinorUnits(this decimal amount)
            => amount * 100;

        /// <summary>
        /// Generates random numbers
        /// </summary>
        /// <param name="digits">Number of digits the generated number should have</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static int GenerateRandomDigits(int digits)
        {
            if (digits <= 0)
                throw new ArgumentException("Digits must be greater than zero.");

            int min = (int)Math.Pow(10, digits - 1);
            int max = (int)Math.Pow(10, digits) - 1;

            byte[] bytes = new byte[4];
            RandomNumberGenerator.Fill(bytes);
            int randomInt = Math.Abs(BitConverter.ToInt32(bytes, 0));

            int value = (randomInt % (max - min + 1)) + min;

            return value;
        }

        /// <summary>
        /// Generates 26-digits reference code
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <returns></returns>
        public static string GenerateReference(EPaymentCode serviceCode)
        {
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string service = ((int)serviceCode).ToString("D4");

            byte[] bytes = new byte[4];
            RandomNumberGenerator.Fill(bytes);

            uint randomUInt = BitConverter.ToUInt32(bytes, 0);
            int value = (int)(randomUInt % 1_000_000);
            string randomPart = value.ToString("D6");

            string body = $"{timestamp}{service}{randomPart}";
            string checkDigits = CalculateCheckDigits(body);

            return body + checkDigits;
        }

        /// <summary>
        /// Checks if a reference code is valid
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool TryParse(string reference, out PaymentReferenceInfo? info)
        {
            info = null;
            if (string.IsNullOrWhiteSpace(reference) || reference.Length != 28)
                return false;

            if (!IsValidReference(reference))
                return false;

            string timestampPart = reference.Substring(0, 14);
            string servicePart = reference.Substring(14, 4);
            string randomPart = reference.Substring(18, 6);
            string checkPart = reference.Substring(24, 2);

            if (!DateTime.TryParseExact(
                    timestampPart,
                    "yyyyMMddHHmmss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                    out var timestampUtc))
                return false;

            if (!int.TryParse(servicePart, out int serviceCode))
                return false;

            info = new PaymentReferenceInfo
            {
                TimestampUtc = timestampUtc,
                ServiceCode = serviceCode,
                RandomPart = randomPart,
                CheckDigits = checkPart
            };

            return true;
        }

        /// <summary>
        /// Checks if a reference code is valid
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static bool IsValidReference(string reference)
        {
            string body = reference[..^2];
            string checkDigits = reference[^2..];

            return CalculateCheckDigits(body) == checkDigits;
        }

        public static bool IsValidPaystckSignature(string requestBody, string paystackSignature, string secret)
        {
            if(string.IsNullOrEmpty(requestBody) || 
                string.IsNullOrEmpty(paystackSignature) || 
                string.IsNullOrEmpty(secret))
            {
                return false;
            }

            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secret));
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestBody));

            var computedSignature = BitConverter
                .ToString(computedHash)
                .Replace("-", "")
                .ToLower();

            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(computedSignature),
                Encoding.UTF8.GetBytes(paystackSignature));
        }

        static string CalculateCheckDigits(string number)
        {
            int remainder = 0;
            foreach (char c in number)
            {
                remainder = (remainder * 10 + (c - '0')) % 97;
            }

            int check = 98 - remainder;
            return check.ToString("D2");
        }
    }
}