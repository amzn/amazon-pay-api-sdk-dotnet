using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using System;

namespace Amazon.Pay.API.InStore.MerchantScan
{
    public class MerchantScanResponse : AmazonPayResponse
    {
        /// <summary>
        /// The Amazon Pay - generated identifier for the API request.
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionId")]
        public string ChargePermissionId { get; set; }
    }
}
