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
