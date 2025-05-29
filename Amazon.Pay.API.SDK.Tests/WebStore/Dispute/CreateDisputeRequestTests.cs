using System;
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Dispute;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.Dispute
{
    [TestFixture]
    public class CreateDisputeRequestTests
    {
        [Test]
        public void CanConstructWithMinimalRequiredProperties()
        {
            // act
            var request = new CreateDisputeRequest("P01-1111111-1111111-C123456", 49.99M, Currency.USD);

            // assert
            Assert.IsNotNull(request);
            Assert.AreEqual("P01-1111111-1111111-C123456", request.ChargeId);
            Assert.IsNotNull(request.DisputeAmount);
            Assert.AreEqual(49.99M, request.DisputeAmount.Amount);
            Assert.AreEqual(Currency.USD, request.DisputeAmount.CurrencyCode);
            Assert.IsNull(request.FilingReason);
            Assert.AreEqual(default(DateTime), request.FilingTimestamp);
            Assert.IsNull(request.StatusDetails);
            Assert.AreEqual(default(DateTime), request.MerchantResponseDeadline);
            Assert.IsNull(request.ProviderMetadata);
        }

        [Test]
        public void CanConstructWithAllPropertiesInitializedAsExpected()
        {
            // arrange
            var filingTimestamp = new DateTime(2025, 5, 2, 9, 0, 0, DateTimeKind.Utc);
            var merchantDeadline = new DateTime(2025, 5, 10, 23, 59, 0, DateTimeKind.Utc);
            var statusDetails = new StatusDetailsRequest
            {
                State = "ActionRequired",
                ReasonCode = "MerchantResponseRequired",
                ReasonDescription = "Charge disputed due to suspected fraud",
                LastUpdatedTimestamp = filingTimestamp
            };
            var providerMetadata = new ProviderMetadata
            {
                ProviderReferenceId = "provider-abc-456"
            };

            // act
            var request = new CreateDisputeRequest(
                "P01-1111111-1111111-C123456",
                85.50M,
                Currency.GBP,
                "Fraudulent",
                filingTimestamp,
                statusDetails,
                merchantDeadline,
                providerMetadata
            );

            // assert
            Assert.IsNotNull(request);
            Assert.AreEqual("P01-1111111-1111111-C123456", request.ChargeId);
            Assert.AreEqual(85.50M, request.DisputeAmount.Amount);
            Assert.AreEqual(Currency.GBP, request.DisputeAmount.CurrencyCode);
            Assert.AreEqual("Fraudulent", request.FilingReason);
            Assert.AreEqual(filingTimestamp, request.FilingTimestamp);
            Assert.AreEqual(statusDetails, request.StatusDetails);
            Assert.AreEqual(merchantDeadline, request.MerchantResponseDeadline);
            Assert.AreEqual(providerMetadata, request.ProviderMetadata);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // Arrange
            var filingTimestamp = new DateTime(2025, 5, 2, 12, 30, 0, DateTimeKind.Utc);
            var merchantDeadline = new DateTime(2025, 5, 5, 17, 0, 0, DateTimeKind.Utc);

            var disputeRequest = new CreateDisputeRequest(
                chargeId: "P01-1111111-1111111-C123456",
                chargeAmount: 15.50M,
                currencyCode: Currency.EUR
            )
            {
                // Manually add other optional properties after object creation
                ProviderMetadata = new ProviderMetadata
                {
                    ProviderDisputeId = "A2CQXQ2TUSYG7J"
                },
                FilingReason = "ProductNotReceived",
                FilingTimestamp = filingTimestamp, // Set fixed datetime for consistency
                MerchantResponseDeadline = merchantDeadline, // Set fixed deadline
                StatusDetails = new StatusDetailsRequest
                {
                    State = "ActionRequired",
                    ReasonCode = "MerchantResponseRequired"
                }
            };

            // Act
            string json = JsonConvert.SerializeObject(disputeRequest);

            // Assert basic fields presence
            StringAssert.Contains("\"chargeId\":\"P01-1111111-1111111-C123456\"", json);
            StringAssert.Contains("\"amount\":15.50", json);
            StringAssert.Contains("\"currencyCode\":\"EUR\"", json);
            StringAssert.Contains("\"filingReason\":\"ProductNotReceived\"", json);
            StringAssert.Contains("\"filingTimestamp\":\"20250502T123000Z\"", json);
            StringAssert.Contains("\"merchantResponseDeadline\":\"20250505T170000Z\"", json);
            StringAssert.Contains("\"providerDisputeId\":\"A2CQXQ2TUSYG7J\"", json);
            StringAssert.Contains("\"state\":\"ActionRequired\"", json);
            StringAssert.Contains("\"reasonCode\":\"MerchantResponseRequired\"", json);
        }

        [Test]
        public void CanConvertToJsonFull()
        {
            // arrange
            var filingTimestamp = new DateTime(2025, 5, 2, 12, 30, 0, DateTimeKind.Utc);
            var merchantDeadline = new DateTime(2025, 5, 5, 17, 0, 0, DateTimeKind.Utc);
            var statusDetails = new StatusDetailsRequest
            {
                State = "ActionRequired",
                ReasonCode = "MerchantResponseRequired",
                ReasonDescription = "Customer did not receive the item",
                LastUpdatedTimestamp = filingTimestamp
            };
            var providerMetadata = new ProviderMetadata
            {
                ProviderReferenceId = "ext-ref-999"
            };

            var request = new CreateDisputeRequest(
                "P01-1111111-1111111-C123456",
                22.75M,
                Currency.USD,
                "Fraudulent",
                filingTimestamp,
                statusDetails,
                merchantDeadline,
                providerMetadata
            );

            // act
            string json = JsonConvert.SerializeObject(request);

            // assert basic fields presence
            StringAssert.Contains("\"chargeId\":\"P01-1111111-1111111-C123456\"", json);
            StringAssert.Contains("\"amount\":22.75", json);
            StringAssert.Contains("\"currencyCode\":\"USD\"", json);
            StringAssert.Contains("\"filingReason\":\"Fraudulent\"", json);
            StringAssert.Contains("\"filingTimestamp\":\"20250502T123000Z\"", json);
            StringAssert.Contains("\"merchantResponseDeadline\":\"20250505T170000Z\"", json);
            StringAssert.Contains("\"providerReferenceId\":\"ext-ref-999\"", json);
            StringAssert.Contains("\"state\":\"ActionRequired\"", json);
        }
    }
}