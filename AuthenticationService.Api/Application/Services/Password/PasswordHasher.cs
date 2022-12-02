using System;
using System.Security.Cryptography;

namespace AuthenticationService.Api.Application.Services.Password
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int _iterations = 10102;
        private const int _saltLength = 24;
        private const int _hashLength = 24;
        private const char _splitter = '|';

        /// <summary>
        /// Hashing password 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Invalid format");

            var saltBytes = new byte[_saltLength];
            using var randomNumber = new RNGCryptoServiceProvider();
            randomNumber.GetBytes(saltBytes);
            using var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, _iterations);
            var hash = deriveBytes.GetBytes(_hashLength);

            var passwordHash = $"{Convert.ToBase64String(saltBytes)}{_splitter}" + $"{_iterations}{_splitter}" + $"{Convert.ToBase64String(hash)}";

            return passwordHash;
        }

        /// <summary>
        /// Verifies incoming and current password
        /// </summary>
        /// <param name="verifiedPassword">Password to verify</param>
        /// <param name="currentPassword">Password hash in DB</param>
        /// <returns></returns>
        public bool IsValid(string verifiedPassword, string currentPassword)
        {
            if (string.IsNullOrWhiteSpace(verifiedPassword) || string.IsNullOrWhiteSpace(currentPassword))
                throw new ArgumentException("Invalid format");

            var passwordItems = currentPassword.Split(_splitter);
            if (passwordItems.Length != 3)
                return false;

            byte[] currentPasswordSalt;
            byte[] currentPasswordHash;
            int currentPasswordIterations;

            try
            {
                currentPasswordSalt = Convert.FromBase64String(passwordItems[0]);
                currentPasswordIterations = int.Parse(passwordItems[1]);
                currentPasswordHash = Convert.FromBase64String(passwordItems[2]);
            }
            catch (FormatException)
            {
                return false;
            }

            using var deriveBytes = new Rfc2898DeriveBytes(verifiedPassword, currentPasswordSalt, currentPasswordIterations);
            var verifiedPasswordHash = deriveBytes.GetBytes(currentPasswordHash.Length);

            var result = Compare(currentPasswordHash, verifiedPasswordHash);

            return result;
        }

        private bool Compare(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
        {
            return a1.SequenceEqual(a2);
        }
    }
}
