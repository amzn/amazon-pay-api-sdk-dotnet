using Amazon.Pay.API.InStore.Types;
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.InStore.Refund
{
    public class CreateRefundRequest : ApiRequestBody
    {
        public CreateRefundRequest(string chargeId, decimal refundAmount, Currency currencyCode, string refundReferenceId)
        {
            ChargeId = chargeId;
            RefundTotal = new Price(refundAmount, currencyCode);
            RefundReferenceId = refundReferenceId;

            Metadata = new Metadata();
        }

        /// <summary>
        /// Charge identifer.
        /// </summary>
        [JsonProperty(PropertyName = "chargeId")]
        public string ChargeId { get; internal set; }

        /// <summary>
        /// Reference ID for the refund.
        /// </summary>
        [JsonProperty(PropertyName = "refundReferenceId")]
        public string RefundReferenceId { get; internal set; }

        /// <summary>
        /// Amount to be refunded. Refund amount can be either 15% or 75 USD/GBP/EUR (whichever is less) above the captured amount.
        /// </summary>
        [JsonProperty(PropertyName = "refundTotal")]
        public Price RefundTotal { get; internal set; }

        /// <summary>
        /// Description shown on the buyer payment instrument statement.
        /// </summary>
        [JsonProperty(PropertyName = "softDescriptor")]
        public string SoftDescriptor { get; internal set; }

        /// <summary>
        /// This is a composite object. It includes merchant note, custom information, and communication context, as described below.
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public Metadata Metadata { get; internal set; }
    }
}
