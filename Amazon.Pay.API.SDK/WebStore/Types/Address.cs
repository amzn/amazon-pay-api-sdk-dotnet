using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class Address
    {
        /// <summary>
        /// Address name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; internal set; }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        [JsonProperty(PropertyName = "addressLine1")]
        public string AddressLine1 { get; internal set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        [JsonProperty(PropertyName = "addressLine2")]
        public string AddressLine2 { get; internal set; }

        /// <summary>
        /// The third line of the address.
        /// </summary>
        [JsonProperty(PropertyName = "addressLine3")]
        public string AddressLine3 { get; internal set; }

        /// <summary>
        /// City of the address.
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; internal set; }

        /// <summary>
        /// County of the address.
        /// </summary>
        [JsonProperty(PropertyName = "county")]
        public string County { get; internal set; }

        /// <summary>
        /// District of the address.
        /// </summary>
        [JsonProperty(PropertyName = "district")]
        public string District { get; internal set; }

        /// <summary>
        /// The state or region.
        /// </summary>
        [JsonProperty(PropertyName = "stateOrRegion")]
        public string StateOrRegion { get; internal set; }

        /// <summary>
        /// Postal code of the address.
        /// </summary>
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; internal set; }

        /// <summary>
        /// Country code of the address in ISO 3166 format.
        /// </summary>
        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; internal set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; internal set; }
    }
}