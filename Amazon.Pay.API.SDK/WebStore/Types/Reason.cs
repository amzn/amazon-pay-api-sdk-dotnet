using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class Reason
    {
        /// <summary>
        /// Reason code for current state.
        /// </summary>
        [JsonProperty(PropertyName = "reasonCode")]
        public string ReasonCode { get; internal set; }

        /// <summary>
        /// An optional description of the Checkout Session state.
        /// </summary>
        [JsonProperty(PropertyName = "reasonDescription")]
        public string ReasonDescription { get; internal set; }
    }
}
