using Amazon.Pay.API.WebStore.ChargePermission;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.ChargePermission
{
    [TestFixture]
    public class UpdateChargePermissionRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitialized()
        {
            // act
            var request = new UpdateChargePermissionRequest();

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.MerchantMetadata);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var request = new UpdateChargePermissionRequest();

            // act
            string json = request.ToJson();

            // assert
            Assert.AreEqual("{}", json);
        }

        [Test]
        public void CanConvertToJsonMerchantMetadata()
        {
            // arrange
            var request = new UpdateChargePermissionRequest();
            request.MerchantMetadata.CustomInformation = "bar";
            request.MerchantMetadata.MerchantReferenceId = "345";
            request.MerchantMetadata.MerchantStoreName = "anotherShop";
            request.MerchantMetadata.NoteToBuyer = "mee";

            // act
            string json = request.ToJson();

            // assert
            Assert.AreEqual("{\"merchantMetadata\":{\"merchantReferenceId\":\"345\",\"merchantStoreName\":\"anotherShop\",\"noteToBuyer\":\"mee\",\"customInformation\":\"bar\"}}", json);
        }
    }
}
