# Amazon Pay SDK-V2 (.Net)
Amazon Pay API V2 Integration

Please note the Amazon Pay V2 SDK can only be used for V2-specific API calls (e.g., Alexa Delivery Trackers, In-Store API, API V2, etc.)

If you are developing an integration using the original [Amazon Pay API Reference Guide](https://developer.amazon.com/docs/amazon-pay-api/intro.html), then you will need to use the original [Amazon Pay SDK (.Net)](https://github.com/amzn/amazon-pay-sdk-csharp).

## Requirements

* Build targets .Net Core 2.0 and .Net Framework 3.5
* Newtonsoft.JSON 12.0.2
* BouncyCastle.NetCore 1.8.5

## SDK Installation

.Net SDK is available on Nuget

## Public and Private Keys

MWS access keys, MWS secret keys, and MWS authorization tokens from previous MWS or Amazon Pay V1 integrations cannot be used with this SDK.

You will need to generate your own public/private key pair to make API calls with this SDK.  This can be done using openssl commands:

```
openssl genrsa -out private.txt 2048
openssl rsa -in private.txt -pubout > public.txt
```

The first command above generates a private key and the second line uses the private key to generate a public key.

To associate the key with your account, send an email to amazon-pay-delivery-notifications@amazon.com that includes (1) your *public* key and (2) your Merchant ID.  Do not send your private key to Amazon (or anyone else) under any circumstance!

In your Seller Central account, within 1-2 business days, the account administrator will receive a message that includes the public_key_id you will need to use the SDK.

## Namespace

Namespace for this package is AmazonPayV2 so that there are no conflicts with the original Amazon Pay MWS SDK's that use the AmazonPay namespace.

## Configuration Array

```csharp
private static readonly PayConfiguration config = new PayConfiguration
{
    Region = Regions.na,
    Environment = Environments.sandbox,
    PublicKeyId = "public_key_id",
    PrivateKey = "private_key_text"
};

```

# Convenience Functions (Overview)

Make use of the built-in convenience functions to easily make API calls.  Scroll down further to see example code snippets.

When using the convenience functions, the request payload will be signed using the provided private key, and a HTTPS request is made to the correct regional endpoint.
In the event of request throttling, the HTTPS call will be attempted up to three times using an exponential backoff approach.

## Alexa Delivery Trackers API
Please note that your merchant account must be whitelisted to use the [Delivery Trackers API](https://developer.amazon.com/docs/amazon-pay-onetime/delivery-order-notifications.html).  This method is available in the base client ("AmazonPayClient") and is available in both the "WebStoreClient" and the "InStoreClient"

* **deliveryTrackers**(payload, headers = null) &#8594; POST to "v1/deliveryTrackers"

## API V2
The headers argument is not optional for create/POST calls below because it requires, at a minimum, the x-amz-pay-idempotency-key header.  These methods are available in the WebStoreClient

### API V2 CheckoutSession object
* **CreateCheckoutSession**(JObject payload, dictionary<string, string> headers) &#8594; POST to "v1/checkoutSessions"
* **GetCheckoutSession**(string checkoutSessionId, dictionary<string, string> headers = null) &#8594; GET to "v1/checkoutSessions/[checkoutSessionId]"
* **UpdateCheckoutSession**(string checkoutSessionId, JObject updateObject, dictionary<string, string> headers = null) &#8594; PATCH to "v1/checkoutSessions/[checkoutSessionId]"

### API V2 ChargePermission object
* **getChargePermission**(string chargePermissionId, dictionary<string, string> headers = null) &#8594; GET to "v1/chargePermissions/[chargePermissionId]"
* **updateChargePermission**(string chargePermissionId, JObject updateObject, $dictionary<string, string> headers = null) &#8594; PATCH to "v1/chargePermissions/[chargePermissionId]"
* **closeChargePermission**(string chargePermissionId, JObject closeRequest, dictionary<string, string> headers = null) &#8594; DELETE to "v1/chargePermissions/[chargePermissionId]/close"

### Charge object
* **createCharge**(JObject createChargeRequest, Dictionary<string, string> headers = null) &#8594; POST to "v1/charges"
* **getCharge**(String chargeId, Dictionary<string, string> headers = null) &#8594; GET to "v1/charges/$chargeId"
* **captureCharge**(string chargeId, JObject captureRequest, Dictionary<string, string> headers = null) &#8594; POST to "v1/charges/$chargeId/capture"
* **cancelCharge**(String chargeId, JObject cancelRequest, Dictionary<string, string> headers = null) &#8594; DELETE to "v1/charges/$chargeId/cancel"

### API V2 Refund object
* **createRefund**(JObject refundRequest, Dictionary<string, string> headers = null) &#8594; POST to "v1/refunds"
* **getRefund**(String refundId, Dictionary<string, string> headers = null) &#8594; GET to "v1/refunds/[refundId]"

## In-Store API
Please contact your Amazon Pay Account Manager before using the In-Store API calls in a Production environment to obtain a copy of the In-Store Integration Guide.  These methods are available in the InStoreClient

* **merchantScan**(JObject scanRequest, Dictionary<string, string> headers = null) &#8594; POST to "in-store/v1/merchantScan"
* **charge**(JObject chargeRequest, Dictionary<string, string> headers = null) &#8594; POST to "in-store/v1/charge"
* **refund**(JObject refundRequest, Dictionary<string, string> headers = null) &#8594; POST to "in-store/v1/refund"


# Using Convenience Functions

Four quick steps are needed to make an API call:

Step 1. Construct a Client (using the previously defined Config Array).

```csharp
AmazonPayClient testPayClient = new AmazonPayClient(config);
// -or-
WebStoreClient testPayClient = new WebStoreClient(config);
// -or-
InStoreClient testPayClient = new InStoreClient(config);
```

Step 2. Generate the payload.

```csharp
string jsonRequest = JObject.Parse("{""scanData"":""UKhrmatMeKdlfY6b"",""scanReferenceId"":""0b8fb271-2ae2-49a5-b35d7"",""merchantCOE"":""US"",""ledgerCurrency"":""USD"",""chargeTotal"":{""currencyCode"":""USD"",""amount"":""2.00""},""metadata"":{""merchantNote"":""Merchant Name"",""communicationContext"":{""merchantStoreName"":""Store Name"",""merchantOrderId"":""789123""}}}");
```

Step 3. Execute the call.

```csharp
AmazonPayResponse requestResult = testPayClient.InstoreMerchantScan(jsonRequest);
```

Step 4. Check the result.

requestResult will be an object with the following properties:

* '**Status**' - integer HTTP status code (200, 201, etc.)
* '**Response**' - the JObject response body
* '**RawResponse**' - the JSON response body as text
* '**RequestId**' - the Request ID from Amazon API gateway
* '**Url**' - the URL for the REST call the SDK calls, for troubleshooting purposes
* '**Method** - POST, GET, PATCH, or DELETE
* '**Headers**' - an array containing the various headers generated by the SDK, for troubleshooting purposes
* '**RawRequest**' - the JSON request payload
* '**Retries**' - usually 0, but reflects the number of times a request was retried due to throttling or other server-side issue
* '**Duration**' - duration in milliseconds of SDK function call
* '**Success**' - true/false indication of HTTP Status

The first two items (status, response) are critical.  The remaining items are useful in troubleshooting situations.

If you are a Solution Provider and need to make an API call on behalf of a different merchant account, you will need to pass along an extra authentication token parameter into the API call.

```csharp
Dictionary<String, String> testHeaders = new Dictionary<String, String>()
{
    {"x-amz-pay-authtoken", 'other_merchant_super_secret_token'}
};
AmazonPayResponse requestResult = testPayClient.InstoreMerchantScan(jsonRequest, testHeaders);
```

# Convenience Functions Code Samples

## Alexa Delivery Notifications

```csharp
private static readonly PayConfiguration config = new PayConfiguration
{
    Region = Regions.na,
    Environment = Environments.sandbox,
    PublicKeyId = "public_key_id",
    PrivateKey = "private_key_text"
}

JObject testRequest = JObject.Parse(@"{
    ""amazonOrderReferenceId"" : ""P00-0000000-0000000"",
    ""deliveryDetails"" : [{
        ""trackingNumber"" : ""1Z999AA10123456789"",
        ""carrierCode"" : ""UPS""
      }]
}");

AmazonClient testPayClient = new AmazonClient(config);
AmazonPayResponse testResponse = testPayClient.deliveryTrackers(testRequest);
if (testResponse.success)
{
    // success
}
else
{
    // check testResponse.status and investigate testResponse.response
}
```

## API V2 - Create Checkout Session

```csharp
using System;
using AmazonPayV2.types;
using AmazonPayV2;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace getCheckoutSessionId
{
    public partial class index : System.Web.UI.Page
    {
        private static readonly PayConfiguration config = new PayConfiguration
        {
            Region = Regions.na,
            Environment = Environments.sandbox,
            PublicKeyId = "public_key_id",
            PrivateKey = "private_key_text"
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JObject testRequest = JObject.Parse(@"{ 
                ""webCheckoutDetails"": { 
                    ""checkoutReviewReturnUrl"" : ""https://example.com/updateCheckoutSession.aspx""
                },
                ""storeId"" : ""store_id""
            }");
            String iKey = Guid.NewGuid().ToString().Replace("-", "");
            Dictionary<String, String> headers = new Dictionary<String, String>()
            {
                {"x-amz-pay-Idempotency-Key", iKey}
            };
            WebStoreClient testPayClient = new WebStoreClient(config);
            AmazonPayResponse testCreateResponse = testPayClient.CreateCheckoutSession(testRequest, headers);

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(testCreateResponse.RawResponse);
            Response.End();
        }
    }
}
```

## API V2 - Get Checkout Session

```csharp
private static readonly PayConfiguration config = new PayConfiguration
{
    Region = Regions.na,
    Environment = Environments.sandbox,
    PublicKeyId = "public_key_id",
    PrivateKey = "private_key_text"
}

WebStoreClient testPayClient = new WebStoreClient(config);
String iKey = Guid.NewGuid().ToString().Replace("-", "");
Dictionary<String, String> headers = new Dictionary<String, String>()
{
    {"x-amz-pay-Idempotency-Key", iKey}
};

String checkoutSessionId = Request.QueryString["amazonCheckoutSessionId"];

AmazonPayResponse checkoutSessionResponse =   testPayClient.GetCheckoutSession(checkoutSessionId, headers);

```

## API V2 - Update Checkout Session

```csharp
private static readonly PayConfiguration config = new PayConfiguration
{
    Region = Regions.na,
    Environment = Environments.sandbox,
    PublicKeyId = "public_key_id",
    PrivateKey = "private_key_text"
}
string jsonRequestString = @"{
    ""webCheckoutDetails"":
    {
        ""checkoutResultReturnUrl"":""https://10.1.118.125/resultReturn.aspx""
    },
    ""paymentDetails"":
    {
        ""paymentIntent"": ""Confirm"",
        ""canHandlePendingAuthorization"": false,
        ""chargeAmount"": {
            ""amount"": ""50.00"",
            ""currencyCode"": ""USD""
         }
     },
     ""merchantMetadata"":
     {
        ""merchantReferenceId"": ""123-77-876"",
        ""merchantStoreName"": ""AmazonTestStoreFront"",
        ""noteToBuyer"": ""merchantNoteForBuyer"",
        ""customInformation"": """"
     }
}";
JObject testUpdateRequest = JObject.Parse(jsonRequestString);
WebStoreClient testPayClient = new WebStoreClient(config);

AmazonPayResponse checkoutSessionUpdateResponse = testPayClient.UpdateCheckoutSession(Request["amazonCheckoutSessionId"], testUpdateRequest);

```

## API V2 - Capture Charge

```csharp
private static readonly PayConfiguration config = new PayConfiguration
{
    Region = Regions.na,
    Environment = Environments.sandbox,
    PublicKeyId = "public_key_id",
    PrivateKey = "private_key_text"
}

string jsonRequestString = @"{
   ""captureAmount"": {
        ""amount"": ""1.23"",
        ""currencyCode"": ""USD""
    },
    ""softDescriptor"": ""For CC Statement""
}";
try {
    String chargeId = "S01-0000000-0000000-C000000";
    String iKey = Guid.NewGuid().ToString().Replace("-", "");
    Dictionary<String, String> headers = new Dictionary<String, String>()
    {
        {"x-amz-pay-Idempotency-Key", iKey}
    };
    WebStoreClient testPayClient = new WebStoreClient(config);
    AmazonPayResponse captureChargeResponse = testPayClient.captureCharge(chargeId, jsonRequestString, headers);

    if( captureChargeResponse.success) {
        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(captureChargeResponse.RawResponse);
        Response.End();
    } else {
        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write("{ errorResponse: " + captureChargeResponse.RawResponse + " }");
        Response.End();
    }
} catch (Exception ex) {
    // handle the exception
    echo ex . "\n";
}
```

## In Store MerchantScan

```csharp

JObject merchantScanRequest = JObject.Parse(@"{
    ""scanData"": ""[scanData]"",
    ""scanReferenceId"": ""[scanReferenceId]"",
    ""merchantCOE"": ""US"",
    ""ledgerCurrency"": ""USD""
}");

AmazonPayResponse merchantScanResponse = testInStoreClient.MerchantScan(merchantScanRequest);

string merchantScanChargePermissionId = merchantScanResponse.Response.SelectToken("chargePermissionId").ToString();

```

## Manual Signing (Advanced Use-Cases Only)

Example call to createSignature function with values: 

```csharp
Uri targetUrl = new Uri("https://pay-api.amazon.com/sandbox/in-store/v1/merchantScan");

string signingRequest = @"{
    ""scanData"": ""ScanData"",
    ""scanReferenceId"": ""0b8fb271-2ae2-49a5-b35d4"",
    ""merchantCOE"": ""US"",
    ""ledgerCurrency"": ""USD"",
    ""chargeTotal"": {
        ""currencyCode"": ""USD"",
        ""amount"": ""2.00""
    },
    ""metadata"": {
        ""merchantNote"": ""Ice Cream"",
        ""customInformation"": ""In-store Ice Cream"",
        ""communicationContext"": {
            ""merchantStoreName"": ""Store Name"",
            ""merchantOrderId"": ""789123""
        }
    }
}";

Dictionary<string, List<string>> requestParams = new Dictionary<string, List<string>>;
InStoreClient testInStoreClient = new InStoreClient(config);

Dictionary<String, String> headers = new Dictionary<String, String>()
{
    {"accept", "application/json"},
    {"content-type", "application/json" },
    {"x-amz-pay-region", "uk" }
};
Dictionary<string, string> signedResponse = testInStoreClient.SignRequest(targetUrl, HTTPMethods.POST, requestParams, signingRequest, headers);
```
