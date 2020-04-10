using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Charge
{
    public class CaptureChargeRequest : ApiRequestBody
    {
        public CaptureChargeRequest(decimal amount, Currency currencyCode)
        {
            CaptureAmount = new Price(amount, currencyCode);
        }

        /// <summary>
        /// Amount to capture.
        /// </summary>
        [JsonProperty(PropertyName = "captureAmount")]
        public Price CaptureAmount { get; internal set; }

        /// <summary>
        /// Description shown on the buyer's payment instrument statement..
        /// </summary>
        [JsonProperty(PropertyName = "softDescriptor")]
        public string SoftDescriptor { get; set; }
    }
}
