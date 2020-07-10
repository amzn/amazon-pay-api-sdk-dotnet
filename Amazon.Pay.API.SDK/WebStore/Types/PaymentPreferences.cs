using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class PaymentPreferences
    {
        /// <summary>
        /// Amazon Pay-provided description for buyer-selected payment instrument.
        /// </summary>
        [JsonProperty(PropertyName = "paymentDescriptor")]
        public string PaymentDescriptor { get; internal set; }
    }
}
