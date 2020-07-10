using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.ChargePermission
{
    public class CloseChargePermissionRequest : ApiRequestBody
    {
        public CloseChargePermissionRequest(string closureReason)
        {
            ClosureReason = closureReason;
        }

        [Obsolete("Use constructor with closureReason instead")]
        public CloseChargePermissionRequest()
        {
        }

        /// <summary>
        /// Merchant-provided reason for closing Charge Permission.
        /// </summary>
        [JsonProperty(PropertyName = "closureReason")]
        public string ClosureReason { get; set; }

        /// <summary>
        /// Cancels pending charges.
        /// </summary>
        /// <remarks>
        /// Default: false.
        /// </remarks>
        [JsonProperty(PropertyName = "cancelPendingCharges")]
        public bool? CancelPendingCharges { get; set; }
    }
}
