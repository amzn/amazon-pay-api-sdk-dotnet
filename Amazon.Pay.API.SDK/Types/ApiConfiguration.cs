using Amazon.Pay.API.Types;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;

using Environment = Amazon.Pay.API.Types.Environment;

namespace Amazon.Pay.API
{
    public class ApiConfiguration
    {
        private string privateKey;
        /// <summary>
        /// Initializes a new instance of the ApiConfiguration class.
        /// </summary>
        /// <param name="region">The payment region the Amazon Pay merchant account is registered in.</param>
        /// <param name="environment">The Amazon Pay environment (live or sandbox).</param>
        /// <param name="publicKeyId">The identifier for the registered key pair.</param>
        /// <param name="privateKey">The private key in form of a file path, or directly as a string.</param>
        /// <param name="algorithm">The Amazon Signature algorithm, uses AmazonSignatureAlgorithm.Default, if not specified.</param>
        public ApiConfiguration(Region region, Environment environment, string publicKeyId, string privateKey, AmazonSignatureAlgorithm algorithm = AmazonSignatureAlgorithm.Default)
        {
            Region = region;
            Environment = environment;
            PublicKeyId = publicKeyId;
            PrivateKey = privateKey;
            Algorithm = algorithm;
        }

        /// <summary>
        /// Initializes a new instance of the ApiConfiguration class without Enviroment.
        /// Use this initialization for having environment specific publicKeyId (i.e PublicKeyId starts with prefix LIVE or SANDBOX)
        /// </summary>
        /// <param name="region">The payment region the Amazon Pay merchant account is registered in.</param>
        /// <param name="publicKeyId">The identifier for the registered key pair.</param>
        /// <param name="privateKey">The private key in form of a file path, or directly as a string.</param>
        /// <param name="algorithm">The Amazon Signature algorithm, uses AmazonSignatureAlgorithm.Default, if not specified.</param>
        public ApiConfiguration(Region region, string publicKeyId, string privateKey, AmazonSignatureAlgorithm algorithm = AmazonSignatureAlgorithm.Default)
        {
            Region = region;
            PublicKeyId = publicKeyId;
            PrivateKey = privateKey;
            Algorithm = algorithm;
        }

        /// <summary>
        /// The payment region the Amazon Pay merchant account is registered in.
        /// </summary>
        public Region Region { get; set; }

        /// <summary>
        /// The identifier for the registered key pair.
        /// </summary>
        public string PublicKeyId { get; set; }

        /// <summary>
        /// The private key in form of a file path, or directly as a string.
        /// </summary>
        public string PrivateKey
        {
            get { return privateKey; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(PrivateKey), "A private key (file path or PEM content) must be provided");
                }

                string normalizedValue = NormalizePrivateKey(value);
                if (normalizedValue.Length == 0)
                {
                    throw new ArgumentException("A private key (file path or PEM content) must be provided", nameof(PrivateKey));
                }

                if (normalizedValue.Contains(PemBeginMarker))
                {
                    privateKey = ValidatePemFormat(normalizedValue, "Provided private key is not in the expected PEM format");
                    return;
                }

                string filePath = IsExistingFile(value) ? value : (IsExistingFile(normalizedValue) ? normalizedValue : null);
                if (filePath != null)
                {
                    string fileContent;
                    try
                    {
                        fileContent = File.ReadAllText(filePath);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Provided private key file could not be read", nameof(PrivateKey));
                    }

                    privateKey = ValidatePemFormat(NormalizePrivateKey(fileContent), "Provided file does not contain a private key in the expected format");
                    return;
                }

                throw new FileNotFoundException("Provided private key file cannot be found");
            }
        }

        private const string PemBeginMarker = "-----BEGIN";

        private static string NormalizePrivateKey(string value)
        {
            return value.Trim().TrimStart('\uFEFF').Trim();
        }

        private static string ValidatePemFormat(string pem, string errorMessage)
        {
            if (!pem.StartsWith(PemBeginMarker, StringComparison.Ordinal))
            {
                throw new ArgumentException(errorMessage, nameof(PrivateKey));
            }

            if (pem.StartsWith(PemBeginMarker + " OPENSSH PRIVATE KEY", StringComparison.Ordinal))
            {
                throw new ArgumentException("OpenSSH private keys are not supported, generate the key in PEM format (e.g. ssh-keygen -m PEM)", nameof(PrivateKey));
            }

            object pemObject;
            try
            {
                pemObject = new PemReader(new StringReader(pem)).ReadObject();
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to parse the private key", nameof(PrivateKey));
            }

            if (!IsRsaPrivateKey(pemObject))
            {
                throw new ArgumentException("Unsupported private key format", nameof(PrivateKey));
            }

            return pem;
        }

        private static bool IsRsaPrivateKey(object pemObject)
        {
            AsymmetricKeyParameter key = pemObject is AsymmetricCipherKeyPair pair ? pair.Private : pemObject as AsymmetricKeyParameter;
            return key is RsaKeyParameters && key.IsPrivate;
        }

        private static bool IsExistingFile(string path)
        {
            try
            {
                return new FileInfo(path).Exists;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// The Amazon Pay environment (live or sandbox).
        /// </summary>
        public Environment Environment { get; set; }

        /// <summary>
        /// The Amazon Signature algorithm, uses AmazonSignatureAlgorithm.Default, if not specified.
        /// </summary>
        public AmazonSignatureAlgorithm Algorithm { get; set; }

        /// <summary>
        /// Returns the API Version associated to this configuration.
        /// </summary>
        /// <remarks>
        /// API version is hard-coded per major version of this library as different 
        /// versions may have different object types, property names, etc.
        /// </remarks>
        public int ApiVersion { get { return Constants.ApiVersion; } }

        /// <summary>
        /// Specifies how often the API client will retry an API request in case of failure.
        /// </summary>
        public int MaxRetries { get; set; } = 3;
    }
}
