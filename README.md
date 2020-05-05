# Amazon Pay API SDK (.NET)
This SDK will allow you to integrate your application with the Amazon Pay API.

Please note this SDK can only be used for specific products of Amazon Pay, including
* Checkout V2
* Alexa Delivery Notifications
* In-Store API

For integrating the API of other Amazon Pay products, please refer to [Amazon Pay API Reference Guide](https://developer.amazon.com/docs/amazon-pay-api/intro.html) 

## Requirements

* Platform: .NET Core 2.0 or .NET Framework 3.5
* Newtonsoft.JSON 12.0.2
* BouncyCastle.NETCore 1.8.5

## SDK Installation

This SDK is available on NuGet under the name 'amazon-pay-api-sdk-dotnet'. 

Installation via Package Manager:
```
PM> Install-Package amazon-pay-api-sdk-dotnet -Version 1.0.0
```

Installation via .NET CLI:
```
> dotnet add package amazon-pay-api-sdk-dotnet --version 1.0.0
```

## Public and Private Keys

Amazon Pay uses asymmetric encryption to secure communication. Generate two keys: one public key and one private key. Exchange the public key for a publicKeyId to access Amazon Pay APIs.

In Windows 10 this can be done with ssh-keygen commands:

```
ssh-keygen -t rsa -b 2048 -f private.pem
ssh-keygen -f private.pem -e -m PKCS8 > public.pub
```

In Linux or macOS this can be done using openssl commands:

```
openssl genrsa -out private.pem 2048
openssl rsa -in private.pem -pubout > public.pub
```

The first command above generates a private key and the second line uses the private key to generate a public key.

To associate the key with your account, send an email to your Amazon Pay account manager, or to Amazon Pay merchant support, including (1) your *public* key and (2) your Merchant ID.  Do not send your private key to Amazon (or anyone else) under any circumstance!

In your Seller Central account, within 1-2 business days, the account administrator will receive a message that includes the public_key_id you will need to use the SDK.

# Convenience Functions (Code Samples)

Make use of the built-in convenience functions to easily make API calls.  Scroll down further to see example code snippets.

When using the convenience functions, the request payload will be signed using the provided private key, and a HTTPS request is made to the correct regional endpoint.
In the event of request throttling, the HTTPS call will be attempted up to three times using an exponential backoff approach.

### Initiate Client
Before issuing any API call, you will have to create a configuration object and pass this to the constructor of the client class. You can then use the client for any of the following code samples of the Amazon Checkout v2 API.

```csharp
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore;

public class Sample
{
    public WebStoreClient InitiateClient()
    {
        // set up config
        var payConfiguration = new ApiConfiguration
        (
            region: Region.Europe,
            environment: Environment.Sandbox,
            publicKeyId: "MY_PUBLIC_KEY_ID",
            privateKey: "PATH_OR_CONTENT_OF_MY_PRIVATE_KEY"
        );

        // init API client
        var client = new WebStoreClient(payConfiguration);

        return client;
    }
}
```

### Create CheckoutSession
```csharp
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore;

public class Sample
{
    public CheckoutSessionResponse CreateCheckoutSession()
    {
        // prepare the request
        var request = new  CreateCheckoutSessionRequest
        (
            checkoutReviewReturnUrl: "https://example.com/review.html",
            storeId: "amzn1.application-oa2-client.22398d9b313d459e9fd68872785cf54a"
        );

        // send the request
        CheckoutSessionResponse result = client.CreateCheckoutSession(request);

        // check if API call was successful
        if (!result.Success)
        {
            // do something, e.g. throw an error
        }

        return result;
    }
}
```

### Update CheckoutSession
```csharp
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore;
using Amazon.Pay.API.WebStore.Types;

public class Sample
{
    public CheckoutSessionResponse UpdateCheckoutSession(string checkoutSessionId)
    {
        // prepare the request
        var request = new UpdateCheckoutSessionRequest();
        request.WebCheckoutDetails.CheckoutResultReturnUrl = "https://example.com/thankyou.html";
        request.PaymentDetails.ChargeAmount.Amount = 99.99M;
        request.PaymentDetails.ChargeAmount.CurrencyCode = Currency.EUR;
        request.PaymentDetails.PaymentIntent = PaymentIntent.Authorize;
        request.MerchantMetadata.MerchantReferenceId = "12345";
        request.MerchantMetadata.MerchantStoreName = "My Shop";
        request.MerchantMetadata.NoteToBuyer = "Thank you!";

        // send the request
        var result = client.UpdateCheckoutSession(checkoutSessionId, request);

        // check if API call was successful
        if (!result.Success)
        {
            // do something, e.g. throw an error
        }

        return result;
    }
}
```

### Complete CheckoutSession
```csharp
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore;

public class Sample
{
    public CheckoutSessionResponse CompleteCheckoutSession(string checkoutSessionId)
    {
        // prepare the request
        var request = new CompleteCheckoutSessionRequest(106.98M, Currency.EUR);

        // send the request
        var result = client.CompleteCheckoutSession(checkoutSessionId, request);

        // check if API call was successful
        if (!result.Success)
        {
            // do something, e.g. throw an error
        }

        return result;
    }
}
```

