using Newtonsoft.Json;

namespace Amazon.Pay.API.InStore.Types
{
    public class CommunicationContext
    {
        /// <summary>
        /// The identifier of the store from which the purchase is initiated.
        /// </summary>
        [JsonProperty(PropertyName = "merchantStoreName")]
        public string MerchantStoreName { get; set; }

        /// <summary>
        /// The merchant-specified identifier of this order.
        /// </summary>
        [JsonProperty(PropertyName = "merchantOrderId")]
        public string MerchantOrderId { get; set; }
    }
}