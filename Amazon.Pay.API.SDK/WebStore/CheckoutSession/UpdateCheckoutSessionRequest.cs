using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.CheckoutSession
{
    public class UpdateCheckoutSessionRequest : ApiRequestBody
    {
        public UpdateCheckoutSessionRequest()
        {
            WebCheckoutDetails = new WebCheckoutDetails();
            PaymentDetails = new PaymentDetails();
            MerchantMetadata = new MerchantMetadata();
            ProviderMetadata = new ProviderMetadata();
        }

        /// <summary>
        /// URLs associated to the Checkout Session used for completing checkout
        /// </summary>
        [JsonProperty(PropertyName = "webCheckoutDetails")]
        public WebCheckoutDetails WebCheckoutDetails { get; internal set; }

        /// <summary>
        /// Payment details specified by the merchant, such as the amount and method for charging the buyer
        /// </summary>
        [JsonProperty(PropertyName = "paymentDetails")]
        public PaymentDetails PaymentDetails { get; internal set; }

        /// <summary>
        /// Merchant-provided order info.
        /// </summary>
        [JsonProperty(PropertyName = "merchantMetadata")]
        public MerchantMetadata MerchantMetadata { get; internal set; }

        /// <summary>
        /// Supplementary data.
        /// </summary>
        [JsonProperty(PropertyName = "supplementaryData")]
        public string SupplementaryData { get; set; }

        /// <summary>
        /// Merchant identifer of the Solution Provider (SP) - also known as ecommerce provider.
        /// </summary>
        [JsonProperty(PropertyName = "platformId")]
        public string PlatformId { get; set; }

        /// <summary>
        /// Payment service provider (PSP)-provided order information.
        /// </summary>
        [JsonProperty(PropertyName = "providerMetadata")]
        public ProviderMetadata ProviderMetadata { get; internal set; }
    }
}
