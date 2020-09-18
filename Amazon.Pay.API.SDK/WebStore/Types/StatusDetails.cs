using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.Types
{
    public class StatusDetails
    {
        /// <summary>
        /// Current object state.
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; internal set; }

        /// <summary>
        /// Reason code for current state.
        /// </summary>
        [JsonProperty(PropertyName = "reasonCode")]
        public string ReasonCode { get; internal set; }

        /// <summary>
        /// An optional description of the Checkout Session state.
        /// </summary>
        [JsonProperty(PropertyName = "reasonDescription")]
        public string ReasonDescription { get; internal set; }

        /// <summary>
        /// UTC date and time when the state was last updated in ISO 8601 format.
        /// </summary>
        [JsonProperty(PropertyName = "lastUpdatedTimestamp")]
        public DateTime LastUpdatedTimestamp { get; internal set; }
    }
}
