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

        /// <summary>
        /// Billing address for buyer-selected payment instrument. Billing address is only available in EU or for PayOnly product type.
        /// </summary>
        [JsonProperty(PropertyName = "billingAddress")]
        public Address BillingAddress { get; internal set; }
    }
}
