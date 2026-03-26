using KwikNesta.Shared.Models.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KwikNesta.Shared.Helpers
{
    public class TokenHelper
    {
        public static string CreateAccessToken(string userId, string email, string[] roles, JwtSettings settings)
        {
            var claims = GetClaims(userId, email, roles, settings.Issuer);
            var creds = GetSigningCredentials(settings.Key);
            var jwt = GetJwtSecurityToken(claims, creds, settings, DateTime.UtcNow, settings.Audience);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static string GenerateRandomBase64String(int size = 64)
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(size));
        }

        public static string GenerateOtp(int length = 6)
        {
            if (length <= 0) throw new ArgumentException("OTP length must be positive.");

            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            int randomNumber = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF;

            int otpValue = randomNumber % (int)Math.Pow(10, length);

            return otpValue.ToString(new string('0', length));
        }

        public static string HashToken(string token, string secretKey)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Base64UrlEncode(hashBytes);
        }

        public static string Encrypt(string plainText, string secretKey)
        {
            using var aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(secretKey));
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            var result = new byte[aes.IV.Length + cipherBytes.Length];
            Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
            Buffer.BlockCopy(cipherBytes, 0, result, aes.IV.Length, cipherBytes.Length);

            return Convert.ToBase64String(result);
        }

        public static string Decrypt(string cipherText, string secretKey)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(secretKey));

            var iv = new byte[16];
            var cipherBytes = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipherBytes, 0, cipherBytes.Length);

            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }

        #region Private Methods
        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');
        }

        private static SigningCredentials GetSigningCredentials(string privateKey)
        {
            var key = Encoding.UTF8.GetBytes(privateKey);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private static List<Claim> GetClaims(string userId, string email, string[] roles, string issuer)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, userId),
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            return claims;
        }

        private static JwtSecurityToken GetJwtSecurityToken(List<Claim> claims,
                                                           SigningCredentials creds,
                                                           JwtSettings jwtSettings,
                                                           DateTime now,
                                                           string audience)
        {
            var jwt = new JwtSecurityToken(
                    issuer: jwtSettings.Issuer,
                    audience: audience,
                    claims: claims,
                    notBefore: now,
                    expires: now.AddMinutes(jwtSettings.ExpirationMinutes),
                    signingCredentials: creds
                );
            return jwt;
        }
        #endregion
    }
}