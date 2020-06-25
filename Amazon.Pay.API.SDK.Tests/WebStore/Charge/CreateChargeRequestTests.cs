using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Charge;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.Charge
{
    [TestFixture]
    public class CreateChargeRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitializedAsExpected()
        {
            // arrange
            var chargePermissionId = "S02-7331650-8246451";

            // act
            var request = new CreateChargeRequest(chargePermissionId, 12.99M, Currency.EUR);

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.ChargePermissionId);
            Assert.IsNotNull(request.ChargeAmount);
            Assert.AreEqual(12.99, request.ChargeAmount.Amount);
            Assert.AreEqual(Currency.EUR, request.ChargeAmount.CurrencyCode);
            Assert.IsNotNull(request.ProviderMetadata);
            Assert.IsNull(request.CanHandlePendingAuthorization);
            Assert.IsNull(request.CaptureNow);
            Assert.IsNull(request.SoftDescriptor);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var chargePermissionId = "S02-7331650-8246451";
            var request = new CreateChargeRequest(chargePermissionId, 12.99M, Currency.EUR);

            // act
            string json = request.ToJson();

            // assert
            Assert.AreEqual("{\"chargePermissionId\":\"S02-7331650-8246451\",\"chargeAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"}}", json);
        }

        [Test]
        public void CanConvertToJsonFull()
        {
            // arrange
            var chargePermissionId = "S02-7331650-8246451";
            var request = new CreateChargeRequest(chargePermissionId, 12.99M, Currency.EUR);
            request.ProviderMetadata.ProviderReferenceId = "foo";
            request.SoftDescriptor = "foo";
            request.CaptureNow = true;
            request.CanHandlePendingAuthorization = true;

            // act
            string json = request.ToJson();

            // assert
            Assert.AreEqual("{\"chargePermissionId\":\"S02-7331650-8246451\",\"chargeAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"},\"captureNow\":true,\"softDescriptor\":\"foo\",\"canHandlePendingAuthorization\":true,\"providerMetadata\":{\"providerReferenceId\":\"foo\"}}", json);
        }


    }
}
