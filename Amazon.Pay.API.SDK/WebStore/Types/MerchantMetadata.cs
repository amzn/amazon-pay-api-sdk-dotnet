using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.WebStore.Types
{
    public class MerchantMetadata
    {
        /// <summary>
        /// External merchant order identifer.
        /// </summary>
        [JsonProperty(PropertyName = "merchantReferenceId")]
        public string MerchantReferenceId { get; set; }

        /// <summary>
        /// Merchant store name.
        /// </summary>
        [JsonProperty(PropertyName = "merchantStoreName")]
        public string MerchantStoreName { get; set; }

        /// <summary>
        /// Description of the order that is shared in buyer communication.
        /// </summary>
        [JsonProperty(PropertyName = "noteToBuyer")]
        public string NoteToBuyer { get; set; }

        /// <summary>
        /// Custom info for the order. This data is not shared in any buyer communication.
        /// </summary>
        [JsonProperty(PropertyName = "customInformation")]
        public string CustomInformation { get; set; }
    }
}
