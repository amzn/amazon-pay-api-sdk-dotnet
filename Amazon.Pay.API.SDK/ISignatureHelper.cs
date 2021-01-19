using System;
using System.Collections.Generic;
using Amazon.Pay.API.Types;

namespace Amazon.Pay.API
{
    public interface ISignatureHelper
    {
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
        string CreateCanonicalRequest(ApiRequest apiRequest, Dictionary<String, List<String>> preSignedHeaders);

        /// <summary>
        /// Generates the mandatory headers required in the request
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>dictionary of required headers</returns>
        Dictionary<string, List<string>> CreateDefaultHeaders(Uri uri);

        /// <summary>
        /// Creates the string that is going to be signed
        /// </summary>
        /// <param name="canonicalRequest"></param>
        /// <returns>string to sign</returns>
        string CreateStringToSign(string canonicalRequest);

        /// <summary>
        /// Generates a signature for the string passed in
        /// </summary>
        /// <param name="stringToSign"></param>
        /// <returns>signature string</returns>
        string GenerateSignature(string stringToSign);
    }
}