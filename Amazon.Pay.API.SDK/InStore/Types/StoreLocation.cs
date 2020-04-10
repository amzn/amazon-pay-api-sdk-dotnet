using Newtonsoft.Json;

namespace Amazon.Pay.API.InStore.Types
{
    public class StoreLocation
    {
        public StoreLocation(string countryCode)
        {
            CountryCode = countryCode;
        }

        /// <summary>
        /// Address name.
        /// </summary>
        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }
    }
}