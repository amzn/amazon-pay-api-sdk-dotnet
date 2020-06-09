using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.ChargePermission
{
    public class CloseChargePermissionRequest : ApiRequestBody
    {
        // TODO: add constructor to enforce provision of ClosureReason

        /// <summary>
        /// Merchant-provided reason for closing Charge Permission.
        /// </summary>
        [JsonProperty(PropertyName = "closureReason")]
        public string ClosureReason { get; set; }

        /// <summary>
        /// Cancels pending charges.
        /// </summary>
        [JsonProperty(PropertyName = "cancelPendingCharges")]
        public bool CancelPendingCharges { get; set; }
    }
}
