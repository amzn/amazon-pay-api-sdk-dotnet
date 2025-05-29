using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class ProviderMetadata
    {
        /// <summary>
        /// Payment service provider (PSP)-provided order identifier.
        /// </summary>
        [JsonProperty(PropertyName = "providerReferenceId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProviderReferenceId { get; set; }

        /// <summary>
        /// Payment service provider (PSP)-provided dispute identifier.
        /// </summary>
        [JsonProperty(PropertyName = "providerDisputeId", NullValueHandling = NullValueHandling.Ignore)]
        public string ProviderDisputeId { get; set; }
    }
}
