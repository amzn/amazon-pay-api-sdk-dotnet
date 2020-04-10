using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Amazon.Pay.API.DeliveryTracker
{
    public class DeliveryTrackerResponse : AmazonPayResponse
    {
        /// <summary>
        /// The Amazon Order Reference ID associated with the order for which the shipments need to be tracked.
        /// </summary>
        [JsonProperty(PropertyName = "amazonOrderReferenceId")]
        public string AmazonOrderReferenceId { get; internal set; }

        /// <summary>
        /// The Charge Permission ID associated with the order for which the shipments need to be tracked.
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionId")]
        public string ChargePermissionId { get; internal set; }

        /// <summary>
        /// Delivery details of the request.
        /// </summary>
        [JsonProperty(PropertyName = "deliveryDetails")]
        public List<DeliveryDetail> DeliveryDetails { get; internal set; }
    }
}
