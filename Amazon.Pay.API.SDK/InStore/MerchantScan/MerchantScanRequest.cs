using Amazon.Pay.API.InStore.Types;
using Amazon.Pay.API.Types;
using Newtonsoft.Json;

namespace Amazon.Pay.API.InStore.MerchantScan
{
    public class MerchantScanRequest : ApiRequestBody
    {
        public MerchantScanRequest(string scanData, string scanReferenceId, Currency ledgerCurrency, string merchantCOE)
        {
            ScanData = scanData;
            ScanReferenceId = scanReferenceId;
            LedgerCurrency = ledgerCurrency;
            MerchantCOE = merchantCOE;
        }

        public MerchantScanRequest(string scanData, string scanReferenceId, Currency ledgerCurrency, string merchantCOE, string countryCode)
            : this(scanData, scanReferenceId, ledgerCurrency, merchantCOE)
        {
            StoreLocation = new StoreLocation(countryCode);
            Metadata = new Metadata();
        }

        /// <summary>
        /// Buyer scan data that is scanned by a store associate.
        /// </summary>
        [JsonProperty(PropertyName = "scanData")]
        public string ScanData { get; set; }

        /// <summary>
        /// The identifier for this API request, which you define and which must be unique for every single API request.
        /// </summary>
        [JsonProperty(PropertyName = "scanReferenceId")]
        public string ScanReferenceId { get; set; }

        /// <summary>
        /// Merchant settlement currency.
        /// </summary>
        [JsonProperty(PropertyName = "ledgerCurrency")]
        public Currency LedgerCurrency { get; set; }

        /// <summary>
        /// Merchant country of establishment.
        /// </summary>
        [JsonProperty(PropertyName = "merchantCOE")]
        public string MerchantCOE { get; set; }

        /// <summary>
        /// Information on the store location.
        /// </summary>
        [JsonProperty(PropertyName = "storeLocation")]
        public StoreLocation StoreLocation { get; internal set; }

        /// <summary>
        /// This is a composite object. It includes merchant note, custom information, and communication context, as described below.
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public Metadata Metadata { get; internal set; }
    }
}
