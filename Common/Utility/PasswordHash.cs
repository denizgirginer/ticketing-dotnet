using System;
using System.Security.Cryptography;

namespace Ticket.Common.Utility
{
    public static class PasswordHash
    {
        private static int HASH_LENGTH = 16;
        private static int PASSWORD_LENGTH = 20;
        private static byte[] GetSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[HASH_LENGTH]);
            return salt;
        }

        public static string HashPassword(string password)
        {
            var salt = GetSalt();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(PASSWORD_LENGTH);

            byte[] hashBytes = new byte[HASH_LENGTH+PASSWORD_LENGTH];
            Array.Copy(salt, 0, hashBytes, 0, HASH_LENGTH);
            Array.Copy(hash, 0, hashBytes, HASH_LENGTH, PASSWORD_LENGTH);

            return Convert.ToBase64String(hashBytes);
        }

        public static void ValidatePassword(string password, string savedPasswordHash)
        {
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[HASH_LENGTH];
            Array.Copy(hashBytes, 0, salt, 0, HASH_LENGTH);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(PASSWORD_LENGTH);
            /* Compare the results */
            for (int i = 0; i < PASSWORD_LENGTH; i++)
                if (hashBytes[i + HASH_LENGTH] != hash[i])
                    throw new UnauthorizedAccessException();
        }
    }
}
