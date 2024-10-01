using System;
using System.Security.Cryptography;
using Common.Helper_Methods;
using Microsoft.AspNetCore.Identity;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 10000;

    public string HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the password and combine with the salt
        byte[] hash = HashPassword(password, salt);

        // Combine salt and hash
        byte[] hashBytes = new byte[SaltSize + KeySize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

        // Convert to base64-encoded string
        string base64Hash = Convert.ToBase64String(hashBytes);
        return base64Hash;
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        // Extract the salt from the hashed password
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        // Compute hash with the extracted salt and compare
        byte[] hash = HashPassword(password, salt);

        for (int i = 0; i < KeySize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
            {
                return false;
            }
        }

        return true;
    }

    private static byte[] HashPassword(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            return pbkdf2.GetBytes(KeySize);
        }
    }
}