using System.Security.Cryptography;
using System.Text;

namespace Reneee.Identity.Utils
{
    public static class RSAKeyUtils
    {
        public static RSA GetPrivateKey(string privateKeyPath)
        {
            ValidatePath(privateKeyPath);
            var privateKey = File.ReadAllBytes(privateKeyPath);
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);
            return rsa;
        }

        public static RSA GetPublicKey(string publicKeyPath)
        {
            ValidatePath(publicKeyPath);
            var publicKey = File.ReadAllBytes(publicKeyPath);
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(publicKey, out _);
            return rsa;
        }

        public static void EnsureRsaKeys(string privateKeyPath, string publicKeyPath)
        {
            if (string.IsNullOrWhiteSpace(privateKeyPath))
            {
                throw new ArgumentException("Private key path is null or empty", nameof(privateKeyPath));
            }

            if (string.IsNullOrWhiteSpace(publicKeyPath))
            {
                throw new ArgumentException("Public key path is null or empty", nameof(publicKeyPath));
            }

            if (!File.Exists(privateKeyPath) || !File.Exists(publicKeyPath))
            {
                using var rsa = RSA.Create(2048);
                var privateKey = rsa.ExportRSAPrivateKey();
                var publicKey = rsa.ExportRSAPublicKey();

                File.WriteAllBytes(privateKeyPath, privateKey);
                File.WriteAllBytes(publicKeyPath, publicKey);
            }
        }
        private static void ValidatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path is null or empty", nameof(path));
            }
        }
    }
}
