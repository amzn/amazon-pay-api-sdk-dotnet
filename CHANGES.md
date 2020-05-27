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