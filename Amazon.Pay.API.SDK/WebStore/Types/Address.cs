using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class Address : AddressBase
    {
        /// <summary>
        /// County of the address.
        /// </summary>
        [JsonProperty(PropertyName = "county")]
        public string County { get; set; }

        /// <summary>
        /// District of the address.
        /// </summary>
        [JsonProperty(PropertyName = "district")]
        public string District { get; set; }
    }
}
