using Newtonsoft.Json;

namespace Amazon.Pay.API.InStore.Types
{
    public class Metadata
    {
        public Metadata()
        {
            CommunicationContext = new CommunicationContext();
        }

        /// <summary>
        /// Any custom note that the merchant wants to add for the purchase.
        /// </summary>
        [JsonProperty(PropertyName = "merchantNote")]
        public string MerchantNote { get; set; }

        /// <summary>
        /// Any additional information that you want to include with this order reference.
        /// </summary>
        [JsonProperty(PropertyName = "customInformation")]
        public string CustomInformation { get; set; }

        /// <summary>
        /// Includes merchant store name and order Id.
        /// </summary>
        [JsonProperty(PropertyName = "communicationContext")]
        public CommunicationContext CommunicationContext { get; set; }
    }
}