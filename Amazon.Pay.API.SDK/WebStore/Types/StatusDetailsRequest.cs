using Amazon.Pay.API.Types;
using Amazon.Pay.API.Converters;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.Types
{
    public class StatusDetailsRequest
    {
        /// <summary>
        /// Current object state.
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// Reason code for current state.
        /// </summary>
        [JsonProperty(PropertyName = "reasonCode")]
        public string ReasonCode { get; set; }

        /// <summary>
        /// An optional description of the state.
        /// </summary>
        [JsonProperty(PropertyName = "reasonDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ReasonDescription { get; set; }

        /// <summary>
        /// State describing the outcome resolution of the dispute
        /// </summary>
        [JsonProperty(PropertyName = "resolution", NullValueHandling = NullValueHandling.Ignore)]
        public string Resolution { get; set; }

        /// <summary>
        /// UTC date and time when the state was last updated in ISO 8601 format.
        /// </summary>
        [JsonProperty(PropertyName = "lastUpdatedTimestamp", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(AmazonDateTimeConverter))]
        public DateTime? LastUpdatedTimestamp { get; set; }
    }
}
