#### Version 2.7.2 - October 2023
* Introducing new Merchant Onboarding & Account Management APIs, which allows our partners to onboard merchants programatically and as part of account management offer them creation, updation and deletion/dissociation capability.
* Introducing new API called finalizeCheckoutSession which validates checkout attributes and finalizes checkout session. On success returns charge permission id and charge id. Use this API to process payments for JavaScript-based integrations.
* Added new parameter "chargePermissionId" in CreateCheckoutSession Request, as a part of PMOF(Payment Method On File) changes.

#### Version 2.7.1 - June 2023
* Added new parameters "CheckoutButtonText" in GetCheckoutSession Response, as a part of Affirm changes in Place Order Button. Change is fully backwards compatible.
* Added the Sample Code snippets for the Charge APIs.
* Corrected README.md file related to Reporting APIs.

#### Version 2.7.0 - April 2023
* Introducing new v2 Reporting APIs. Reports allow you to retrieve consolidated data about Amazon Pay transactions and settlements. In addition to managing and downloading reports using Seller Central, Amazon Pay offers APIs to manage and retrieve your reports.
* Introducing new signature generation algorithm AMZN-PAY-RSASSA-PSS-V2 & increasing salt length from 20 to 32.
* Added new parameters "MultiAddressesCheckoutMetadata" and "ShippingAddressList" in Checkout Session objects to support PayAndShipMultiAddress productType. Change is fully backwards compatible.
* New type of Charge permission "PaymentMethodOnFile" is added, ChargeInitiator & Channel are added to CreateCharge API request/response objects
* Adding Error code 408 to API retry logic
* Note : To use new V2 algorithm AMZN-PAY-RSASSA-PSS-V2, "algorithm" needs to be provided as an additional field in "ApiConfiguration" and also while rendering Amazon Pay button in "createCheckoutSessionConfig". The changes are backwards-compatible, SDK will use AMZN-PAY-RSASSA-PSS by default.

#### Version 2.6.0 - July 2022
* Fixed security vulnerabilities in the SDK version 2.5.1.
* Adding a new parameter "PlatformId" in Charge object which will in help Solution Provider monitoring
**Please dont use Version 2.5.0**

#### Version 2.5.1 - January 2022
* Applied patch to address issues occurred in Version 2.5.0.

#### Version 2.5.0 - January 2022
* Updated "ExpirationTimestamp" property on ChargePermissionResponse to nullable
* Migrated signature generating algorithm from AMZN-PAY-RSASSA-PSS to AMZN-PAY-RSASSA-PSS-V2 & increased salt length from 20 to 32
* Note : From this SDK version, "algorithm" need to be provided as additional field in "createCheckoutSessionConfig" while rendering Amazon Pay button.

#### Version 2.4.9 - June 2021
* Added API Retry mechanism for error codes 502 & 504

### Version 2.4.8 - May 2021
* Enabled support for environment specific keys (i.e Public key & Private key). The changes are fully backwards-compatible, where merchants can also use non environment specific keys
* Added "EstimateOrderTotal" to PaymentDetails

### Version 2.4.7 - May 2021
* Added "recurringMetadata" to UpdateChargePermissionRequest for ChargePermission API
* Added "HasPrimeMembershipType" to BuyerResponse for Buyer API
* Added "SendDeliveryTrackingInformation" and "GetAuthorizationToken" to IClient, inherited by IInStoreClient and IWebStoreClient 

### Version 2.4.6 - April 2021
* Added "checkoutCancelUrl" to WebCheckoutDetails for CheckoutSession API

### Version 2.4.5 - March 2021
* Added "phoneNumber" and "primeMembershipTypes" to "Buyer" object for both CheckoutSession and ChargePermission API's
* Added "phoneNumber" and "primeMembershipTypes" to "BuyerResponse" object for GetBuyer API
* Added "billingAddress" & "primeStatus" as scopes to SignInScopes
* Enabled CreateCheckoutSession Scopes for CreateCheckoutSession API
* Note : Passing "primeStatus" as SignInScope or CheckoutSessionScope will return value for field "primeMembershipTypes" in response, only if the customer is eligible for prime membership

### Version 2.4.4 - February 2021
* Switch from BouncyCastle and BouncyCastle.Core reference to latest release Portable.BouncyCastle (1.8.9), while also switching to use default trailer for PssSigner in signature generation.  This resolves previous signature generation issues encountered using non-default trailer for PssSigner on BouncyCastle versions 1.8.6 and higher.

### Version 2.4.3 - January 2021
* Add parameterless constructor for CreateCheckoutSessionRequest to better support Additional Payment Button (APB) workflow
* Add Interface for SignatureHelper to allow mocking in tests
* Correct packaging of LICENSE

### Version 2.4.2 - December 2020
* Added the "ShippingAdress" property to BuyerResponse which can be accessed by passing "shippingAddress" as signInScopes parameter while rendering Amazon Sign-in button

### Version 2.4.1
* Add Interfaces for WebStoreClient and InstoreClient to allow developers to mock classes in tests.

### Verion 2.4.0 - October 2020
* Added support for Additional Payment Button (APB) flow, see https://amazonpaycheckoutintegrationguide.s3.amazonaws.com/amazon-pay-apb-checkout/add-the-amazon-pay-button.html, which adds WebCheckoutDetails.CheckoutMode and AddressDetails to CreateCheckoutSessionRequest
* Added missing property SoftDescriptor to PaymentDetails

### Version 2.3.2 - September 2020
* Correct State.Refunded name and other improvements to State and ReasonCode (static to const)

### Version 2.3.1 - September 2020
* Replace enums for ReasonCode and State with static class to avoid JSON exception on introduction of new non-breaking changes in the API

### Version 2.3.0 - July 2020
* Add suppport for Recurring and Multi-ship API features
* Remove duplicate code in SDK's CreateCheckoutSessionRequest and UpdateCheckoutSessionRequest objects
* Remove duplicate Tests.DotNetCore unit tests and folded into existing unit tests
* Fix bug causing some objects to become corrupted after .ToJson() call

### Version 2.2.0 - June 25, 2020

* Added support for "Sign-In only" feature (types and mehthods)
* Added missing property ExpirationTimestamp on CheckoutSessionResponse class
* Simplified usage of DeliverySpecifications / AddressRestriction feature
* Fixed issue with decimal conversion when using Japanese Yen

### Version 2.1.0 - June 9, 2020

* Added GetAuthorizationToken method (available for all clients)
* Set TLS version for API requests to version 1.2 to avoid issues on .NET versions where TLS 1.2 is not yet the default

### Version 2.0.0 - May 25, 2020

Adapted SDK to support Amazon Pay API version 2

**Amazon Pay Checkout v2 changes**

* Introduction of "Complete CheckoutSession" API call
* Billing address moved from `PaymentPreferences` to root of `CheckoutSession` and `ChargePermission` objects
* `ChargePermission` now provides `Limits` field that shows maximum chargeable amount and remaining balance
* Added helper function for button signature generation to WebStoreClient (Amazon Pay Checkout v2 client)

### Version 1.0.0 - Apr 13, 2020

Initial release, incl. support for

* Amazon Pay Checkout v2
* Amazon Pay In-Store API
* Alexa Delivery Notifications for Amazon Pay
