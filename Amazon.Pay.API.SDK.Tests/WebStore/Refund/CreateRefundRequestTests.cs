using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Refund;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.Refund
{
    [TestFixture]
    public class CreateRefundRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitializedAsExpected()
        {
            // arrange
            var chargeId = "S02-7331650-8246451";

            // act
            var request = new CreateRefundRequest(chargeId, 12.99M, Currency.EUR);

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.ChargeId);
            Assert.IsNotNull(request.RefundAmount);
            Assert.AreEqual(12.99, request.RefundAmount.Amount);
            Assert.AreEqual(Currency.EUR, request.RefundAmount.CurrencyCode);
            Assert.IsNull(request.SoftDescriptor);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var chargeId = "S02-7331650-8246451";
            var request = new CreateRefundRequest(chargeId, 12.99M, Currency.EUR);

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"chargeId\":\"S02-7331650-8246451\",\"refundAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"}}", json);
        }

        [Test]
        public void CanConvertToJsonFull()
        {
            // arrange
            var chargeId = "S02-7331650-8246451";
            var request = new CreateRefundRequest(chargeId, 12.99M, Currency.EUR);
            request.SoftDescriptor = "foo";

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"chargeId\":\"S02-7331650-8246451\",\"refundAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"},\"softDescriptor\":\"foo\"}", json);
        }


    }
}
