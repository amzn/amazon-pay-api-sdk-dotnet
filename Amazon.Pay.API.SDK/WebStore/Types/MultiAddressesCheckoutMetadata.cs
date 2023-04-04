using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class MultiAddressesCheckoutMetadata
    {
        /// <summary>
        /// Amazon Address ID from shippingAddressList in PayAndShipMultiAddress productType.
        /// </summary>
        [JsonProperty(PropertyName = "addressId")]
        public string AddressId { get; set; }

        /// <summary>
        /// Description of the Checkout Session constraint(s).
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public Price Price { get; set; }
    }
}