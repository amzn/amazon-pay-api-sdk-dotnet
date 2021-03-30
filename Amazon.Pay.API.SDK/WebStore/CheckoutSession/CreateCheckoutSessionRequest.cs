using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.CheckoutSession
{
    public class CreateCheckoutSessionRequest : UpdateCheckoutSessionRequest
    {

        /// <summary>
        /// Initializes a new instance of the CreateCheckoutSessionRequest class.
        /// </summary>
        /// <param name="checkoutReviewReturnUrl">Checkout review URL provided by the merchant. Amazon Pay will redirect to this URL after the buyer selects their preferred payment instrument and shipping address.</param>
        /// <param name="storeId">Store ID as defined in Seller Central.</param>
        public CreateCheckoutSessionRequest(string checkoutReviewReturnUrl, string storeId) : base()
        {
            WebCheckoutDetails.CheckoutReviewReturnUrl = checkoutReviewReturnUrl;
            StoreId = storeId;
            DeliverySpecifications = new DeliverySpecifications();
            AddressDetails = new AddressDetails();
        }

        /// <summary>
        /// Initializes a new instance of the CreateCheckoutSessionRequest class.
        /// </summary>
        public CreateCheckoutSessionRequest() : base()
        {
            DeliverySpecifications = new DeliverySpecifications();
            AddressDetails = new AddressDetails();
        }

        /// <summary>
        /// Initializes a new instance of the CreateCheckoutSessionRequest class.
        /// </summary>
        /// <param name="checkoutReviewReturnUrl">Checkout review URL provided by the merchant. Amazon Pay will redirect to this URL after the buyer selects their preferred payment instrument and shipping address.</param>
        /// <param name="storeId">Store ID as defined in Seller Central.</param>
        /// <param name="scopes">Scopes passed by merchant to request buyer data</param>
        public CreateCheckoutSessionRequest(string checkoutReviewReturnUrl, string storeId, CheckoutSessionScope[] checkoutSessionScopes) : base()
        {
            WebCheckoutDetails.CheckoutReviewReturnUrl = checkoutReviewReturnUrl;
            StoreId = storeId;
            DeliverySpecifications = new DeliverySpecifications();
            AddressDetails = new AddressDetails();
            CheckoutSessionScope = checkoutSessionScopes;
        }

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
        /// Specify shipping address when CheckoutMode=ProcessOrder (Additional Payment Button, APB, mode).
        /// </summary>
        [JsonProperty(PropertyName = "addressDetails")]
        public AddressDetails AddressDetails { get; internal set; }

        /// <summary>
        /// Checkout Session Scopes
        /// </summary>
        [JsonProperty(PropertyName = "scopes")]
        public CheckoutSessionScope[] CheckoutSessionScope { get; internal set; }

    }
}
