using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.Converters;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.Dispute
{
    public class UpdateDisputeRequest : ApiRequestBody
    {
        public UpdateDisputeRequest(string state, string reasonCode, DateTime closureTimestamp, string reasonDescription = null, string resolution = null, DateTime? lastUpdatedTimestamp = null)
        {
            StatusDetails = new StatusDetailsRequest
            {
                State = state,
                ReasonCode = reasonCode,
                ReasonDescription = reasonDescription,
                Resolution = resolution,
                LastUpdatedTimestamp = lastUpdatedTimestamp
            };
            ClosureTimestamp = closureTimestamp;
        }

        /// <summary>
        /// Status detail of the dispute object including latest Dispute state and corresponding reason
        /// </summary>
        [JsonProperty(PropertyName = "statusDetails")]
        public StatusDetailsRequest StatusDetails { get; set; }

        /// <summary>
        /// This field is required when calling Update API for update reason – Close. This represents timestamp when the dispute was closed.
        /// </summary>
        [JsonProperty(PropertyName = "closureTimestamp")]
        [JsonConverter(typeof(AmazonDateTimeConverter))]
        public DateTime ClosureTimestamp { get; set; }
    }
}