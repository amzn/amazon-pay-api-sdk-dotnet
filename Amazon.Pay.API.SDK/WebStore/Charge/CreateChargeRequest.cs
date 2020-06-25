using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Amazon.Pay.API.WebStore.Charge
{
    public class CreateChargeRequest : ApiRequestBody
    {
        public CreateChargeRequest(string chargePermissionId, decimal chargeAmount, Currency currencyCode)
        {
            ProviderMetadata = new ProviderMetadata();

            ChargePermissionId = chargePermissionId;
            ChargeAmount = new Price(chargeAmount, currencyCode);
        }

        [OnSerializing]
        internal void OnSerializing(StreamingContext content)
        {
            // skip 'providerMetadata' if there was no data provided
            if (string.IsNullOrEmpty(ProviderMetadata.ProviderReferenceId))
            {
                ProviderMetadata = null;
            }
        }

        /// <summary>
        /// Charge Permission identifier.
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionId")]
        public string ChargePermissionId { get; set; }

        /// <summary>
        /// The total amount that has been captured using this Charge.
        /// </summary>
        [JsonProperty(PropertyName = "chargeAmount")]
        public Price ChargeAmount { get; internal set; }

        /// <summary>
        /// Boolean that indicates whether or not Charge should be captured immediately after a successful authorization.
        /// </summary>
        [JsonProperty(PropertyName = "captureNow")]
        public bool? CaptureNow { get; set; }

        /// <summary>
        /// Description shown on the buyer payment instrument statement, if CaptureNow is set to true. Do not set this value if CaptureNow is set to false.
        /// </summary>
        [JsonProperty(PropertyName = "softDescriptor")]
        public string SoftDescriptor { get; set; }

        /// <summary>
        /// Boolean that indicates whether merchant can handle pending response.
        /// </summary>
        [JsonProperty(PropertyName = "canHandlePendingAuthorization")]
        public bool? CanHandlePendingAuthorization { get; set; }

        /// <summary>
        /// Payment service provider (PSP)-provided order information.
        /// </summary>
        [JsonProperty(PropertyName = "providerMetadata")]
        public ProviderMetadata ProviderMetadata { get; internal set; }
    }
}
