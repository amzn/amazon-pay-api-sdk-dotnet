# Amazon Pay API SDK (.NET)
This SDK will allow you to integrate your application with the Amazon Pay API.

Please note this SDK can only be used for specific products of Amazon Pay, including
* Checkout V2
* Alexa Delivery Notifications
* In-Store API

For integrating the API of other Amazon Pay products, please refer to [Amazon Pay API Reference Guide](https://developer.amazon.com/docs/amazon-pay-api/intro.html) 

## Requirements

This SDK is compatible with .NET Standard 2.0 (including .NET Core 2.0), as well as .NET Framework 4.5 or higher. To support usage in strong-named (signed) .NET Framework applications, the SDK defines a different set of dependencies for .NET Framework and .NET Standard. While dependent libraries for .NET Framework are all strong-named, the libaries for .NET Standard may not be strong-named. The SDK assembly itself is strong-named.

Please note that BouncyCastle 1.8.6 has known to cause issues with the signature generation that is required for API calls. The library has therefore been restricted to version >= 1.8.2 && < 1.8.6 until a fix can be provided.

### .NET Standard / .NET Core

* Platform: .NET Standard 2.0
* Newtonsoft.JSON (>= 12.0.2)
* BouncyCastle.NETCore (>= 1.8.2 && < 1.8.6)

### .NET Framwork

* Platform: .NET Framework 4.5
* Newtonsoft.JSON (>= 12.0.2)
* BouncyCastle (>= 1.8.2 && < 1.8.6)

## SDK Installation

This SDK is currently available via GitHub only (NuGet release coming soon). Please download the latest version [here](https://github.com/amzn/amazon-pay-api-sdk-dotnet/releases/download/2.0.0/Amazon.Pay.API.SDK.2.0.0.nupkg).  After download, use one of the following commands for installing the package to your project (replace value for -Source parameter if you have downloaded the package to some other place).

Visual Studio Package Manager Console
```
Install-Package Amazon.Pay.API.SDK -Version 2.0.0 -Source %USERPROFILE%\Downloads
```

.NET Core CLI
```
dotnet add package Amazon.Pay.API.SDK -v 2.0.0 -s %USERPROFILE%\Downloads\
```


## API Credentials (Public and Private Keys)

Amazon Pay uses asymmetric encryption to secure communication. You will need a public/private key pair and a corresponding Public Key ID (a unique Amazon Pay identifier for the key pair) to access Amazon Pay APIs. 

Information on how to retrieve the required API credentials can be found [here](http://amazonpaycheckoutintegrationguide.s3.amazonaws.com/amazon-pay-checkout/get-set-up-for-integration.html#4-get-your-public-key-id).

Please note that as an alternative to generating the private key via Seller Central as described in the link above, you can also generate the private key directly on your system.

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

To associate the key with your account, please follow the steps 1 and 2 described [here](http://amazonpaycheckoutintegrationguide.s3.amazonaws.com/amazon-pay-checkout/get-set-up-for-integration.html#4-get-your-public-key-id). Then:
* Navigate to the "API keys" section
* Click on the "Create keys" button
* Click "Use an existing public key to create API credentials"
* Copy/paste your public key into the edit field
* Provide a descriptive name for the credentials, e.g. "My Shop - API Credentials"
* Click on the “Create keys” button to create the public/private key pair
* After the dialog has closed, you will find you 'Public Key ID' under "Existing API keys"

# Convenience Functions (Code Samples)

Make use of the built-in convenience functions to easily make API calls.  Scroll down further to see example code snippets.

When using the convenience functions, the request payload will be signed using the provided private key, and a HTTPS request is made to the correct regional endpoint.
In the event of request throttling, the HTTPS call will be attempted up to three times using an exponential backoff approach.

## Amazon Pay Checkout v2

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
### Generate Button Signature

The signatures generated by this helper function are only valid for the Checkout v2 front-end buttons. Unlike API signing, no timestamps are involved, so the result of this function can be considered a static signature that can safely be placed in your website JS source files and used repeatedly (as long as your payload does not change).

```csharp
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore;

public class Sample
{
    public string GenerateButtonSignature()
    {
        // prepare the request
        var request = new  CreateCheckoutSessionRequest
        (
            checkoutReviewReturnUrl: "https://example.com/review.html",
            storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
        );

        // generate the button signature
        var signature = client.GenerateButtonSignature(request);

        return result;
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
            storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
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

## Other available methods

### Get authorization token for delegated API calls

Call this method to retrieve a delegated authorization token used in order to make API calls on behalf of a merchant. This method is available in all client classes; the example below shows usage in the WebStoreClient.

**Important:** This method is available only in "Live" mode.

```csharp
using Amazon.Pay.API.AuthorizationToken;
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore;
using System.Collections.Generic;

public class Sample
{
    private ApiConfiguration config;

    public void PerformDelegatedApiCall()
    {
        // set up config
        config = new ApiConfiguration
        (
            region: Region.Europe,
            environment: Environment.Live, // IMPORTANT: only available in "Live" mode
            publicKeyId: "MY_PUBLIC_KEY_ID",
            privateKey: "PATH_OR_CONTENT_OF_MY_PRIVATE_KEY"
        );

        // init API client
        var client = new WebStoreClient(config);


        // prepare the request for the auth token retrieval
        string mwsAuthToken = "amzn.mws.00000000-0000-0000-0000-000000000000"; // the MWS Auth Token
        string merchantId = "MERCHANT_ID"; // the merchant ID of the account that the API call is done on behalf of

        // send the request
        AuthorizationTokenResponse result = client.GetAuthorizationToken(mwsAuthToken, merchantId);

        // check if API call was successful
        if (!result.Success)
        {
            // do something, e.g. throw an error
        }

        // now do some API call on behalf of the merchant, for example a CheckoutSession creation

        var request = new CreateCheckoutSessionRequest
        (
            checkoutReviewReturnUrl: "https://example.com/review.html",
            storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
        );

        // IMPORTANT: the auth token must be added as an additional header for authorizing the API call
        var headers = new Dictionary<string, string>();
        headers.Add(Constants.Headers.AuthToken, result.AuthorizationToken);

        CheckoutSessionResponse anotherResult = client.CreateCheckoutSession(request);

        // check if API call was successful
        if (!anotherResult.Success)
        {
            // do something, e.g. throw an error
        }

    }
}
```