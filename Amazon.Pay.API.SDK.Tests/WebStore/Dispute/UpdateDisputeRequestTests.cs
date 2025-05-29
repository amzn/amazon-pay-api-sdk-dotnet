using System;
using Amazon.Pay.API.WebStore.Dispute;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.Dispute
{
    [TestFixture]
    public class UpdateDisputeRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitializedAsExpected()
        {
            // arrange
            var closureTimestamp = new DateTime(2025, 5, 1, 10, 0, 0, DateTimeKind.Utc);
            var lastUpdatedTimestamp = new DateTime(2025, 4, 30, 15, 30, 0, DateTimeKind.Utc);

            // act
            var request = new UpdateDisputeRequest(
                state: "Resolved",
                reasonCode: "MerchantAcceptedDispute",
                closureTimestamp: closureTimestamp,
                reasonDescription: "Issue resolved with customer",
                resolution: "BuyerWon",
                lastUpdatedTimestamp: lastUpdatedTimestamp
            );

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.StatusDetails);
            Assert.AreEqual("Resolved", request.StatusDetails.State);
            Assert.AreEqual("MerchantAcceptedDispute", request.StatusDetails.ReasonCode);
            Assert.AreEqual("Issue resolved with customer", request.StatusDetails.ReasonDescription);
            Assert.AreEqual("BuyerWon", request.StatusDetails.Resolution);
            Assert.AreEqual(lastUpdatedTimestamp, request.StatusDetails.LastUpdatedTimestamp);
            Assert.AreEqual(closureTimestamp, request.ClosureTimestamp);
        }

        [Test]
        public void CanConstructWithMinimalRequiredProperties()
        {
            // arrange
            var closureTimestamp = new DateTime(2025, 5, 1, 12, 0, 0, DateTimeKind.Utc);

            // act
            var request = new UpdateDisputeRequest("Closed", "InvestigatorResolved", closureTimestamp);

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.StatusDetails);
            Assert.AreEqual("Closed", request.StatusDetails.State);
            Assert.AreEqual("InvestigatorResolved", request.StatusDetails.ReasonCode);
            Assert.IsNull(request.StatusDetails.ReasonDescription);
            Assert.IsNull(request.StatusDetails.Resolution);
            Assert.IsNull(request.StatusDetails.LastUpdatedTimestamp);
            Assert.AreEqual(closureTimestamp, request.ClosureTimestamp);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var closureTimestamp = new DateTime(2025, 5, 1, 12, 0, 0, DateTimeKind.Utc);
            var request = new UpdateDisputeRequest("Closed", "InvestigatorResolved", closureTimestamp);

            // act
            string json = JsonConvert.SerializeObject(request);
            string expected = $"{{\"statusDetails\":{{\"state\":\"Closed\",\"reasonCode\":\"InvestigatorResolved\"}},\"closureTimestamp\":\"{closureTimestamp:yyyyMMddTHHmmssZ}\"}}";

            // assert
            Assert.AreEqual(expected, json);
        }

        [Test]
        public void CanConvertToJsonFull()
        {
            // arrange
            var closureTimestamp = new DateTime(2025, 5, 1, 12, 0, 0, DateTimeKind.Utc);
            var lastUpdatedTimestamp = new DateTime(2025, 4, 30, 9, 15, 0, DateTimeKind.Utc);
            var request = new UpdateDisputeRequest("Resolved", "MerchantAcceptedDispute", closureTimestamp, "Issue resolved with customer", "BuyerWon", lastUpdatedTimestamp);

            // act
            string json = JsonConvert.SerializeObject(request);
            string expected = $"{{\"statusDetails\":{{\"state\":\"Resolved\",\"reasonCode\":\"MerchantAcceptedDispute\",\"reasonDescription\":\"Issue resolved with customer\",\"resolution\":\"BuyerWon\",\"lastUpdatedTimestamp\":\"{lastUpdatedTimestamp:yyyyMMddTHHmmssZ}\"}},\"closureTimestamp\":\"{closureTimestamp:yyyyMMddTHHmmssZ}\"}}";

            // assert
            Assert.AreEqual(expected, json);
        }
    }
}
