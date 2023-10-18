using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public abstract class AddressBase : AddressInfoBase
    {
        /// <summary>
        /// Address name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The third line of the address.
        /// </summary>
        [JsonProperty(PropertyName = "addressLine3")]
        public string AddressLine3 { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
