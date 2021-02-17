using System.Collections.Generic;

namespace Amazon.Pay.API
{
    public class Constants
    {
        public const string SdkVersion = "2.4.4.0";
        public const string SdkName    = "amazon-pay-api-sdk-dotnet";
        public const int    ApiVersion = 2;

        public const string AmazonSignatureAlgorithm = "AMZN-PAY-RSASSA-PSS";

        public static readonly Dictionary<string, int> serviceErrors = new Dictionary<string, int>() {
             {"Internal Server Error", 500},
             {"Service Unavailable", 503},
             {"Too Many Requests", 429}
         };

        public class Headers
        {
            public const string IdempotencyKey = "x-amz-pay-idempotency-key";
            public const string RequestId = "x-amz-pay-request-id";
            public const string AuthToken = "x-amz-pay-authtoken";
            public const string Region = "x-amz-pay-region";
            public const string Date = "x-amz-pay-date";
            public const string Host = "x-amz-pay-host";
        }

        public class ApiServices
        {
            public const string Default = ""; // on purpose (default is the 'root' API)
            public const string InStore = "in-store";
        }

        public class Resources
        {
            public class WebStore
            {
                public const string CheckoutSessions = "checkoutSessions";
                public const string ChargePermissions = "chargePermissions";
                public const string Charges = "charges";
                public const string Refunds = "refunds";
                public const string Buyer = "buyers";
            }

            public class InStore
            {
                public const string MerchantScan = "merchantScan";
                public const string Refund = "refund";
                public const string Charge = "charge";
            }

            public const string DeliveryTracker = "deliveryTrackers";
            public const string TokenExchange = "authorizationTokens";
        }

        public class Methods
        {
            internal const string Close = "close";
            internal const string Capture = "capture";
            internal const string Delete = "delete";
            internal const string Cancel = "cancel";
            internal const string Complete = "complete";
        }
    }
}
