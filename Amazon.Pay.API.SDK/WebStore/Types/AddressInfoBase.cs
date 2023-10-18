using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public abstract class AddressInfoBase
    {
        /// <summary>
        /// Gets or sets address line 1.
        /// </summary>
        [JsonProperty(PropertyName = "addressLine1")]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets address line 2.
        /// </summary>
        [JsonProperty(PropertyName = "addressLine2")]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets state or region.
        /// </summary>
        [JsonProperty(PropertyName = "stateOrRegion")]
        public string StateOrRegion { get; set; }

        /// <summary>
        /// Gets or sets postal code.
        /// </summary>
        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets postal code.
        /// </summary>
        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }
    }
}