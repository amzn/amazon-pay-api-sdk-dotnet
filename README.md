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
