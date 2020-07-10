using Amazon.Pay.API.WebStore.Charge;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.Charge
{
    [TestFixture]
    public class CancelChargeRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitializedAsExpected()
        {
            // act
            var request = new CancelChargeRequest("foo");

            // assert
            Assert.IsNotNull(request);
            Assert.AreEqual("foo", request.CancellationReason);
        }

        [Test]
        public void CanConvertToJson()
        {
            // arrange
            var request = new CancelChargeRequest("foo");

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"cancellationReason\":\"foo\"}", json);
        }
    }
}
