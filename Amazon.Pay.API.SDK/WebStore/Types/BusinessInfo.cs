using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class BusinessInfo
    {
        public BusinessInfo()
        {
            BusinessAddress = new AddressInfo();
        }

        /// <summary>
        /// Gets or sets the business type.
        /// </summary>
        [JsonProperty(PropertyName = "businessType")]
        public BusinessType? BusinessType { get; set; }

        /// <summary>
        /// Gets or sets the country of establishment.
        /// </summary>
        [JsonProperty(PropertyName = "countryOfEstablishment")]
        public string CountryOfEstablishment { get; set; }

        /// <summary>
        /// Gets or sets the business legal name.
        /// </summary>
        [JsonProperty(PropertyName = "businessLegalName")]
        public string BusinessLegalName { get; set; }

        /// <summary>
        /// Gets or sets the business address.
        /// </summary>
        [JsonProperty(PropertyName = "businessAddress")]
        public AddressInfo BusinessAddress { get; set; }
    }
}