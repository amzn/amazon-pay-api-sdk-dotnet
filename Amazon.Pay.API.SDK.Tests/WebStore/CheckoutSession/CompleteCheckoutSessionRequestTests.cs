using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.CheckoutSession;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.CheckoutSession
{
    [TestFixture]
    public class CompleteCheckoutSessionRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitialized()
        {
            // act
            var request = new CompleteCheckoutSessionRequest(99, Currency.EUR);

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.ChargeAmount);
            Assert.AreEqual(99, request.ChargeAmount.Amount);
            Assert.AreEqual(Currency.EUR, request.ChargeAmount.CurrencyCode);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var request = new CompleteCheckoutSessionRequest(99, Currency.EUR);

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"chargeAmount\":{\"amount\":99,\"currencyCode\":\"EUR\"}}", json);
        }

        [Test]
        public void CanConvertToJsonMultiship()
        {
            // arrange
            var request = new CompleteCheckoutSessionRequest(99, Currency.EUR);
            request.TotalOrderAmount.Amount = 299.99m;
            request.TotalOrderAmount.CurrencyCode = Currency.EUR;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"chargeAmount\":{\"amount\":99,\"currencyCode\":\"EUR\"},\"totalOrderAmount\":{\"amount\":299.99,\"currencyCode\":\"EUR\"}}", json);
        }
    }
}
