using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Charge;
using Amazon.Pay.API.WebStore.Types;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.Charge
{
    [TestFixture]
    public class CreateChargeRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitializedAsExpected()
        {
            // Arrange
            var chargePermissionId = "S02-7331650-8246451";

            // Act
            var request = new CreateChargeRequest(chargePermissionId, 12.99M, Currency.EUR);

            // Assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.ChargePermissionId);
            Assert.IsNotNull(request.ChargeAmount);
            Assert.AreEqual(12.99, request.ChargeAmount.Amount);
            Assert.AreEqual(Currency.EUR, request.ChargeAmount.CurrencyCode);
            Assert.IsNotNull(request.ProviderMetadata);
            Assert.IsNotNull(request.MerchantMetadata);
            Assert.IsNull(request.CanHandlePendingAuthorization);
            Assert.IsNull(request.CaptureNow);
            Assert.IsNull(request.SoftDescriptor);
            Assert.IsNull(request.PlatformId);
            Assert.IsNull(request.ChargeInitiator);
            Assert.IsNull(request.Channel);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // Arrange
            var chargePermissionId = "S02-7331650-8246451";
            var request = new CreateChargeRequest(chargePermissionId, 12.99M, Currency.EUR);  
            // Act
            string actualJson = request.ToJson();
            
            // Expected JSON string
            string expectedJson = "{\"chargePermissionId\":\"S02-7331650-8246451\",\"chargeAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"}}";
            
            // Assert
            Assert.AreEqual(actualJson, expectedJson);

            // Verify object hasn't been corrupted
            request.ProviderMetadata.ProviderReferenceId = "foo";
            request.SoftDescriptor = "foo";
            request.CaptureNow = true;
            request.PlatformId = "My Platform Id";
            request.CanHandlePendingAuthorization = true;
            request.MerchantMetadata.MerchantReferenceId = "123abc!";
            request.MerchantMetadata.MerchantStoreName = "My Store Name";
            request.MerchantMetadata.NoteToBuyer = "My Note to Buyer";
            request.MerchantMetadata.CustomInformation = "My Custom Info";
        }

        [Test]
        public void CanConvertToJsonFull()
        {
            // Arrange
            var chargePermissionId = "S02-7331650-8246451";
            var request = new CreateChargeRequest(chargePermissionId, 12.99M, Currency.EUR)
                {
                    ProviderMetadata = new ProviderMetadata { ProviderReferenceId = "foo1" },
                    SoftDescriptor = "foo2",
                    CaptureNow = true,
                    PlatformId = "My Platform Id",
                    CanHandlePendingAuthorization = true
                };

             // Act
            string actualJson = request.ToJson();
            
            // Expected JSON string
            string expectedJson = "{\"chargePermissionId\":\"S02-7331650-8246451\",\"chargeAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"},\"captureNow\":true,\"softDescriptor\":\"foo2\",\"platformId\":\"My Platform Id\",\"canHandlePendingAuthorization\":true,\"providerMetadata\":{\"providerReferenceId\":\"foo1\"}}";
            
            // Assert
            Assert.AreEqual(actualJson, expectedJson);
        }

        [Test]
        public void CanConvertToJsonRecurring()
        {
            // Arrange
            var chargePermissionId = "S02-7331650-8246451";
            var request = new CreateChargeRequest(chargePermissionId, 12.99M, Currency.EUR)
                {
                    ProviderMetadata = new ProviderMetadata { ProviderReferenceId = "foo1" },
                    SoftDescriptor = "foo2",
                    CaptureNow = true,
                    PlatformId = "My Platform Id",
                    CanHandlePendingAuthorization = true,
                    MerchantMetadata = new MerchantMetadata 
                        { 
                            MerchantReferenceId = "123abc!",
                            MerchantStoreName = "My Store Name",
                            NoteToBuyer = "My Note to Buyer",
                            CustomInformation = "My Custom Info",
                        }
                };

            // Act
            string actualJson = request.ToJson();
            
            // Expected JSON string
            string expectedJson = "{\"chargePermissionId\":\"S02-7331650-8246451\",\"chargeAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"},\"captureNow\":true,\"softDescriptor\":\"foo2\",\"platformId\":\"My Platform Id\",\"canHandlePendingAuthorization\":true,\"providerMetadata\":{\"providerReferenceId\":\"foo1\"},\"merchantMetadata\":{\"merchantReferenceId\":\"123abc!\",\"merchantStoreName\":\"My Store Name\",\"noteToBuyer\":\"My Note to Buyer\",\"customInformation\":\"My Custom Info\"}}";
            
            // Assert
            Assert.AreEqual(actualJson, expectedJson);
        }

        [Test]
        public void CanConvertToJsonPaymentMethodOnFile()
        {
            // Arrange
            var chargePermissionId = "S02-7331650-8246451";
            var request = new CreateChargeRequest(chargePermissionId, 12.99M, Currency.EUR)
                {
                    ProviderMetadata = new ProviderMetadata { ProviderReferenceId = "foo" },
                    SoftDescriptor = "foo",
                    CaptureNow = true,
                    PlatformId = "My Platform Id",
                    CanHandlePendingAuthorization = true,
                    ChargeInitiator = ChargeInitiator.CITR,
                    Channel = Channel.Web,
                };

            // Act
            string actualJson = request.ToJson();
            
            // Expected JSON string
            string expectedJson = "{\"chargePermissionId\":\"S02-7331650-8246451\",\"chargeAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"},\"captureNow\":true,\"softDescriptor\":\"foo\",\"platformId\":\"My Platform Id\",\"canHandlePendingAuthorization\":true,\"providerMetadata\":{\"providerReferenceId\":\"foo\"},\"chargeInitiator\":\"CITR\",\"channel\":\"Web\"}";

            // Assert
            Assert.AreEqual(actualJson, expectedJson);
        }

        [Test]
        public void CanConvertToJsonSavedWallet()
        {
            // Arrange
            var chargePermissionId = "S01-7436918-9739892";
            var checkoutResultReturnUrl = "https://example.com/return.html";
            var request = new CreateChargeRequest(chargePermissionId, 99.99M, Currency.EUR, checkoutResultReturnUrl)
                {
                    ProviderMetadata = new ProviderMetadata { ProviderReferenceId = "foo" },
                    SoftDescriptor = "foo",
                    CaptureNow = true,
                    PlatformId = "My Platform Id",
                    CanHandlePendingAuthorization = true,
                    ChargeInitiator = ChargeInitiator.CITR,
                    Channel = Channel.Web
                };

            // Act
            string actualJson = request.ToJson();
            
            // Expected JSON string
            string expectedJson = "{\"chargePermissionId\":\"S01-7436918-9739892\",\"chargeAmount\":{\"amount\":99.99,\"currencyCode\":\"EUR\"},\"captureNow\":true,\"softDescriptor\":\"foo\",\"platformId\":\"My Platform Id\",\"canHandlePendingAuthorization\":true,\"providerMetadata\":{\"providerReferenceId\":\"foo\"},\"chargeInitiator\":\"CITR\",\"channel\":\"Web\",\"webCheckoutDetails\":{\"checkoutResultReturnUrl\":\"https://example.com/return.html\"}}";

            // Assert
            Assert.AreEqual(actualJson, expectedJson);
        }
    }
}
