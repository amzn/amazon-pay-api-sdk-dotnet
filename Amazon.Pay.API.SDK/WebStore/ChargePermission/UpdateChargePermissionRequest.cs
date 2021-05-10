using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.WebStore.ChargePermission
{
    public class UpdateChargePermissionRequest : ApiRequestBody
    {
        public UpdateChargePermissionRequest()
        {
            MerchantMetadata = new MerchantMetadata();
            RecurringMetadata = new RecurringMetadata();
        }

        /// <summary>
        /// Merchant-provided order info.
        /// </summary>
        [JsonProperty(PropertyName = "merchantMetadata")]
        public MerchantMetadata MerchantMetadata { get; internal set; }

        /// <summary>
        /// Metadata about how the recurring Charge Permission will be used.
        /// </summary>
        [JsonProperty(PropertyName = "recurringMetadata")]
        public RecurringMetadata RecurringMetadata { get; internal set; }

    }
}
