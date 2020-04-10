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
        }

        /// <summary>
        /// Merchant-provided order info.
        /// </summary>
        [JsonProperty(PropertyName = "merchantMetadata")]
        public MerchantMetadata MerchantMetadata { get; internal set; }
    }
}
