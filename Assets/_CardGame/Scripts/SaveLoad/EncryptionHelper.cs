using System;
using System.Text;

public static class EncryptionHelper
{
    private static readonly string key = "qqkQuSOZQlvMKsvepe/CXxvki28iP7FT"; // Use a more secure key

    public static string Encrypt(string plainText)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        for (var i = 0; i < plainBytes.Length; i++) plainBytes[i] ^= keyBytes[i % keyBytes.Length];
        return Convert.ToBase64String(plainBytes);
    }

    public static string Decrypt(string encryptedText)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var encryptedBytes = Convert.FromBase64String(encryptedText);
        for (var i = 0; i < encryptedBytes.Length; i++) encryptedBytes[i] ^= keyBytes[i % keyBytes.Length];
        return Encoding.UTF8.GetString(encryptedBytes);
    }
}