using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class WebCheckoutDetails
    {
        /// <summary>
        /// Checkout review URL provided by the merchant. Amazon Pay will redirect to this URL after the buyer selects their preferred payment instrument and shipping address.
        /// </summary>
        [JsonProperty(PropertyName = "checkoutReviewReturnUrl")]
        public string CheckoutReviewReturnUrl { get; set; }

        /// <summary>
        /// Checkout result URL provided by the merchant. Amazon Pay will redirect to this URL after completing the transaction.
        /// </summary>
        [JsonProperty(PropertyName = "checkoutResultReturnUrl")]
        public string CheckoutResultReturnUrl { get; set; }

        /// <summary>
        /// URL provided by Amazon Pay. Merchant will redirect to this page after setting transaction details to complete checkout.
        /// </summary>
        [JsonProperty(PropertyName = "amazonPayRedirectUrl")]
        public string AmazonPayRedirectUrl { get; internal set; }
    }
}
