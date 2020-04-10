using Amazon.Pay.API.InStore.Types;
using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.InStore.Refund
{
    public class RefundResponse : AmazonPayResponse
    {
        /// <summary>
        /// Refund identifier.
        /// </summary>
        [JsonProperty(PropertyName = "refundId")]
        public string RefundId { get; internal set; }

        /// <summary>
        /// Charge status.
        /// </summary>
        [JsonProperty(PropertyName = "refundStatus")]
        public TransactionStatus RefundStatus { get; internal set; }
    }
}
