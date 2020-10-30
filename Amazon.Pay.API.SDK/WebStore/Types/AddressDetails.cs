using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class AddressDetails : AddressBase
    {
        /// <summary>
        /// Distrit or County of the address.
        /// </summary>
        [JsonProperty(PropertyName = "districtOrCounty")]
        public string DistrictOrCounty { get; set; }
    }
}
