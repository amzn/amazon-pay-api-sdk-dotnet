using Amazon.Pay.API.WebStore.ChargePermission;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.ChargePermission
{
    [TestFixture]
    public class CloseChargePermissionRequestTests
    {
        [Test]
        public void CanConstruct()
        {
            // act
            var request = new CloseChargePermissionRequest("foo");

            // assert
            Assert.IsNotNull(request);
            Assert.IsNull(request.CancelPendingCharges);
            Assert.AreEqual("foo", request.ClosureReason);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var request = new CloseChargePermissionRequest("foo");

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"closureReason\":\"foo\"}", json);
        }

        [Test]
        public void CanConvertToJsonMerchantMetadata()
        {
            // arrange
            var request = new CloseChargePermissionRequest("foo");
            request.CancelPendingCharges = true;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"closureReason\":\"foo\",\"cancelPendingCharges\":true}", json);
        }


    }
}
