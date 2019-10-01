using AmazonPayV2.types;
using System.Collections.Generic;

namespace AmazonPayV2
{
    public class Constants
    {
        public static readonly string SDKClientVersion = "4.2.1";
        public static readonly string GithubSDKName = "amazon-pay-sdk-v2-dotnet";
        public static readonly string AmazonSignatureAlgorithm = "AMZN-PAY-RSASSA-PSS";
        public static readonly string AmazonPayAPIVersionInStore = "v1";
        public static readonly string AmazonPayAPIVersionWebStore = "v1";
        public static readonly string AmazonPayAPIVersionDeliveryTrackers = "v1";
        public static readonly string AmazonPayAPIPathInStore = "in-store/";
        public static readonly string AmazonPayAPIPathWebStore = "";
        public static readonly string AmazonPayAPIPathDeliveryTrackers = "";

        public static readonly string PrivateKey = "Private key";
        public static readonly string Region = "Region";
        public static readonly string PublicKeyId = "Public key id";

        public static readonly string MerchantScan = "merchantScan";
        public static readonly string Refund = "refund";
        public static readonly string Charge = "charge";
        public static readonly string DeliveryTrackers = "deliveryTrackers";
        public static readonly string CheckoutSessions = "checkoutSessions";
        public static readonly string ChargePermissions = "chargePermissions";
        public static readonly string Charges = "charges";
        public static readonly string Refunds = "refunds";

        public static readonly string ScanData = "scanData";
        public static readonly string BadgePayPrefix = "AMZ-EMP-";
        public static readonly int ScanDataStartIndex = 0;
        public static readonly int ScanDataUpdatedStartIndex = 1;

        public static readonly Dictionary<string, string> endpointMappings = new Dictionary<string, string>() {
             {Regions.eu.ToString(), "https://pay-api.amazon.eu"},
             {Regions.na.ToString(), "https://pay-api.amazon.com"},
             {Regions.jp.ToString(), "https://pay-api.amazon.jp"},
             {Regions.service.ToString(), "" }
         };

        public static readonly Dictionary<string, int> serviceErrors = new Dictionary<string, int>() {
             {"Internal Server Error", 500},
             {"Service Unavailable", 503},
             {"Too Many Requests", 429}
         };
    }
}