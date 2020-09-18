using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.InStore.Types
{
    public class TransactionStatus
    {
        /// <summary>
        /// State of the charge.
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}