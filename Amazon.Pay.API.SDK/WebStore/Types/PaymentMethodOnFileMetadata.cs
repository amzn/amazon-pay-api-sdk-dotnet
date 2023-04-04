using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.Types
{
    public class PaymentMethodOnFileMetadata
    {
        /// <summary>
        /// Whether or not to trigger only setup flow to setup payment method on file.
        /// </summary>
        [JsonProperty(PropertyName = "setupOnly")]
        public bool? SetupOnly { get; set; }
    }
}
