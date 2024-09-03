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