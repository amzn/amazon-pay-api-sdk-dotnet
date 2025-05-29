using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.Dispute
{
    public class DisputeResponse : AmazonPayResponse
    {

        /// <summary>
        /// Amazon Pay’s Dispute object identifier.
        /// </summary>
        [JsonProperty(PropertyName = "disputeId")]
        public string DisputeId { get; internal set; }

        /// <summary>
        /// Charge Identifier for which the dispute is created..
        /// </summary>
        [JsonProperty(PropertyName = "chargeId")]
        public string ChargeId { get; internal set; }

        /// <summary>
        /// The total amount that has been disputed.
        /// </summary>
        [JsonProperty(PropertyName = "disputeAmount")]
        public Price DisputeAmount { get; internal set; }

        /// <summary>
        /// Type of the dispute
        /// </summary>
        [JsonProperty(PropertyName = "disputeType")]
        public string DisputeType { get; internal set; }

        /// <summary>
        /// The reason for which the dispute has been filed.
        /// </summary>
        [JsonProperty(PropertyName = "filingReason")]
        public string FilingReason { get; internal set; }

        /// <summary>
        /// The party responsible for resolving the dispute.
        /// </summary>
        [JsonProperty(PropertyName = "resolutionAuthority")]
        public string ResolutionAuthority { get; internal set; }

        /// <summary>
        /// Time window by which merchant should respond to a dispute request, otherwise dispute will be resolved in buyer's favor.
        /// </summary>
        [JsonProperty(PropertyName = "merchantResponseDeadline")]
        public DateTime MerchantResponseDeadline { get; internal set; }

        /// <summary>
        /// Status detail of the dispute object including latest Dispute state, resolution state and corresponding reason of the resolution.
        /// </summary>
        [JsonProperty(PropertyName = "statusDetails")]
        public StatusDetails StatusDetails { get; internal set; }

        /// <summary>
        /// Dispute details provided by the provider (PSP)
        /// </summary>
        [JsonProperty(PropertyName = "providerMetadata", NullValueHandling = NullValueHandling.Ignore)]
        public ProviderMetadata ProviderMetadata { get; internal set; }

        /// <summary>
        /// The environment of the Amazon Pay API (live or sandbox).
        /// </summary>
        [JsonProperty(PropertyName = "releaseEnvironment")]
        public string ReleaseEnvironment { get; internal set; }

        /// <summary>
        /// Collection of merchants provided evidence
        /// </summary>
        [JsonProperty(PropertyName = "merchantEvidences", NullValueHandling = NullValueHandling.Ignore)]
        public List<MerchantEvidence> MerchantEvidences { get; internal set; }
    }

}