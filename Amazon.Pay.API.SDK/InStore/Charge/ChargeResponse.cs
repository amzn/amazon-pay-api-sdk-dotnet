using Amazon.Pay.API.InStore.Types;
using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.InStore.Charge
{
    public class ChargeResponse : AmazonPayResponse
    {
        /// <summary>
        /// Charge identifer.
        /// </summary>
        [JsonProperty(PropertyName = "chargeId")]
        public string ChargeId { get; internal set; }

        /// <summary>
        /// Charge status.
        /// </summary>
        [JsonProperty(PropertyName = "chargeStatus")]
        public TransactionStatus ChargeStatus { get; internal set; }
    }
}
