# 01System Encryption and Decryption Application

Welcome to the 01System Encryption and Decryption Application! This application allows you to encrypt and decrypt strings using a specified key.

## Features

- **Encrypt**: Encrypt a plain text string using a key.
- **Decrypt**: Decrypt an encrypted string using the same key.

## Getting Started

### Prerequisites

- .NET 6.0 SDK or later

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/your-username/01System_Encryption_Decryption_Application.git
    cd 01System_Encryption_Decryption_Application
    ```

2. Build the project:
    ```sh
    dotnet build
    ```

### Usage

1. Run the application:
    ```sh
    dotnet run
    ```

2. Follow the prompts to encrypt and decrypt strings.

### Example

plaintext
---------Welcome to 01System Encryption and Decryption Application ---------

Enter a string to encrypt: HelloWorld
Enter a key for encryption: MySecretKey

Encrypted Value: <encrypted_string>

Now let's decrypt the string.

Enter the encrypted string: <encrypted_string>
Please Enter the same key for decryption: MySecretKey

Decrypted Value: HelloWorld


# Code Explanation
SecurityHelper.cs

This class contains methods for encrypting and decrypting strings using the AES algorithm.

        using System;
        using System.IO;
        using System.Linq;
        using System.Security.Cryptography;
        using System.Text;

        namespace _01System_Encryption_Dcryption_Application.Helper
        {
            public static class SecurityHelper
            {        
        public static string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateKey(key);
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

        public static string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateKey(key);
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

# Program.cs

This class contains the main program logic to interact with the user for encryption and decryption.

        using _01System_Encryption_Dcryption_Application.Helper;

        Console.WriteLine("---------Welcome to 01System Encryption and Decryption Application ---------");

        Console.Write("Enter a string to encrypt: ");
        string plainText = Console.ReadLine()!;

        Console.Write("Enter a key for encryption: ");
        string key = Console.ReadLine()!;

        string encrypted = SecurityHelper.Encrypt(plainText, key);
        Console.WriteLine("Encrypted Value: " + encrypted);

        Console.WriteLine("\nNow let's decrypt the string.");

        Console.Write("Enter the encrypted string: ");
        string cipherText = Console.ReadLine()!;

        Console.Write("Please Enter the same key for decryption: ");
        string decryptionKey = Console.ReadLine()!;

        string decrypted = SecurityHelper.Decrypt(cipherText, decryptionKey);
        Console.WriteLine("Decrypted Value: " + decrypted);



