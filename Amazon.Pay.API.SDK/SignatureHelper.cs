using Amazon.Pay.API.Types;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Amazon.Pay.API
{
    public class SignatureHelper : ISignatureHelper
    {
        private readonly ApiConfiguration payConfiguration;
        private readonly string LineSeparator = "\n";
        private readonly CanonicalBuilder canonicalBuilder;

        public SignatureHelper(ApiConfiguration payConfiguration, CanonicalBuilder canonicalBuilder)
        {
            this.payConfiguration = payConfiguration;
            this.canonicalBuilder = canonicalBuilder;
        }

        /// <summary>
        /// Creates a string that includes the information from the request in a 
        /// standardized (canonical) format.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethodName"></param>
        /// <param name="parameters"></param>
        /// <param name="preSignedHeaders"></param>
        /// <param name="requestPayload"></param>
        /// <returns>canonical request string</returns>
        /// <seealso cref="http://amazonpaycheckoutintegrationguide.s3.amazonaws.com/amazon-pay-api-v2/signing-requests.html"/>
        public string CreateCanonicalRequest(ApiRequest apiRequest, Dictionary<String, List<String>> preSignedHeaders)
        {
            string path = apiRequest.Path.AbsolutePath;
            StringBuilder canonicalRequestBuilder = new StringBuilder();

            // if a body was passed to the request, convert it into a JSON string
            string body = string.Empty;
            if (apiRequest.Body != null)
            {
                body = apiRequest.Body.ToJson();
            }

            canonicalRequestBuilder.Append(apiRequest.HttpMethod.ToString())
                                    .Append(LineSeparator)
                                    .Append(canonicalBuilder.GetCanonicalizedURI(path))
                                    .Append(LineSeparator)
                                    .Append(canonicalBuilder.GetCanonicalizedQueryString(apiRequest.QueryParameters))
                                    .Append(LineSeparator)
                                    .Append(canonicalBuilder.GetCanonicalizedHeaderString(preSignedHeaders))
                                    .Append(LineSeparator)
                                    .Append(canonicalBuilder.GetSignedHeadersString(preSignedHeaders))
                                    .Append(LineSeparator)
                                    .Append(canonicalBuilder.HashThenHexEncode(body));

            return canonicalRequestBuilder.ToString();
        }

        /// <summary>
        /// Generates the mandatory headers required in the request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>dictionary of required headers</returns>
        public Dictionary<string, List<string>> CreateDefaultHeaders(Uri uri)
        {
            Dictionary<string, List<string>> headers = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            List<string> acceptHeaderValue = new List<string>
             {
                 "application/json"
             };
            headers.Add("accept", acceptHeaderValue);

            List<string> contentHeaderValue = new List<string>
             {
                 "application/json"
             };
            headers.Add("content-type", contentHeaderValue);

            List<string> regionHeaderValue = new List<string>
             {
                 payConfiguration.Region.ToShortform()  // TODO: replace by method parameter to get rid of config dependency
             };
            headers.Add(Constants.Headers.Region, regionHeaderValue);

            List<string> dateHeaderValue = new List<string>
             {
                 Util.GetFormattedTimestamp()
             };
            headers.Add(Constants.Headers.Date, dateHeaderValue);

            List<string> hostHeaderValue = new List<string>
             {
                 uri.Host
             };
            headers.Add(Constants.Headers.Host, hostHeaderValue);

            return headers;
        }

        /// <summary>
        /// Creates the string that is going to be signed
        /// </summary>
        /// <param name="canonicalRequest"></param>
        /// <returns>string to sign</returns>
        public string CreateStringToSign(string canonicalRequest)
        {
            string hashedCanonicalRequest = canonicalBuilder.HashThenHexEncode(canonicalRequest);

            StringBuilder stringToSignBuilder = new StringBuilder(payConfiguration.Algorithm.GetName());
            stringToSignBuilder.Append(LineSeparator)
                                .Append(hashedCanonicalRequest);

            return stringToSignBuilder.ToString();
        }

        /// <summary>
        /// Generates a signature for the string passed in
        /// </summary>
        /// <param name="stringToSign"></param>
        /// <returns>signature string</returns>
        public string GenerateSignature(string stringToSign)
        {
            SecureRandom random = new SecureRandom();

            byte[] bytesToSign = Encoding.UTF8.GetBytes(stringToSign);

            //AMZN-PAY-RSASSA-PSS-V2 uses 32 and AMZN-PAY-RSASSA-PSS uses 20 as salt length 
            int SaltLength = payConfiguration.Algorithm.GetSaltLength();

            // read the private key
            PemReader pemReader = new PemReader(new StringReader(payConfiguration.PrivateKey)); // TODO: replace by method parameter to get rid of config dependency
            object pemObject = pemReader.ReadObject();

            if (pemReader == null)
            {
                throw new ArgumentException("Unsupported private key format");
            }

            ICipherParameters parameters;
            if (pemObject is AsymmetricKeyParameter)
            {
                // PKCS #8 format ("BEGIN PRIVATE KEY")
                parameters = (AsymmetricKeyParameter)pemObject;
            }
            else if (pemObject is AsymmetricCipherKeyPair)
            {
                // RSA key format ("BEGIN RSA PRIVATE KEY")
                var pair = pemObject as AsymmetricCipherKeyPair;
                parameters = pair.Private;
            }
            else
            {
                throw new ArgumentException("Unsupported private key format");
            }

            // initiate the signing object
            PssSigner pssSigner = new PssSigner(new RsaEngine(), new Sha256Digest(), SaltLength);
            pssSigner.Init(true, new ParametersWithRandom(parameters, random));
            pssSigner.BlockUpdate(bytesToSign, 0, bytesToSign.Length);

            // sign it
            byte[] signature = pssSigner.GenerateSignature();

            return Convert.ToBase64String(signature);
        }

    }
}
