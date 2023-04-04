using Amazon.Pay.API.Types;
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
                FileInfo fileInfo;

                try
                {
                    fileInfo = new FileInfo(value);
                } 
                catch(Exception)
                {
                    fileInfo = null;
                }
                
                if (fileInfo != null && fileInfo.Exists)
                {
                    privateKey = File.ReadAllText(value);
                }
                else
                {
                    privateKey = value;
                }

                if (!privateKey.StartsWith("-----"))
                {
                    if (fileInfo.Exists)
                    {
                        throw new ArgumentException("Provided file does not contain a private key in the expected format");
                    }
                    else
                    {
                        throw new FileNotFoundException("Provided private key file cannot be found", privateKey);
                    }
                }
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
