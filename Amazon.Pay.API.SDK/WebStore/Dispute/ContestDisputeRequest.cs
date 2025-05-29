using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.Dispute
{
    public class ContestDisputeRequest : ApiRequestBody
    {
        public ContestDisputeRequest(List<MerchantEvidence> merchantEvidences)
        {
            MerchantEvidences = merchantEvidences;
        }

        /// <summary>
        /// Evidences provided by the merchant to contest the dispute.
        /// </summary>
        [JsonProperty(PropertyName = "merchantEvidences")]
        public List<MerchantEvidence> MerchantEvidences { get; set; }
    }
}