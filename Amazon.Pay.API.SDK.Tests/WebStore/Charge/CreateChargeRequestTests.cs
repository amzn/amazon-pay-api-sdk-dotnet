﻿using Amazon.Pay.API.Types;
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
            Assert.IsNotNull(request.MerchantMetadata);
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
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"chargePermissionId\":\"S02-7331650-8246451\",\"chargeAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"}}", json);

            // verify object hasn't been corrupted
            request.ProviderMetadata.ProviderReferenceId = "foo";
            request.SoftDescriptor = "foo";
            request.CaptureNow = true;
            request.CanHandlePendingAuthorization = true;
            request.MerchantMetadata.MerchantReferenceId = "123abc!";
            request.MerchantMetadata.MerchantStoreName = "My Store Name";
            request.MerchantMetadata.NoteToBuyer = "My Note to Buyer";
            request.MerchantMetadata.CustomInformation = "My Custom Info";
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
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"chargePermissionId\":\"S02-7331650-8246451\",\"chargeAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"},\"captureNow\":true,\"softDescriptor\":\"foo\",\"canHandlePendingAuthorization\":true,\"providerMetadata\":{\"providerReferenceId\":\"foo\"}}", json);
        }

        [Test]
        public void CanConvertToJsonRecurring()
        {
            // arrange
            var chargePermissionId = "S02-7331650-8246451";
            var request = new CreateChargeRequest(chargePermissionId, 12.99M, Currency.EUR);
            request.ProviderMetadata.ProviderReferenceId = "foo1";
            request.SoftDescriptor = "foo2";
            request.CaptureNow = true;
            request.CanHandlePendingAuthorization = true;
            request.MerchantMetadata.MerchantReferenceId = "123abc!";
            request.MerchantMetadata.MerchantStoreName = "My Store Name";
            request.MerchantMetadata.NoteToBuyer = "My Note to Buyer";
            request.MerchantMetadata.CustomInformation = "My Custom Info";

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"chargePermissionId\":\"S02-7331650-8246451\",\"chargeAmount\":{\"amount\":12.99,\"currencyCode\":\"EUR\"},\"captureNow\":true,\"softDescriptor\":\"foo2\",\"canHandlePendingAuthorization\":true,\"providerMetadata\":{\"providerReferenceId\":\"foo1\"},\"merchantMetadata\":{\"merchantReferenceId\":\"123abc!\",\"merchantStoreName\":\"My Store Name\",\"noteToBuyer\":\"My Note to Buyer\",\"customInformation\":\"My Custom Info\"}}", json);
        }


    }
}
