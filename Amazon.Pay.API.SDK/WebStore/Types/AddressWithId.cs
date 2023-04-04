using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class AddressWithId : Address
    {
        /// <summary>
        /// Amazon Address ID from shippingAddressList in PayAndShipMultiAddress productType.
        /// </summary>
        [JsonProperty(PropertyName = "addressId")]
        public string AddressId { get; internal set; }
    }
}