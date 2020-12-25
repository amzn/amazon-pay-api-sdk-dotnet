using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Buyer
{
    public class BuyerResponse : AmazonPayResponse
    {
        /// <summary>
        /// Unique Amazon Pay buyer identifier.
        /// </summary>
        [JsonProperty(PropertyName = "buyerId")]
        public string BuyerId { get; internal set; }

        /// <summary>
        /// Buyer default shipping address country.
        /// </summary>
        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; internal set; }

        /// <summary>
        /// Buyer name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Buyer email address.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; internal set; }

        /// <summary>
        /// Buyer default shipping address postal code.
        /// </summary>
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; internal set; }

        /// <summary>
        /// Shipping address selected by the buyer.
        /// </summary>
        [JsonProperty(PropertyName = "shippingAddress")]
        public Address ShippingAddress { get; internal set; }
    }
}
