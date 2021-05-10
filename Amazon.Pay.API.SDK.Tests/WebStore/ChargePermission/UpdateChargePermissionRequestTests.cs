using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.ChargePermission;
using Amazon.Pay.API.WebStore.Types;
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
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{}", json);

            // verify object hasn't been corrupted
            request.MerchantMetadata.CustomInformation = "bar";
            request.MerchantMetadata.MerchantReferenceId = "345";
            request.MerchantMetadata.MerchantStoreName = "anotherShop";
            request.MerchantMetadata.NoteToBuyer = "mee";
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
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"merchantMetadata\":{\"merchantReferenceId\":\"345\",\"merchantStoreName\":\"anotherShop\",\"noteToBuyer\":\"mee\",\"customInformation\":\"bar\"}}", json);
        }

        [Test]
        public void CanConvertToJsonRecurringMetadata()
        {
            // arrange
            var request = new UpdateChargePermissionRequest();
            request.RecurringMetadata.Frequency.Unit = FrequencyUnit.Month;
            request.RecurringMetadata.Frequency.Value = 3;
            request.RecurringMetadata.Amount.Amount = 23.45m;
            request.RecurringMetadata.Amount.CurrencyCode = Currency.GBP;

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"recurringMetadata\":{\"frequency\":{\"unit\":\"Month\",\"value\":3},\"amount\":{\"amount\":23.45,\"currencyCode\":\"GBP\"}}}", json);

        }

    }
}
