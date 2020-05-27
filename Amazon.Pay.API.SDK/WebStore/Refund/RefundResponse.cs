using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.Refund
{
    public class RefundResponse : AmazonPayResponse
    {
        /// <summary>
        /// Refund identifier.
        /// </summary>
        [JsonProperty(PropertyName = "refundId")]
        public string RefundId { get; internal set; }

        /// <summary>
        /// Charge identifer.
        /// </summary>
        [JsonProperty(PropertyName = "chargeId")]
        public string ChargeId { get; internal set; }

        /// <summary>
        /// Amount to be refunded. Refund amount can be either 15% or 75 USD/GBP/EUR (whichever is less) above the captured amount.
        /// </summary>
        [JsonProperty(PropertyName = "refundAmount")]
        public Price RefundAmount { get; internal set; }

        /// <summary>
        /// Description shown on the buyer payment instrument statement.
        /// </summary>
        [JsonProperty(PropertyName = "softDescriptor")]
        public string SoftDescriptor { get; internal set; }

        /// <summary>
        /// UTC date and time when the refund was created in ISO 8601 format.
        /// </summary>
        [JsonProperty(PropertyName = "creationTimestamp")]
        public DateTime CreationTimestamp { get; internal set; }

        /// <summary>
        /// State of the refund object.
        /// </summary>
        [JsonProperty(PropertyName = "statusDetails")]
        public StatusDetails StatusDetails { get; internal set; }

        /// <summary>
        /// The environment of the Amazon Pay API (live or sandbox).
        /// </summary>
        [JsonProperty(PropertyName = "releaseEnvironment")]
        public string ReleaseEnvironment { get; internal set; }
    }
}
