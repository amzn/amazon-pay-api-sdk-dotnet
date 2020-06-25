using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Types;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.CheckoutSession
{
    [TestFixture]
    public class UpdateCheckoutSessionRequestTests
    {
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

            // assert
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

            // assert
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

            // assert
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

            // assert
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

            // assert
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

            // assert
            Assert.AreEqual("{\"supplementaryData\":\"foo\"}", json);
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

            // act
            string json = request.ToJson();

            // assert
            Assert.AreEqual("{\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://example.com/review.html\",\"checkoutResultReturnUrl\":\"https://example.com/thankyou.html\"},\"paymentDetails\":{\"paymentIntent\":\"Authorize\",\"chargeAmount\":{\"amount\":1080,\"currencyCode\":\"EUR\"}},\"merchantMetadata\":{\"merchantReferenceId\":\"123\",\"merchantStoreName\":\"myStore\",\"noteToBuyer\":\"myBuyerNote\",\"customInformation\":\"foo\"},\"supplementaryData\":\"foo\",\"providerMetadata\":{\"providerReferenceId\":\"foo\"}}", json);
        }
    }
}
