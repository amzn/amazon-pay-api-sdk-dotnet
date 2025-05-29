using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.Converters;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.Dispute
{
    public class CreateDisputeRequest : ApiRequestBody
    {
        public CreateDisputeRequest(string chargeId, decimal chargeAmount, Currency currencyCode)
        {
            ChargeId = chargeId;
            DisputeAmount = new Price(chargeAmount, currencyCode);

        }

        public CreateDisputeRequest(
            string chargeId,
            decimal chargeAmount,
            Currency currencyCode,
            string filingReason,
            DateTime filingTimestamp,
            StatusDetailsRequest statusDetails,
            DateTime merchantResponseDeadline,
            ProviderMetadata providerMetadata)
        {
            ChargeId = chargeId;
            DisputeAmount = new Price(chargeAmount, currencyCode);
            FilingReason = filingReason;
            FilingTimestamp = filingTimestamp;
            StatusDetails = statusDetails;
            MerchantResponseDeadline = merchantResponseDeadline;
            ProviderMetadata = providerMetadata;
        }

        /// <summary>
        /// Charge identifier.
        /// </summary>
        [JsonProperty(PropertyName = "chargeId")]
        public string ChargeId { get; set; }

        /// <summary>
        /// Dispute amount and currency code
        /// </summary>
        [JsonProperty(PropertyName = "disputeAmount")]
        public Price DisputeAmount { get; set; }

        /// <summary>
        /// Reason for which the dispute has been filed
        /// </summary>
        [JsonProperty(PropertyName = "filingReason")]
        public string FilingReason { get; set; }

        /// <summary>
        /// Time the dispute was filed
        /// </summary>
        [JsonProperty(PropertyName = "filingTimestamp")]
        [JsonConverter(typeof(AmazonDateTimeConverter))]
        public DateTime FilingTimestamp { get; set; }

        /// <summary>
        /// Status detail of the dispute object including latest Dispute state and corresponding reason.
        /// </summary>
        [JsonProperty(PropertyName = "statusDetails")]
        public StatusDetailsRequest StatusDetails { get; set; }

        /// <summary>
        /// Time window by which merchant should respond to a dispute request, otherwise dispute will be resolved in buyer's favour.
        /// </summary>
        [JsonProperty(PropertyName = "merchantResponseDeadline")]
        [JsonConverter(typeof(AmazonDateTimeConverter))]
        public DateTime MerchantResponseDeadline { get; set; }

        /// <summary>
        /// Partners details containing attributes like partner Dispute identifier
        /// </summary>
        [JsonProperty(PropertyName = "providerMetadata")]
        public ProviderMetadata ProviderMetadata { get; set; }
    }

}