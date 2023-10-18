using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.CheckoutSession
{
    [TestFixture]
    public class CreateCheckoutSessionRequestTests
    {
        private readonly MerchantMetadata metadataToTest = new MerchantMetadata() { CustomInformation = "foo", MerchantReferenceId = "123", MerchantStoreName = "myStore", NoteToBuyer = "myBuyerNote" };
        [Test]
        public void CanConstructWithAllPropertiesInitialized()
        {
            // act
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.WebCheckoutDetails);
            Assert.IsNotNull(request.DeliverySpecifications);
            Assert.IsNotNull(request.MerchantMetadata);
            Assert.IsNotNull(request.PaymentDetails);
            Assert.IsNotNull(request.ProviderMetadata);
            Assert.IsNotNull(request.RecurringMetadata);
            Assert.AreEqual("https://example.com/review.html", request.WebCheckoutDetails.CheckoutReviewReturnUrl);
            Assert.AreEqual("amzn1.application-oa2-client.000000000000000000000000000000000", request.StoreId);
        }

        [Test]
        public void CanConstructParameterlessCtorWithAllPropertiesInitialized()
        {
            // act
            var request = new CreateCheckoutSessionRequest();

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.WebCheckoutDetails);
            Assert.IsNotNull(request.DeliverySpecifications);
            Assert.IsNotNull(request.MerchantMetadata);
            Assert.IsNotNull(request.PaymentDetails);
            Assert.IsNotNull(request.ProviderMetadata);
            Assert.IsNotNull(request.RecurringMetadata);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\"}}", json);

            // verify request object still allows all setters after being serialized
            request.MerchantMetadata.CustomInformation = metadataToTest.CustomInformation;
            request.MerchantMetadata.MerchantReferenceId = metadataToTest.MerchantReferenceId;
            request.MerchantMetadata.MerchantStoreName = metadataToTest.MerchantStoreName;
            request.MerchantMetadata.NoteToBuyer = metadataToTest.NoteToBuyer;
            request.ChargePermissionType = ChargePermissionType.Recurring;
            request.RecurringMetadata.Frequency.Unit = FrequencyUnit.Variable;
            request.RecurringMetadata.Frequency.Value = 2;
            request.RecurringMetadata.Amount.Amount = 12.34m;
            request.RecurringMetadata.Amount.CurrencyCode = Currency.USD;
            request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.Allowed;
            request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("US").AddZipCodesRestriction("12345");
            request.DeliverySpecifications.SpecialRestrictions.Add(SpecialRestriction.RestrictPackstations);
            request.DeliverySpecifications.SpecialRestrictions.Add(SpecialRestriction.RestrictPOBoxes);
            request.PaymentDetails.ChargeAmount.Amount = 1080M;
            request.PaymentDetails.ChargeAmount.CurrencyCode = Currency.EUR;
            request.PaymentDetails.TotalOrderAmount.Amount = 1080M;
            request.PaymentDetails.TotalOrderAmount.CurrencyCode = Currency.EUR;
            request.PaymentDetails.PaymentIntent = PaymentIntent.Authorize;
        }

        [Test]
        public void CanConvertParameterlessCtorToJsonMinimal()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest();

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);

            // verify request object still allows all setters after being serialized
            request.MerchantMetadata.CustomInformation = metadataToTest.CustomInformation;
            request.MerchantMetadata.MerchantReferenceId = metadataToTest.MerchantReferenceId;
            request.MerchantMetadata.MerchantStoreName = metadataToTest.MerchantStoreName;
            request.MerchantMetadata.NoteToBuyer = metadataToTest.NoteToBuyer;
            request.ChargePermissionType = ChargePermissionType.Recurring;
            request.RecurringMetadata.Frequency.Unit = FrequencyUnit.Variable;
            request.RecurringMetadata.Frequency.Value = 2;
            request.RecurringMetadata.Amount.Amount = 12.34m;
            request.RecurringMetadata.Amount.CurrencyCode = Currency.USD;
            request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.Allowed;
            request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("US")
                .AddZipCodesRestriction("12345");
            request.DeliverySpecifications.SpecialRestrictions.Add(SpecialRestriction.RestrictPackstations);
            request.DeliverySpecifications.SpecialRestrictions.Add(SpecialRestriction.RestrictPOBoxes);
            request.PaymentDetails.ChargeAmount.Amount = 1080M;
            request.PaymentDetails.ChargeAmount.CurrencyCode = Currency.EUR;
            request.PaymentDetails.TotalOrderAmount.Amount = 1080M;
            request.PaymentDetails.TotalOrderAmount.CurrencyCode = Currency.EUR;
            request.PaymentDetails.PaymentIntent = PaymentIntent.Authorize;
        }

        [Test]
        public void CanConvertToJsonDeliverySpecifications()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );
            request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.Allowed;
            request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("US").AddZipCodesRestriction("12345");
            request.DeliverySpecifications.SpecialRestrictions.Add(SpecialRestriction.RestrictPackstations);
            request.DeliverySpecifications.SpecialRestrictions.Add(SpecialRestriction.RestrictPOBoxes);

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"deliverySpecifications\":{\"specialRestrictions\":[\"RestrictPackstations\",\"RestrictPOBoxes\"],\"addressRestrictions\":{\"type\":\"Allowed\",\"restrictions\":{\"US\":{\"zipCodes\":[\"12345\"]}}}},\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\"}}", json);
        }

        [Test]
        public void CanConvertToJsonMerchantMetaData()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );
            request.MerchantMetadata.CustomInformation = metadataToTest.CustomInformation;
            request.MerchantMetadata.MerchantReferenceId = metadataToTest.MerchantReferenceId;
            request.MerchantMetadata.MerchantStoreName = metadataToTest.MerchantStoreName;
            request.MerchantMetadata.NoteToBuyer = metadataToTest.NoteToBuyer;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\"},\"merchantMetadata\":{\"merchantReferenceId\":\"123\",\"merchantStoreName\":\"myStore\",\"noteToBuyer\":\"myBuyerNote\",\"customInformation\":\"foo\"}}", json);
        }

        [Test]
        public void CanConvertToJsonRecurring()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );
            request.MerchantMetadata.CustomInformation = metadataToTest.CustomInformation;
            request.MerchantMetadata.MerchantReferenceId = metadataToTest.MerchantReferenceId;
            request.MerchantMetadata.MerchantStoreName = metadataToTest.MerchantStoreName;
            request.MerchantMetadata.NoteToBuyer = metadataToTest.NoteToBuyer;
            request.ChargePermissionType = ChargePermissionType.Recurring;
            request.RecurringMetadata.Frequency.Unit = FrequencyUnit.Variable;
            request.RecurringMetadata.Frequency.Value = 2;
            request.RecurringMetadata.Amount.Amount = 12.34m;
            request.RecurringMetadata.Amount.CurrencyCode = Currency.USD;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\"},\"merchantMetadata\":{\"merchantReferenceId\":\"123\",\"merchantStoreName\":\"myStore\",\"noteToBuyer\":\"myBuyerNote\",\"customInformation\":\"foo\"},\"chargePermissionType\":\"Recurring\",\"recurringMetadata\":{\"frequency\":{\"unit\":\"Variable\",\"value\":2},\"amount\":{\"amount\":12.34,\"currencyCode\":\"USD\"}}}", json);
        }

        [Test]
        public void CanConvertToJsonPaymentMethodOnFile()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );
            request.WebCheckoutDetails.CheckoutResultReturnUrl = "https://example.com/return.html";
            request.ChargePermissionType = ChargePermissionType.PaymentMethodOnFile;
            request.PaymentMethodOnFileMetadata.SetupOnly = true;
            request.PaymentDetails.PaymentIntent = PaymentIntent.Confirm;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"paymentMethodOnFileMetadata\":{\"setupOnly\":true},\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\",\"checkoutResultReturnUrl\":\"https://example.com/return.html\"},\"paymentDetails\":{\"paymentIntent\":\"Confirm\"},\"chargePermissionType\":\"PaymentMethodOnFile\"}", json);
        }

        [Test]
        public void CanConvertToJsonPaymentMethodOnFileForUpdateFlow()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000",
                chargePermissionId: "B01-0000000-0000000"
            );
            request.WebCheckoutDetails.CheckoutResultReturnUrl = "https://example.com/return.html";
            request.ChargePermissionType = ChargePermissionType.PaymentMethodOnFile;
            request.PaymentMethodOnFileMetadata.SetupOnly = true;
            request.PaymentDetails.PaymentIntent = PaymentIntent.Confirm;
            request.PaymentDetails.CanHandlePendingAuthorization = false;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"paymentMethodOnFileMetadata\":{\"setupOnly\":true},\"chargePermissionId\":\"B01-0000000-0000000\",\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\",\"checkoutResultReturnUrl\":\"https://example.com/return.html\"},\"paymentDetails\":{\"paymentIntent\":\"Confirm\",\"canHandlePendingAuthorization\":false},\"chargePermissionType\":\"PaymentMethodOnFile\"}", json);
        }

        [Test]
        public void AdditionalPaymentButton()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );
            request.WebCheckoutDetails.CheckoutMode = CheckoutMode.ProcessOrder;
            request.AddressDetails.Name = "Paul Smith";
            request.AddressDetails.AddressLine1 = "9999 First Avenue";
            request.AddressDetails.City = "New York";
            request.AddressDetails.StateOrRegion = "NY";
            request.AddressDetails.PostalCode = "10016";
            request.AddressDetails.CountryCode = "US";
            request.AddressDetails.PhoneNumber = "212555555";
            request.AddressDetails.DistrictOrCounty = "Manhattan";
            request.PaymentDetails.PaymentIntent = PaymentIntent.AuthorizeWithCapture;
            request.PaymentDetails.ChargeAmount.Amount = 10;
            request.PaymentDetails.ChargeAmount.CurrencyCode = Currency.USD;
            request.PaymentDetails.PresentmentCurrency = Currency.USD;
            request.MerchantMetadata.MerchantReferenceId = "Merchant Ref ID";
            request.MerchantMetadata.MerchantStoreName = "Store Name";
            request.MerchantMetadata.NoteToBuyer = "Buyer Note";

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"addressDetails\":{\"districtOrCounty\":\"Manhattan\",\"name\":\"Paul Smith\",\"phoneNumber\":\"212555555\",\"addressLine1\":\"9999 First Avenue\",\"city\":\"New York\",\"stateOrRegion\":\"NY\",\"postalCode\":\"10016\",\"countryCode\":\"US\"},\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\",\"checkoutMode\":\"ProcessOrder\"},\"paymentDetails\":{\"paymentIntent\":\"AuthorizeWithCapture\",\"chargeAmount\":{\"amount\":10,\"currencyCode\":\"USD\"},\"presentmentCurrency\":\"USD\"},\"merchantMetadata\":{\"merchantReferenceId\":\"Merchant Ref ID\",\"merchantStoreName\":\"Store Name\",\"noteToBuyer\":\"Buyer Note\"}}", json);
        }

        [Test]
        public void CheckoutSessionResponseTest()
        {
            string json = "{\"checkoutSessionId\":\"7efe55c3-38c7-4a1b-944d-aded27032b9a\",\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://localhost/cv2v2/CheckoutReview.php\",\"checkoutResultReturnUrl\":\"https://localhost/cv2v2/CheckoutResult.php\",\"amazonPayRedirectUrl\":\"https://apay-us.amazon.com/checkout/processing?amazonCheckoutSessionId=7efe55c3-38c7-4a1b-944d-aded27032b9a\"},\"productType\":\"PayAndShip\",\"paymentDetails\":{\"paymentIntent\":\"Authorize\",\"canHandlePendingAuthorization\":false,\"chargeAmount\":{\"amount\":\"1.40\",\"currencyCode\":\"USD\"},\"totalOrderAmount\":null,\"softDescriptor\":null,\"presentmentCurrency\":\"USD\",\"allowOvercharge\":null,\"extendExpiration\":null},\"chargePermissionType\":\"OneTime\",\"recurringMetadata\":null,\"merchantMetadata\":{\"merchantReferenceId\":\"2020-0001\",\"merchantStoreName\":\"Your Store Name Goes Here\",\"noteToBuyer\":\"Order #12345-67890\",\"customInformation\":\"custom information can go here\"},\"supplementaryData\":null,\"buyer\":{\"name\":\"Sandbox Guillot\",\"email\":\"guillotb+sandbox@amazon.com\",\"buyerId\":\"amzn1.account.AFBDVVLYITS5Q7CJV7UJVFAMU2PA\"},\"billingAddress\":{\"name\":\"Christopher C. Conn\",\"addressLine1\":\"4996 Rockford Mountain Lane\",\"addressLine2\":null,\"addressLine3\":null,\"city\":\"Appleton\",\"county\":null,\"district\":null,\"stateOrRegion\":\"WI\",\"postalCode\":\"54911\",\"countryCode\":\"US\",\"phoneNumber\":null},\"paymentPreferences\":[{\"paymentDescriptor\":\"Your selected Amazon payment method\"}],\"statusDetails\":{\"state\":\"Open\",\"reasonCode\":null,\"reasonDescription\":null,\"lastUpdatedTimestamp\":\"20201023T211923Z\"},\"shippingAddress\":{\"name\":\"Susie Smith\",\"addressLine1\":\"10 Ditka Ave\",\"addressLine2\":\"Suite 2500\",\"addressLine3\":null,\"city\":\"Chicago\",\"county\":null,\"district\":null,\"stateOrRegion\":\"IL\",\"postalCode\":\"60602\",\"countryCode\":\"US\",\"phoneNumber\":\"800-000-0000\"},\"platformId\":null,\"chargePermissionId\":null,\"chargeId\":null,\"constraints\":[],\"creationTimestamp\":\"20201023T211909Z\",\"expirationTimestamp\":\"20201024T211909Z\",\"storeId\":\"amzn1.application-oa2-client.d9da4374abb24732a174be549d99eb7a\",\"providerMetadata\":{\"providerReferenceId\":null},\"releaseEnvironment\":\"Sandbox\",\"checkoutButtonText\":null,\"deliverySpecifications\":null}";
            var dateConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyyMMddTHHmmssZ" };
            CheckoutSessionResponse r = new CheckoutSessionResponse();
            r = JsonConvert.DeserializeObject<CheckoutSessionResponse>(json, dateConverter);

            Assert.AreEqual("Christopher C. Conn", r.BillingAddress.Name);
            Assert.AreEqual("4996 Rockford Mountain Lane", r.BillingAddress.AddressLine1);
            Assert.IsNull(r.BillingAddress.AddressLine2);
            Assert.IsNull(r.BillingAddress.AddressLine3);
            Assert.AreEqual("Appleton", r.BillingAddress.City);
            Assert.IsNull(r.BillingAddress.County);
            Assert.IsNull(r.BillingAddress.District);
            Assert.AreEqual("WI", r.BillingAddress.StateOrRegion);
            Assert.AreEqual("54911", r.BillingAddress.PostalCode);
            Assert.AreEqual("US", r.BillingAddress.CountryCode);
            Assert.IsNull(r.BillingAddress.PhoneNumber);

            Assert.AreEqual("Susie Smith", r.ShippingAddress.Name);
            Assert.AreEqual("10 Ditka Ave", r.ShippingAddress.AddressLine1);
            Assert.AreEqual("Suite 2500", r.ShippingAddress.AddressLine2);
            Assert.IsNull(r.ShippingAddress.AddressLine3);
            Assert.AreEqual("Chicago", r.ShippingAddress.City);
            Assert.IsNull(r.ShippingAddress.County);
            Assert.IsNull(r.ShippingAddress.District);
            Assert.AreEqual("IL", r.ShippingAddress.StateOrRegion);
            Assert.AreEqual("60602", r.ShippingAddress.PostalCode);
            Assert.AreEqual("US", r.ShippingAddress.CountryCode);
            Assert.AreEqual("800-000-0000", r.ShippingAddress.PhoneNumber);
            Assert.IsNull(r.CheckoutButtonText);
        }

        [Test]
        public void CanConstructWithAllCheckoutSessionScopes()
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
            // act
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000",
                scopes
            );

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.WebCheckoutDetails);
            Assert.IsNotNull(request.DeliverySpecifications);
            Assert.IsNotNull(request.MerchantMetadata);
            Assert.IsNotNull(request.PaymentDetails);
            Assert.IsNotNull(request.ProviderMetadata);
            Assert.IsNotNull(request.RecurringMetadata);
            Assert.IsNotNull(request.CheckoutSessionScope);
            Assert.Contains(CheckoutSessionScope.Name, request.CheckoutSessionScope);
            Assert.Contains(CheckoutSessionScope.Email, request.CheckoutSessionScope);
            Assert.Contains(CheckoutSessionScope.PostalCode, request.CheckoutSessionScope);
            Assert.Contains(CheckoutSessionScope.ShippingAddress, request.CheckoutSessionScope);
            Assert.Contains(CheckoutSessionScope.PhoneNumber, request.CheckoutSessionScope);
            Assert.Contains(CheckoutSessionScope.PrimeStatus, request.CheckoutSessionScope);
            Assert.Contains(CheckoutSessionScope.BillingAddress, request.CheckoutSessionScope);
            Assert.AreEqual("https://example.com/review.html", request.WebCheckoutDetails.CheckoutReviewReturnUrl);
            Assert.AreEqual("amzn1.application-oa2-client.000000000000000000000000000000000", request.StoreId);
        }

        [Test]
        public void CanConvertToJsonWithCheckoutSessionScope()
        {
            CheckoutSessionScope[] scopes = new CheckoutSessionScope[] {
                CheckoutSessionScope.Name,
                CheckoutSessionScope.Email,
                CheckoutSessionScope.PostalCode,
            };

            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000",
                scopes
            );

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"scopes\":[\"name\",\"email\",\"postalCode\"],\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\"}}", json);
        }

        [Test]
        public void CanConvertToJsonMinimalWithAllCheckoutSessionScopes()
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

            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000",
                scopes
            );

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"scopes\":[\"name\",\"email\",\"postalCode\",\"shippingAddress\",\"phoneNumber\",\"primeStatus\",\"billingAddress\"],\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\"}}", json);
        }

    }
}
