using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class ProviderMetadata
    {
        /// <summary>
        /// Payment service provider (PSP)-provided order identifier.
        /// </summary>
        [JsonProperty(PropertyName = "providerReferenceId")]
        public string ProviderReferenceId { get; set; }

    }
}
