using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.CheckoutSession
{
    public class CreateCheckoutSessionRequest : ApiRequestBody
    {
        /// <summary>
        /// Initializes a new instance of the CreateCheckoutSessionRequest class. 
        /// </summary>
        /// <param name="checkoutReviewReturnUrl">Checkout review URL provided by the merchant. Amazon Pay will redirect to this URL after the buyer selects their preferred payment instrument and shipping address.</param>
        /// <param name="storeId">Store ID as defined in Seller Central.</param>
        public CreateCheckoutSessionRequest(string checkoutReviewReturnUrl, string storeId)
        {
            WebCheckoutDetails = new WebCheckoutDetails();
            DeliverySpecifications = new DeliverySpecifications();
            PaymentDetails = new PaymentDetails();
            MerchantMetadata = new MerchantMetadata();
            ProviderMetadata = new ProviderMetadata();

            WebCheckoutDetails.CheckoutReviewReturnUrl = checkoutReviewReturnUrl;
            StoreId = storeId;
        }

        /// <summary>
        /// URLs associated to the Checkout Session used for completing checkout
        /// </summary>
        [JsonProperty(PropertyName = "webCheckoutDetails")]
        public WebCheckoutDetails WebCheckoutDetails { get; internal set; }

        /// <summary>
        /// Login with Amazon client ID. Do not use the application ID.
        /// </summary>
        [JsonProperty(PropertyName = "storeId")]
        public string StoreId { get; set; }

        /// <summary>
        /// Specify shipping restrictions to prevent buyers from selecting unsupported addresses from their Amazon address book.
        /// </summary>
        [JsonProperty(PropertyName = "deliverySpecifications")]
        public DeliverySpecifications DeliverySpecifications { get; internal set; }

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
