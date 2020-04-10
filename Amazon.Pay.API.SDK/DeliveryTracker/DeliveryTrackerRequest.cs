using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Amazon.Pay.API.DeliveryTracker
{
    public class DeliveryTrackerRequest : ApiRequestBody
    {
        public DeliveryTrackerRequest(string objectId, bool objectIsChargePermission, string trackingNumber, string carrierCode)
        {
            if (objectIsChargePermission)
            {
                ChargePermissionId = objectId;
            }
            else
            {
                AmazonOrderReferenceId = objectId;
            }

            DeliveryDetails = new List<DeliveryDetail>();
            DeliveryDetails.Add(new DeliveryDetail()
            {
                TrackingNumber = trackingNumber,
                CarrierCode = carrierCode
            });
        }

        /// <summary>
        /// The Amazon Order Reference ID associated with the order for which the shipments need to be tracked.
        /// </summary>
        [JsonProperty(PropertyName = "amazonOrderReferenceId")]
        public string AmazonOrderReferenceId { get; set; }

        /// <summary>
        /// The Charge Permission ID associated with the order for which the shipments need to be tracked.
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionId")]
        public string ChargePermissionId { get; set; }

        /// <summary>
        /// Delivery details of the request.
        /// </summary>
        [JsonProperty(PropertyName = "deliveryDetails")]
        public List<DeliveryDetail> DeliveryDetails { get; internal set; }
    }
}
