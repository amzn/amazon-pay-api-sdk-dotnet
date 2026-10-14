using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Amazon.Pay.API.SDK.Tests
{
    public static class TestKeys
    {
        private static readonly Lazy<AsymmetricCipherKeyPair> keyPair = new Lazy<AsymmetricCipherKeyPair>(() =>
        {
            var generator = new RsaKeyPairGenerator();
            generator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            return generator.GenerateKeyPair();
        });

        private static readonly Lazy<AsymmetricCipherKeyPair> ecKeyPair = new Lazy<AsymmetricCipherKeyPair>(() =>
        {
            var generator = new ECKeyPairGenerator();
            generator.Init(new ECKeyGenerationParameters(SecObjectIdentifiers.SecP256r1, new SecureRandom()));
            return generator.GenerateKeyPair();
        });

        public static AsymmetricCipherKeyPair KeyPair => keyPair.Value;

        public static string EcPrivateKeyPem => ToPem(ecKeyPair.Value.Private);

        public static string EcPkcs8Pem => ToPem(new Pkcs8Generator(ecKeyPair.Value.Private));

        public static string Pkcs1Pem => ToPem(KeyPair.Private);

        public static string Pkcs8Pem => ToPem(new Pkcs8Generator(KeyPair.Private));

        public static string PublicKeyPem => ToPem(KeyPair.Public);

        public static string ToPem(object value)
        {
            var writer = new StringWriter { NewLine = "\n" };
            var pemWriter = new PemWriter(writer);
            pemWriter.WriteObject(value);
            pemWriter.Writer.Flush();
            return writer.ToString().Trim();
        }
    }

    public sealed class TempKeyFile : IDisposable
    {
        public TempKeyFile(string content, Encoding encoding = null)
        {
            Path = System.IO.Path.GetTempFileName();
            try
            {
                File.WriteAllText(Path, content, encoding ?? new UTF8Encoding(false));
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public string Path { get; }

        public void Dispose()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
        }
    }
}
