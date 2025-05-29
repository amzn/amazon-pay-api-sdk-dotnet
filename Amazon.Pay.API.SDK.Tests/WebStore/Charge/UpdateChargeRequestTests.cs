using System;
using Amazon.Pay.API.WebStore.Charge;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.Charge
{
    [TestFixture]
    public class UpdateChargeRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitializedAsExpected()
        {
            // arrange
            var lastUpdatedTimestamp = new DateTime(2025, 5, 1, 12, 0, 0, DateTimeKind.Utc);

            // act
            var request = new UpdateChargeRequest("Canceled", "ExpiredUnused", "Charge expired due to usused.", lastUpdatedTimestamp);

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.StatusDetails);
            Assert.AreEqual("Canceled", request.StatusDetails.State);
            Assert.AreEqual("ExpiredUnused", request.StatusDetails.ReasonCode);
            Assert.AreEqual("Charge expired due to usused.", request.StatusDetails.ReasonDescription);
            Assert.AreEqual(lastUpdatedTimestamp, request.StatusDetails.LastUpdatedTimestamp);
        }

        [Test]
        public void CanConstructWithMinimalProperties()
        {
            // act
            var request = new UpdateChargeRequest("Canceled", "ExpiredUnused");

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.StatusDetails);
            Assert.AreEqual("Canceled", request.StatusDetails.State);
            Assert.AreEqual("ExpiredUnused", request.StatusDetails.ReasonCode);
            Assert.IsNull(request.StatusDetails.ReasonDescription);
            Assert.IsNull(request.StatusDetails.LastUpdatedTimestamp);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var request = new UpdateChargeRequest("Canceled", "ExpiredUnused");

            // act
            string json = JsonConvert.SerializeObject(request);
            string expected = "{\"statusDetails\":{\"state\":\"Canceled\",\"reasonCode\":\"ExpiredUnused\"}}";

            // assert
            Assert.AreEqual(expected, json);
        }

        [Test]
        public void CanConvertToJsonFull()
        {
            // arrange
            var lastUpdatedTimestamp = new DateTime(2025, 5, 1, 14, 0, 0, DateTimeKind.Utc);
            var request = new UpdateChargeRequest("Canceled", "ExpiredUnused", "Charge expired due to usused.", lastUpdatedTimestamp);

            // act
            string json = JsonConvert.SerializeObject(request);
            string expected = $"{{\"statusDetails\":{{\"state\":\"Canceled\",\"reasonCode\":\"ExpiredUnused\",\"reasonDescription\":\"Charge expired due to usused.\",\"lastUpdatedTimestamp\":\"{lastUpdatedTimestamp:yyyyMMddTHHmmssZ}\"}}}}";

            // assert
            Assert.AreEqual(expected, json);
        }
    }
}
