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
        /// <remarks>
        /// Download carrier code list here: https://s3-us-west-2.amazonaws.com/tcordov/amazon-pay-delivery-tracker-supported-carriers.csv
        /// </remarks>
        [JsonProperty(PropertyName = "carrierCode")]
        public string CarrierCode { get; set; }
    }
}
