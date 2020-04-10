using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Charge
{
    public class CancelChargeRequest : ApiRequestBody
    {
        public CancelChargeRequest(string cancellationReason)
        {
            CancellationReason = cancellationReason;
        }

        /// <summary>
        /// Merchant-provided reason for canceling Charge.
        /// </summary>
        [JsonProperty(PropertyName = "cancellationReason")]
        public string CancellationReason { get; set; }
    }
}
