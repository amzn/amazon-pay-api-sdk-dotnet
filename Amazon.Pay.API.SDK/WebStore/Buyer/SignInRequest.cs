using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Buyer
{
    public class SignInRequest : ApiRequestBody
    {
        /// <summary>
        /// Initializes a new instance of the SignInRequest class.
        /// </summary>
        /// <param name="checkoutReviewReturnUrl">Checkout review URL provided by the merchant. Amazon Pay will redirect to this URL after the buyer selects their preferred payment instrument and shipping address.</param>
        /// <param name="storeId">Store ID as defined in Seller Central.</param>
        public SignInRequest(string signInReturnUrl, string storeId)
        {
            SignInReturnUrl = signInReturnUrl;
            StoreId = storeId;
        }

        /// <summary>
        /// Initializes a new instance of the SignInRequest class.
        /// </summary>
        /// <param name="checkoutReviewReturnUrl">Checkout review URL provided by the merchant. Amazon Pay will redirect to this URL after the buyer selects their preferred payment instrument and shipping address.</param>
        /// <param name="storeId">Store ID as defined in Seller Central.</param>
        public SignInRequest(string signInReturnUrl, string storeId, params SignInScope[] signInScopes) : this(signInReturnUrl, storeId)
        {
            SignInScopes = signInScopes;
        }

        /// <summary>
        /// Login with Amazon client ID. Do not use the application ID.
        /// </summary>
        [JsonProperty(PropertyName = "storeId")]
        public string StoreId { get; set; }

        /// <summary>
        /// Amazon Pay will redirect to this URL after the buyer signs in.
        /// </summary>
        [JsonProperty(PropertyName = "signInReturnUrl")]
        public string SignInReturnUrl { get; set; }

        /// <summary>
        /// The buyer details that you're requesting access for.
        /// </summary>
        /// <remarks>
        /// Note that Amazon Pay will always return BuyerId even if no values are set for this parameter.
        /// </remarks>
        [JsonProperty(PropertyName = "signInScopes")]
        public SignInScope[] SignInScopes { get; set; }
    }
}
