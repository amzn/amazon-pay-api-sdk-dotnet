using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public abstract class AddressBase
    {
        /// <summary>
        /// Address name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        [JsonProperty(PropertyName = "addressLine1")]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        [JsonProperty(PropertyName = "addressLine2")]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The third line of the address.
        /// </summary>
        [JsonProperty(PropertyName = "addressLine3")]
        public string AddressLine3 { get; set; }

        /// <summary>
        /// City of the address.
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// The state or region.
        /// </summary>
        [JsonProperty(PropertyName = "stateOrRegion")]
        public string StateOrRegion { get; set; }

        /// <summary>
        /// Postal code of the address.
        /// </summary>
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Country code of the address in ISO 3166 format.
        /// </summary>
        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
