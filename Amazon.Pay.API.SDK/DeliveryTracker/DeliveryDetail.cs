using Newtonsoft.Json;

namespace Amazon.Pay.API.DeliveryTracker
{
    public class DeliveryDetail
    {
        /// <summary>
        /// The tracking number for the shipment provided by the shipping company.
        /// </summary>
        [JsonProperty(PropertyName = "trackingNumber")]
        public string TrackingNumber { get; set; }

        /// <summary>
        /// The shipping company code used for delivering goods to the customer.
        /// </summary>
        [JsonProperty(PropertyName = "carrierCode")]
        public string CarrierCode { get; set; }
    }
}
