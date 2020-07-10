using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.Types;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.CheckoutSession
{
    [TestFixture]
    public class CreateCheckoutSessionRequestTests
    {
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
            request.MerchantMetadata.CustomInformation = "foo";
            request.MerchantMetadata.MerchantReferenceId = "123";
            request.MerchantMetadata.MerchantStoreName = "myStore";
            request.MerchantMetadata.NoteToBuyer = "myBuyerNote";
            request.ChargePermissionType = ChargePermissionType.Recurring;
            request.RecurringMetadata.Frequency.Unit = FrequencyUnit.Variable;
            request.RecurringMetadata.Frequency.Value = 2;
            request.RecurringMetadata.Amount.Amount = 12.34m;
            request.RecurringMetadata.Amount.CurrencyCode = Currency.USD;
            request.MerchantMetadata.CustomInformation = "foo";
            request.MerchantMetadata.MerchantReferenceId = "123";
            request.MerchantMetadata.MerchantStoreName = "myStore";
            request.MerchantMetadata.NoteToBuyer = "myBuyerNote";
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
            request.MerchantMetadata.CustomInformation = "foo";
            request.MerchantMetadata.MerchantReferenceId = "123";
            request.MerchantMetadata.MerchantStoreName = "myStore";
            request.MerchantMetadata.NoteToBuyer = "myBuyerNote";

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
            request.MerchantMetadata.CustomInformation = "foo";
            request.MerchantMetadata.MerchantReferenceId = "123";
            request.MerchantMetadata.MerchantStoreName = "myStore";
            request.MerchantMetadata.NoteToBuyer = "myBuyerNote";
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


    }
}
