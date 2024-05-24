using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace NewsService.Utility
{
    public class PasswordHelper
    {
        private const char Delimiter = ':';
        public static string GenerateSaltedHash(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password and encode the salt with the hash
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            //return (salt, hash);
            return $"{Convert.ToBase64String(salt)}{Delimiter}{hash}";
        }
        public static bool VerifyPassword(string password, string storedSaltedHash)
        {
            // Retrieve salt and hash from the storedSaltedHash string
            string[] parts = storedSaltedHash.Split(Delimiter);
            if (parts.Length != 2)
            {
                return false; // Invalid format
            }

            byte[] storedSalt = Convert.FromBase64String(parts[0]);
            string storedHash = parts[1];

            // Compute the hash of the entered password using the retrieved salt
            string computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: storedSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            // Compare computedHash with the stored hash
            return computedHash == storedHash;
        }
    }

}

