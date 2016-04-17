using System;
using System.Security.Cryptography;
using System.Text;

namespace meteosat.model
{
    public class EncryptionHandler
    {
        public string Encrypt(string text)
        {
            var encodedBytes = Encoding.Unicode.GetBytes(text);
            var encryptedBytes = ProtectedData.Protect(encodedBytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string text)
        {
            var encryptedBytes = Convert.FromBase64String(text);
            var encodedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(encodedBytes);
        }
    }
}
