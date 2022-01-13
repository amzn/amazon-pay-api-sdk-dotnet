# Amazon Pay API SDK (.NET)
This SDK will allow you to integrate your application with the Amazon Pay API.

Please note this SDK can only be used for specific products of Amazon Pay, including
* Checkout V2
* Alexa Delivery Notifications
* In-Store API

For integrating the API of other Amazon Pay products, please refer to [Amazon Pay API Reference Guide](https://developer.amazon.com/docs/amazon-pay-api/intro.html) 

## Requirements

This SDK is compatible with .NET Standard 2.0 (including .NET Core 2.0), as well as .NET Framework 4.5 or higher. To support usage in strong-named (signed) .NET Framework applications, the SDK defines a different set of dependencies for .NET Framework and .NET Standard. While dependent libraries for .NET Framework are all strong-named, the libaries for .NET Standard may not be strong-named. The SDK assembly itself is strong-named.

### .NET Standard / .NET Core

* Platform: .NET Standard 2.0
* Newtonsoft.JSON (>= 12.0.2)
* Portable.BouncyCastle 1.8.9

### .NET Framwork

* Platform: .NET Framework 4.5
* Newtonsoft.JSON (>= 12.0.2)
* Portable.BouncyCastle 1.8.9

## SDK Installation

This SDK can be downloaded from NuGet [here](https://www.nuget.org/packages/Amazon.Pay.API.SDK) or GitHub [here](https://github.com/amzn/amazon-pay-api-sdk-dotnet/releases/download/2.5.1/Amazon.Pay.API.SDK.2.5.1.nupkg).

NuGet install from Package Manager:
```
Install-Package Amazon.Pay.API.SDK -Version 2.5.1
```

NuGet install from .NET CLI:
```
dotnet add package Amazon.Pay.API.SDK --version 2.*
```

Alternatively, to manually install after a GitHub download, use one of the following commands for installing the package to your project (replace value for -Source parameter if you have downloaded the package to some other place):

Visual Studio Package Manager Console
```
Install-Package Amazon.Pay.API.SDK -Version 2.5.1 -Source %USERPROFILE%\Downloads
```

.NET Core CLI
```
dotnet add package Amazon.Pay.API.SDK -v 2.5.1 -s %USERPROFILE%\Downloads\
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

If you have created envrionment specific keys (i.e Public Key Starts with LIVE or SANDBOX) in Seller Central, then use those PublicKeyId & PrivateKey. In this case, there is no need to pass the Environment parameter to the ApiConfiguration.
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
            publicKeyId: "MY_PUBLIC_KEY_ID", // LIVE-XXXXX or SANDBOX-XXXXX
            privateKey: "PATH_OR_CONTENT_OF_MY_PRIVATE_KEY"
        );

        // init API client
        var client = new WebStoreClient(payConfiguration);

        return client;
    }
}
```
### Generate Button Signature

The signatures generated by the `GenerateButtonSignature` helper method are only valid for the Checkout v2 front-end buttons. Unlike API signing, no timestamps are involved, so the result of this function can be considered a static signature that can safely be placed in your website JS source files and used repeatedly (as long as your payload does not change).

The method is available for both, the Amazon Pay Checkout button, and the Amazon Sign-In (Login) button. Please find code samples for both types below.

#### Amazon Pay Button (One-time payment)

```csharp
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore;

public class Sample : PageModel
{
    // ..

    public string Signature { get; private set; }

    public string Payload { get; private set; }

    public void OnGet()
    {
        // prepare the request
        var request = new CreateCheckoutSessionRequest
        (
            checkoutReviewReturnUrl: "https://example.com/review.html",
            storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
        );

        // generate the button signature
        var signature = client.GenerateButtonSignature(request);

        // generate the signature and payload string that is passed back to the frontend
        Signature = client.GenerateButtonSignature(request);
        Payload = request.ToJson();
    }
}
```

#### Amazon Pay Button (Recurring payment)

```csharp
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.Types;

public class Sample : PageModel
{
    // ..

    public string Signature { get; private set; }

    public string Payload { get; private set; }

    public void OnGet()
    {
        // prepare the request
        var request = new CreateCheckoutSessionRequest
        (
            checkoutReviewReturnUrl: "https://example.com/review.html",
            storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
        );
        request.ChargePermissionType = ChargePermissionType.Recurring;
        request.RecurringMetadata.Frequency.Unit = FrequencyUnit.Variable;
        request.RecurringMetadata.Frequency.Value = 2;
        request.RecurringMetadata.Amount.Amount = 12.34m;
        request.RecurringMetadata.Amount.CurrencyCode = Currency.USD;

        // generate the button signature
        var signature = client.GenerateButtonSignature(request);

        // generate the signature and payload string that is passed back to the frontend
        Signature = client.GenerateButtonSignature(request);
        Payload = request.ToJson();
    }
}
```

#### Amazon Pay Button (Additional payment button)

```csharp
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.Types;

public class Sample : PageModel
{
    // ..

    public string Signature { get; private set; }

    public string Payload { get; private set; }

    public void OnGet()
    {
        // prepare the request
        var request = new CreateCheckoutSessionRequest();
        request.StoreId = "amzn1.application-oa2-client.000000000000000000000000000000000";
        request.WebCheckoutDetails.CheckoutResultReturnUrl = "https://example.com/review.html";
        request.WebCheckoutDetails.CheckoutMode = CheckoutMode.ProcessOrder;

        // set payment details
        request.PaymentDetails.PaymentIntent = PaymentIntent.AuthorizeWithCapture;
        request.PaymentDetails.ChargeAmount.Amount = 10;
        request.PaymentDetails.ChargeAmount.CurrencyCode = Currency.USD;
        request.PaymentDetails.PresentmentCurrency = Currency.USD;

        // set meta data
        request.MerchantMetadata.MerchantReferenceId = "Merchant-order-123";
        request.MerchantMetadata.MerchantStoreName = "Merchant Store Name";
        request.MerchantMetadata.NoteToBuyer = "Thank you for your order";

        // submit shipping address entered by buyer on merchant website
        request.AddressDetails.Name = "Paul Smith";
        request.AddressDetails.AddressLine1 = "9999 First Avenue";
        request.AddressDetails.City= "New York";
        request.AddressDetails.StateOrRegion = "NY";
        request.AddressDetails.PostalCode = "10016";
        request.AddressDetails.CountryCode = "US";
        request.AddressDetails.PhoneNumber = "212555555";

        // generate the button signature
        var signature = client.GenerateButtonSignature(request);

        // generate the signature and payload string that is passed back to the frontend
        Signature = client.GenerateButtonSignature(request);
        Payload = request.ToJson();
    }
    }
}
```

#### Amazon Sign-In Button

```csharp
using Amazon.Pay.API.WebStore;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.WebStore.Buyer;

public class Sample : PageModel
{
    // ..

    public string Signature { get; private set; }

    public string Payload { get; private set; }

    public void OnGet()
    {
        // prepare the request
        SignInScope[] scopes = { SignInScope.Name, SignInScope.Email, SignInScope.PostalCode, SignInScope.ShippingAddress, SignInScope.PhoneNumber };
        var request = new SignInRequest
        (
            signInReturnUrl: "https://example.com/account.html",
            storeId: "amzn1.application-oa2-client.000000000000000000000000000000000",
            signInScopes: scopes
        );

        // generate the button signature
        var signature = client.GenerateButtonSignature(request);

        // generate the signature and payload string that is passed back to the frontend
        Signature = client.GenerateButtonSignature(request);
        Payload = request.ToJson();
    }
}
```

#### Passing Signature and Payload to Front-End

The code below shows how you could pass the signature and payload generated with code samples above back to the front-end. This sample uses an ASP.NET Core Razor Page, but the concept is similar for other .NET web project types.

```html
@page
@model Sample.CheckoutModel
@{
    ViewData["Title"] = "Checkout";
}

<h1>Checkout</h1>

<div id="AmazonPayButton"></div>
<script src="https://static-na.payments-amazon.com/checkout.js"></script>
<script type="text/javascript" charset="utf-8">
        amazon.Pay.renderButton('#AmazonPayButton', {
            // set checkout environment
            merchantId: 'merchant_id',
            ledgerCurrency: 'USD',
            sandbox: true,
            // customize the buyer experience
            checkoutLanguage: 'en_US',
            productType: 'PayAndShip',
            placement: 'Cart',
            buttonColor: 'Gold',
            // configure Create Checkout Session request
            createCheckoutSessionConfig: {
                payloadJSON: '@Html.Raw(Model.Payload)',
                signature: '@Html.Raw(Model.Signature)',
                publicKeyId: 'xxxxxxxxxx',
                algorithm: 'AMZN-PAY-RSASSA-PSS-V2'
            }
        });
</script>
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

        // optional: add address restriction to limit which shipping addresses can be selected (here US addresses will be allowed only)
        request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.Allowed;
        request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("US");

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

### Create CheckoutSession With Scopes
```csharp
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore;

public class Sample
{
    public CheckoutSessionResponse CreateCheckoutSession()
    {
        CheckoutSessionScope[] scopes = new CheckoutSessionScope[] {
                CheckoutSessionScope.Name,
                CheckoutSessionScope.Email,
                CheckoutSessionScope.PostalCode,
                CheckoutSessionScope.ShippingAddress,
                CheckoutSessionScope.PhoneNumber,
                CheckoutSessionScope.PrimeStatus,
                CheckoutSessionScope.BillingAddress
        };

        // prepare the request
        var request = new  CreateCheckoutSessionRequest
        (
            checkoutReviewReturnUrl: "https://example.com/review.html",
            storeId: "amzn1.application-oa2-client.000000000000000000000000000000000",
            scopes
        );

        // optional: add address restriction to limit which shipping addresses can be selected (here US addresses will be allowed only)
        request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.Allowed;
        request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("US");

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

### Get CheckoutSession
```csharp
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore;
using Amazon.Pay.API.WebStore.CheckoutSession;

public class Sample
{
    public void GetCheckoutSession(string checkoutSessionId)
    {
        // send the request
        CheckoutSessionResponse result = client.GetCheckoutSession(checkoutSessionId);

        // check if API call was successful
        if (!result.Success)
        {
            // handle the API error (use Status field to get the numeric error code)
        }
            // do something with the result, for instance:
            Buyer buyer = result.Buyer;
            BillingAddress billingAddress = result.BillingAddress;
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

### Get ChargePermission
```csharp
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore;
using Amazon.Pay.API.WebStore.ChargePermission;
using Amazon.Pay.API.WebStore.Types;
using System;

public class Sample
{
    //  ...

    public void GetChargePermission()
    {
        // prepare the request
        var chargePermissionId = "P01-0000000-0000000";

        // send the request
        ChargePermissionResponse result = client.GetChargePermission(chargePermissionId);

        // check if API call was successful
        if (!result.Success)
        {
            // handle the API error (use Status field to get the numeric error code)
        }

        // do something with the result, for instance:
        State chargePermissionState = result.StatusDetails.State;
        DateTime chargePermissionExpiryDate = result.ExpirationTimestamp;
        Address buyerAddress = result.BillingAddress;

        // ...
    }
}
```

### Update ChargePermission
```csharp
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore;
using Amazon.Pay.API.WebStore.ChargePermission;

public class Sample
{
    //  ...

    public void UpdateChargePermission()
    {
        // prepare the request
        var chargePermissionId = "P01-0000000-0000000";
        var request = new UpdateChargePermissionRequest();
        request.MerchantMetadata.MerchantReferenceId = "32-41-323141-32";
        request.MerchantMetadata.MerchantStoreName = "AmazonTestStoreFront";
        request.MerchantMetadata.NoteToBuyer = "Some Note to buyer";
        request.MerchantMetadata.CustomInformation = "";

        // send the request
        ChargePermissionResponse result = client.UpdateChargePermission(chargePermissionId, request);

        // check if API call was successful
        if (!result.Success)
        {
            // handle the API error (use Status field to get the numeric error code)
        }

        // do something with the result, for instance:
        State chargePermissionState = result.StatusDetails.State;

        // ...
    }
}
```

### Close ChargePermission
```csharp
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore;
using Amazon.Pay.API.WebStore.ChargePermission;

public class Sample
{
    //  ...

    public void CloseChargePermission()
    {
        // prepare the request
        var chargePermissionId = "P01-0000000-0000000";
        var request = new CloseChargePermissionRequest("No more charges required");

        // send the request
        ChargePermissionResponse result = client.CloseChargePermission(chargePermissionId, request);

        // check if API call was successful
        if (!result.Success)
        {
            // handle the API error (use Status field to get the numeric error code)
        }

        // do something with the result, for instance:
        State chargePermissionState = result.StatusDetails.State;

        // ...
    }
}
```

### Get Buyer information

This API call can be used only in conjunction with the Sign-In Button. The buyer token required for the API call is available as URL parameter after the user was redirect to the URL specified by `signInReturnUrl` (see [Amazon Sign-In Button](#amazon-sign-in-button)).

```csharp
using Amazon.Pay.API.WebStore;
using Amazon.Pay.API.WebStore.Buyer;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class Sample : PageModel
{
    // ..

    public BuyerResponse Buyer { get; private set; }

    public void OnGet()
    {
        // the token as retrieved from the URL
        string buyerToken = HttpContext.Request.Query["buyerToken"];

        // get the buyer details
        // NOTE: the BuyerResponse that is returned here contains properties for buyerId, name, email, shippingAddress, phoneNumber etc.
        Buyer = client.GetBuyer(buyerToken);
    }
}
```

## Alexa Delivery Notifications

### Initiate Client
Use any of the channel-specific client classes if you want to send an Alexa delivery notification. See [Initiate WebStoreClient](#initiate-client) for example.

### Send Alexa Delivery Notification

```csharp
using Amazon.Pay.API;
using Amazon.Pay.API.DeliveryTracker;
using Amazon.Pay.API.Types;

public class Sample
{
    public DeliveryTrackerResponse SendDeliveryTrackingInformation()
    {
        // prepare the request
        var request = new DeliveryTrackerRequest
        (
            objectId: "P00-0000000-0000000", // ChargePermissionID or OrderReferenceID
            objectIsChargePermission: true,  // true if above is ChargePermissionID, false otherwise 
            trackingNumber: "1Z999AA10123456784", 
            carrierCode: "UPS"
        );

        // send the notification
        var result = client.SendDeliveryTrackingInformation(request);

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
