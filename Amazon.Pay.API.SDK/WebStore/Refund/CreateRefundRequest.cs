using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Refund
{
    public class CreateRefundRequest : ApiRequestBody
    {
        public CreateRefundRequest(string chargeId, decimal refundAmount, Currency currencyCode)
        {
            ChargeId = chargeId;
            RefundAmount = new Price(refundAmount, currencyCode);
        }

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
        public string SoftDescriptor { get; set; }
    }
}
