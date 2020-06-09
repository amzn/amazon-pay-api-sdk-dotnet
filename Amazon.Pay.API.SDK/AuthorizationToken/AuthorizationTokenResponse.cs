using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.AuthorizationToken
{
    public class AuthorizationTokenResponse : AmazonPayResponse
    {
        /// <summary>
        /// HS256 encoded JWT Token that will be used to make V2 API calls on behalf of the merchant.
        /// </summary>
        [JsonProperty(PropertyName = "authorizationToken")]
        public string AuthorizationToken { get; internal set; }
    }
}
