using AuthenticationService.Contracts.Handlers;
using System;
using System.Security.Cryptography;

namespace AuthenticationService.Application.Handlers
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int _iterations = 100;
        private const int _saltLength = 24;
        private const int _hashLength = 24;
        private const char _splitter = '|';

        /// <inheritdoc/>
        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Invalid format");

            //TODO: implement proper hashing + salt passwords
            var saltBytes = new byte[_saltLength];
            RandomNumberGenerator.Create().GetBytes(saltBytes);
            using var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, _iterations);
            var hash = deriveBytes.GetBytes(_hashLength);

            var passwordHash = $"{Convert.ToBase64String(saltBytes)}{_splitter}" + $"{_iterations}{_splitter}" + $"{Convert.ToBase64String(hash)}";

            return passwordHash;
        }

        /// <inheritdoc/>
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

        private bool Compare(ReadOnlySpan<byte> bArray1, ReadOnlySpan<byte> bArray2)
        {
            return bArray1.SequenceEqual(bArray2);
        }
    }
}
