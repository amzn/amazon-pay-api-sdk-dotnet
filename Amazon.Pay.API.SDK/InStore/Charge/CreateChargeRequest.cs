using Amazon.Pay.API.InStore.Types;
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.InStore.Charge
{
    public class CreateChargeRequest : ApiRequestBody
    {
        public CreateChargeRequest(string chargePermissionId, decimal chargeTotal, Currency currencyCode, string chargeReferenceId)
        { 
            ChargePermissionId = chargePermissionId;
            ChargeTotal = new Price(chargeTotal, currencyCode);
            ChargeReferenceId = chargeReferenceId;

            Metadata = new Metadata();
        }

        /// <summary>
        /// The Amazon Pay - generated identifier for the Merchant Scan request.
        /// </summary>
        [JsonProperty(PropertyName = "chargePermissionId")]
        public string ChargePermissionId { get; set; }

        /// <summary>
        /// The identifier for this API request. This identifier, which you define, must be unique for every single API request.
        /// </summary>
        [JsonProperty(PropertyName = "chargeReferenceId")]
        public string ChargeReferenceId { get; set; }

        /// <summary>
        /// The total amount that has been captured using this Charge.
        /// </summary>
        [JsonProperty(PropertyName = "chargeTotal")]
        public Price ChargeTotal { get; internal set; }

        /// <summary>
        /// Description shown on the buyer payment instrument statement.
        /// </summary>
        [JsonProperty(PropertyName = "softDescriptor")]
        public string SoftDescriptor { get; set; }

        /// <summary>
        /// This is a composite object. It includes merchant note, custom information, and communication context, as described below.
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public Metadata Metadata { get; internal set; }

    }
}
