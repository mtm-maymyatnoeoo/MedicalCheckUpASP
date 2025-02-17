using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace MedicalCheckUpASP.Common
{
    public class PasswordEncryptionService
    {
        // Key and IV should be stored securely and should not be hardcoded in real applications
        private readonly string encryptionKey = "Hello123"; // 16 bytes for AES-128
        //private readonly string iv = "HelloIV123"; // 16 bytes for AES

        // Encrypt password (AES)
        public string EncryptPassword(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                //aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
                // Ensure the key size is 16 bytes for AES-128
                aesAlg.Key = GenerateValidKey(encryptionKey);
                //aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.IV = new byte[16];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        // Decrypt password (AES)
        public string DecryptPassword(string encryptedPassword)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GenerateValidKey(encryptionKey);
                //aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.IV = new byte[16];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
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

        private byte[] GenerateValidKey(string encryptionKey)
        {
            // Ensure the key length is exactly 16 bytes
            byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);

            if (keyBytes.Length < 16)
            {
                Array.Resize(ref keyBytes, 16); // Pad the key with zeros if it's shorter than 16 bytes
            }
            else if (keyBytes.Length > 16)
            {
                Array.Resize(ref keyBytes, 16); // Trim the key if it's longer than 16 bytes
            }

            return keyBytes;
        }
    }
}
