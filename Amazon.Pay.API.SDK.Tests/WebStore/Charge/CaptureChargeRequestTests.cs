using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Charge;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.Charge
{
    [TestFixture]
    public class CaptureChargeRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitializedAsExpected()
        {
            // act
            var request = new CaptureChargeRequest(12.99M, Currency.EUR);

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.CaptureAmount);
            Assert.AreEqual(12.99, request.CaptureAmount.Amount);
            Assert.IsNull(request.SoftDescriptor);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var request = new CaptureChargeRequest(12.99M, Currency.EUR);

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"captureAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"}}", json);
        }

        [Test]
        public void CanConvertToJsonFull()
        {
            // arrange
            var request = new CaptureChargeRequest(12.99M, Currency.EUR);
            request.SoftDescriptor = "foo";

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"captureAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"},\"softDescriptor\":\"foo\"}", json);
        }


    }
}
