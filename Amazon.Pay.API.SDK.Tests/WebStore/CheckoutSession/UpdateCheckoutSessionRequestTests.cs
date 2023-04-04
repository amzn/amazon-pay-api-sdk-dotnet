using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Types;
using System.Collections.Generic;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.CheckoutSession
{
    [TestFixture]
    public class UpdateCheckoutSessionRequestTests
    {
        private readonly List<MultiAddressesCheckoutMetadata> multiAddressesCheckoutMetadataToTest = new List<MultiAddressesCheckoutMetadata>()
        {
            new MultiAddressesCheckoutMetadata() { AddressId = "amzn1.address.ABC", Price = new Price(33, Currency.JPY) },
            new MultiAddressesCheckoutMetadata() { AddressId = "amzn1.address.DEF", Price = new Price(33, Currency.JPY) }
        };

        [Test]
        public void CanConstructWithAllPropertiesInitialized()
        {
            // act
            var request = new UpdateCheckoutSessionRequest();

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.WebCheckoutDetails);
            Assert.IsNotNull(request.MerchantMetadata);
            Assert.IsNotNull(request.PaymentDetails);
            Assert.IsNotNull(request.ProviderMetadata);
            Assert.IsNotNull(request.RecurringMetadata);
            Assert.IsNotNull(request.MultiAddressesCheckoutMetadataList);
            Assert.IsNull(request.PlatformId);
            Assert.IsNull(request.SupplementaryData);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{}", json);
        }

        [Test]
        public void CanConvertToJsonWebCheckoutDetails()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();
            request.WebCheckoutDetails.CheckoutResultReturnUrl = "https://example.com/thankyou.html";
            request.WebCheckoutDetails.CheckoutReviewReturnUrl = "https://example.com/review.html";

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\",\"checkoutResultReturnUrl\":\"https://example.com/thankyou.html\"}}", json);
        }

        [Test]
        public void CanConvertToJsonMerchantMetaData()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();
            request.MerchantMetadata.CustomInformation = "foo";
            request.MerchantMetadata.MerchantReferenceId = "123";
            request.MerchantMetadata.MerchantStoreName = "myStore";
            request.MerchantMetadata.NoteToBuyer = "myBuyerNote";

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"merchantMetadata\":{\"merchantReferenceId\":\"123\",\"merchantStoreName\":\"myStore\",\"noteToBuyer\":\"myBuyerNote\",\"customInformation\":\"foo\"}}", json);
        }

        [Test]
        public void CanConvertToJsonPaymentDetails()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();
            request.PaymentDetails.ChargeAmount.Amount = 1080M;
            request.PaymentDetails.ChargeAmount.CurrencyCode = Currency.EUR;
            request.PaymentDetails.PaymentIntent = PaymentIntent.Authorize;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"paymentDetails\":{\"paymentIntent\":\"Authorize\",\"chargeAmount\":{\"amount\":1080,\"currencyCode\":\"EUR\"}}}", json);
        }

        [Test]
        public void CanConvertToJsonProviderMetadata()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();
            request.ProviderMetadata.ProviderReferenceId = "foo";

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"providerMetadata\":{\"providerReferenceId\":\"foo\"}}", json);
        }

        [Test]
        public void CanConvertToJsonSupplementaryData()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();
            request.SupplementaryData = "foo";

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"supplementaryData\":\"foo\"}", json);
        }

        [Test]
        public void CanConvertToJsonWithCancelUrl()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();
            request.WebCheckoutDetails.CheckoutCancelUrl = "https://example.com/cancel.html";;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"webCheckoutDetails\":{\"checkoutCancelUrl\":\"https://example.com/cancel.html\"}}", json);
        }

        [Test]
        public void CanConvertToJsonFull()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();
            request.SupplementaryData = "foo";
            request.WebCheckoutDetails.CheckoutResultReturnUrl = "https://example.com/thankyou.html";
            request.WebCheckoutDetails.CheckoutReviewReturnUrl = "https://example.com/review.html";
            request.MerchantMetadata.CustomInformation = "foo";
            request.MerchantMetadata.MerchantReferenceId = "123";
            request.MerchantMetadata.MerchantStoreName = "myStore";
            request.MerchantMetadata.NoteToBuyer = "myBuyerNote";
            request.PaymentDetails.ChargeAmount.Amount = 1080M;
            request.PaymentDetails.ChargeAmount.CurrencyCode = Currency.EUR;
            request.PaymentDetails.PaymentIntent = PaymentIntent.Authorize;
            request.ProviderMetadata.ProviderReferenceId = "foo";
            request.SupplementaryData = "foo";
            request.MultiAddressesCheckoutMetadataList = multiAddressesCheckoutMetadataToTest;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\",\"checkoutResultReturnUrl\":\"https://example.com/thankyou.html\"},\"paymentDetails\":{\"paymentIntent\":\"Authorize\",\"chargeAmount\":{\"amount\":1080,\"currencyCode\":\"EUR\"}},\"merchantMetadata\":{\"merchantReferenceId\":\"123\",\"merchantStoreName\":\"myStore\",\"noteToBuyer\":\"myBuyerNote\",\"customInformation\":\"foo\"},\"supplementaryData\":\"foo\",\"providerMetadata\":{\"providerReferenceId\":\"foo\"},\"multiAddressesCheckoutMetadata\":[{\"addressId\":\"amzn1.address.ABC\",\"price\":{\"amount\":33,\"currencyCode\":\"JPY\"}},{\"addressId\":\"amzn1.address.DEF\",\"price\":{\"amount\":33,\"currencyCode\":\"JPY\"}}]}", json);
        }

        [Test]
        public void CanConvertToJsonRecurring()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();
            request.SupplementaryData = "foo1";
            request.WebCheckoutDetails.CheckoutResultReturnUrl = "https://example.com/thankyou.html";
            request.WebCheckoutDetails.CheckoutReviewReturnUrl = "https://example.com/review.html";
            request.MerchantMetadata.CustomInformation = "foo2";
            request.MerchantMetadata.MerchantReferenceId = "123";
            request.MerchantMetadata.MerchantStoreName = "myStore";
            request.MerchantMetadata.NoteToBuyer = "myBuyerNote";
            request.RecurringMetadata.Frequency.Unit = FrequencyUnit.Month;
            request.RecurringMetadata.Frequency.Value = 3;
            request.RecurringMetadata.Amount.Amount = 23.45m;
            request.RecurringMetadata.Amount.CurrencyCode = Currency.GBP;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\",\"checkoutResultReturnUrl\":\"https://example.com/thankyou.html\"},\"merchantMetadata\":{\"merchantReferenceId\":\"123\",\"merchantStoreName\":\"myStore\",\"noteToBuyer\":\"myBuyerNote\",\"customInformation\":\"foo2\"},\"supplementaryData\":\"foo1\",\"recurringMetadata\":{\"frequency\":{\"unit\":\"Month\",\"value\":3},\"amount\":{\"amount\":23.45,\"currencyCode\":\"GBP\"}}}", json);
        }

        [Test]
        public void CanConvertToJsonMultiship()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();
            request.SupplementaryData = "foo";
            request.WebCheckoutDetails.CheckoutResultReturnUrl = "https://example.com/thankyou.html";
            request.WebCheckoutDetails.CheckoutReviewReturnUrl = "https://example.com/review.html";
            request.MerchantMetadata.CustomInformation = "foo";
            request.MerchantMetadata.MerchantReferenceId = "123";
            request.MerchantMetadata.MerchantStoreName = "myStore";
            request.MerchantMetadata.NoteToBuyer = "myBuyerNote";
            request.PaymentDetails.ChargeAmount.Amount = 1080M;
            request.PaymentDetails.ChargeAmount.CurrencyCode = Currency.EUR;
            request.PaymentDetails.PaymentIntent = PaymentIntent.Authorize;
            request.PaymentDetails.TotalOrderAmount.Amount = 1234.56m;
            request.PaymentDetails.TotalOrderAmount.CurrencyCode = Currency.EUR;
            request.PaymentDetails.AllowOvercharge = true;
            request.PaymentDetails.ExtendExpiration = true;
            request.ProviderMetadata.ProviderReferenceId = "foo";
            request.SupplementaryData = "foo";

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\",\"checkoutResultReturnUrl\":\"https://example.com/thankyou.html\"},\"paymentDetails\":{\"paymentIntent\":\"Authorize\",\"chargeAmount\":{\"amount\":1080,\"currencyCode\":\"EUR\"},\"totalOrderAmount\":{\"amount\":1234.56,\"currencyCode\":\"EUR\"},\"allowOvercharge\":true,\"extendExpiration\":true},\"merchantMetadata\":{\"merchantReferenceId\":\"123\",\"merchantStoreName\":\"myStore\",\"noteToBuyer\":\"myBuyerNote\",\"customInformation\":\"foo\"},\"supplementaryData\":\"foo\",\"providerMetadata\":{\"providerReferenceId\":\"foo\"}}", json);
        }

        [Test]
        public void CanConvertToJsonMultiAddressesCheckoutMetadataList()
        {
            // arrange
            var request = new UpdateCheckoutSessionRequest();
            request.WebCheckoutDetails.CheckoutResultReturnUrl = "https://example.com/thankyou.html";
            request.WebCheckoutDetails.CheckoutReviewReturnUrl = "https://example.com/review.html";
            request.MerchantMetadata.CustomInformation = "foo";
            request.MerchantMetadata.MerchantReferenceId = "123";
            request.MerchantMetadata.MerchantStoreName = "myStore";
            request.MerchantMetadata.NoteToBuyer = "myBuyerNote";
            request.MultiAddressesCheckoutMetadataList = multiAddressesCheckoutMetadataToTest;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\",\"checkoutResultReturnUrl\":\"https://example.com/thankyou.html\"},\"merchantMetadata\":{\"merchantReferenceId\":\"123\",\"merchantStoreName\":\"myStore\",\"noteToBuyer\":\"myBuyerNote\",\"customInformation\":\"foo\"},\"multiAddressesCheckoutMetadata\":[{\"addressId\":\"amzn1.address.ABC\",\"price\":{\"amount\":33,\"currencyCode\":\"JPY\"}},{\"addressId\":\"amzn1.address.DEF\",\"price\":{\"amount\":33,\"currencyCode\":\"JPY\"}}]}", json);
        }

    }
}
