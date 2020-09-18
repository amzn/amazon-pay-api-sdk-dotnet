using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.WebStore.Types
{
    public class ChargePermissionStatusDetails
    {
        /// <summary>
        /// Current object state.
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; internal set; }

        /// <summary>
        /// List of reasons for current state
        /// </summary>
        [JsonProperty(PropertyName = "reasons")]
        public List<Reason> Reasons { get; internal set; }

        /// <summary>
        /// UTC date and time when the state was last updated in ISO 8601 format.
        /// </summary>
        [JsonProperty(PropertyName = "lastUpdatedTimestamp")]
        public DateTime LastUpdatedTimestamp { get; internal set; }
    }
}
