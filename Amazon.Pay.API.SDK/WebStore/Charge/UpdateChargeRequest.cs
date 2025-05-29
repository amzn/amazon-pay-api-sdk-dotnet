using System;
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Charge
{
    public class UpdateChargeRequest : ApiRequestBody
    {
        public UpdateChargeRequest(string state,string reasonCode, string reasonDescription = null, DateTime? lastUpdatedTimestamp = null)
        {
            StatusDetails = new StatusDetailsRequest
            {
                State = state,
                ReasonCode = reasonCode,
                ReasonDescription = reasonDescription,
                LastUpdatedTimestamp = lastUpdatedTimestamp
            };
        }

        /// <summary>
        /// charge status of any PPM transactions
        /// </summary>
        [JsonProperty(PropertyName = "statusDetails")]
        public StatusDetailsRequest StatusDetails { get; set; }
    }

}
