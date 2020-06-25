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

            // assert
            Assert.AreEqual("{\"chargeAmount\":{\"amount\":99,\"currencyCode\":\"EUR\"}}", json);
        }
    }
}
