using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class Limits
    {
        [JsonProperty(PropertyName = "amountLimit")]
        public Price AmountLimit { get; internal set; }

        [JsonProperty(PropertyName = "amountBalance")]
        public Price AmountBalance { get; internal set; }
    }
}
