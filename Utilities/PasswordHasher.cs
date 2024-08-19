﻿using financial_manager.Utilities.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace financial_manager.Utilities
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;
        public string HashPassword(string rawPassword, byte[] salt)
        {
            var hashedPassword = KeyDerivation.Pbkdf2(
                password: rawPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: HashSize);

            return Convert.ToBase64String(hashedPassword);
        }

        public byte[] GenerateSalt()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);
                return salt;
            }
        }

        public bool VerifyPassword(string rawPassword, string hashedPassword, byte[] salt)
        {
            var hashedInputPassword = HashPassword(rawPassword, salt);

            return hashedPassword == hashedInputPassword;
        }
    }
}
