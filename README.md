# 01System_Encryption_Dcryption_Application

3. String Encryption
Develop an algorithm to encrypt a string and another algorithm to decrypt the string. The encrypt
methods should take an input string and a key and output an encrypted string. The decrypt methods
should take an encrypted string and a key and output a decrypted string.
If the same input string is encrypted with a different key the output should be different.

# Application build in .NET 6

#This is a console applicaion consist of two methods Encryption and Decryption using Key by AES approch.
1). Encryption
2). Decryption

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _01System_Encryption_Dcryption_Application.Helper
{
    public static class SecurityHelper
    {

        // Encrypt the string by using key
        public static string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateKey(key);

                // Use the first 16 bytes of the key as IV
                aesAlg.IV = aesAlg.Key.Take(16).ToArray();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }


        // Decrypt the string by using key
        public static string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateKey(key);

                // Use the first 16 bytes of the key as IV
                aesAlg.IV = aesAlg.Key.Take(16).ToArray();  

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static byte[] GenerateKey(string key)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
        }
    }
}



# program.cs class

using _01System_Encryption_Dcryption_Application.Helper;

Console.WriteLine("---------Welcome to 01System Encryption and Decryption Application ---------");


Console.Write("Enter a string to encrypt: ");
string plainText = Console.ReadLine()!;

Console.Write("Enter a key for encryption: ");
string key = Console.ReadLine()!;

// Encrypt the string
string encrypted = SecurityHelper.Encrypt(plainText, key);
Console.WriteLine("Encrypted Value: " + encrypted);

Console.WriteLine("\nNow let's decrypt the string.");

Console.Write("Enter the encrypted string: ");
string cipherText = Console.ReadLine()!;

Console.Write("Please Enter the same key for decryption: ");
string decryptionKey = Console.ReadLine()!;

// Decrypt the string
string decrypted = SecurityHelper.Decrypt(cipherText, decryptionKey);
Console.WriteLine("Decrypted Value: " + decrypted);
