using RSAExtensions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PSD_Lab_2.Services
{
    public class LoginService
    {
        private const string LOGIN = "aaa@aaa.aaa";
        private const string PASSWORD = "password";

        private readonly RSACng _serverRSA;

        public LoginService()
        {
            _serverRSA = new RSACng();
        }

        public string GetForm()
            => _serverRSA.ExportPublicKey(RSAKeyType.Pkcs8, true);

        public bool CheckData(string encryptedData)
        {
            var decryptedData = Decrypt(encryptedData);

            var login = decryptedData.Split(" ")[0];
            var password = decryptedData.Split(" ")[1];

            return login == LOGIN && password == PASSWORD;
        }

        private string Decrypt(string encryptedMessage)
        {
            var bytes = Convert.FromBase64String(encryptedMessage);
            var decryptedBytes = _serverRSA.Decrypt(bytes, RSAEncryptionPadding.OaepSHA1);
            var decryptedData = Encoding.UTF8.GetString(decryptedBytes);

            return decryptedData;
        }
    }
}
