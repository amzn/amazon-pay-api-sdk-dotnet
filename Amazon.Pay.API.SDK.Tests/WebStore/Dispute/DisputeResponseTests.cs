using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Dispute;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.Tests.WebStore.Dispute
{
    [TestFixture]
    public class DisputeResponseTests
    {

        [Test]
        public void CanDeserializeDisputeResponse()
        {
            // Arrange
            string json = @"
    {
        ""disputeId"": ""S01-1111111-1111111-B123456"",
        ""chargeId"": ""S01-1111111-1111111-C123456"",
        ""disputeAmount"": {
            ""amount"": 35.50,
            ""currencyCode"": ""USD""
        },
        ""disputeType"": ""Chargeback"",
        ""filingReason"": ""ProductNotReceived"",
        ""resolutionAuthority"": ""PSP"",
        ""merchantResponseDeadline"": ""2025-05-10T17:00:00Z"",
        ""statusDetails"": {
            ""state"": ""ActionRequired"",
            ""reasonCode"": ""MerchantResponseRequired"",
            ""reasonDescription"": ""Customer filed a chargeback"",
            ""lastUpdatedTimestamp"": ""2025-05-02T12:30:00Z""
        },
        ""providerMetadata"": {
            ""providerDisputeId"": ""PD-98765""
        },
        ""releaseEnvironment"": ""Sandbox"",
        ""merchantEvidences"": [
            {
                ""evidenceType"": ""TrackingNumber"",
                ""fileId"": ""customer_signature_file_id"",
                ""evidenceText"": null
            }
        ]
    }";

            // Act
            var dispute = JsonConvert.DeserializeObject<DisputeResponse>(json);

            // Assert
            Assert.AreEqual("S01-1111111-1111111-B123456", dispute.DisputeId);
            Assert.AreEqual("S01-1111111-1111111-C123456", dispute.ChargeId);
            Assert.AreEqual(35.50m, dispute.DisputeAmount.Amount);
            Assert.AreEqual(Currency.USD, dispute.DisputeAmount.CurrencyCode);
            Assert.AreEqual("Chargeback", dispute.DisputeType);
            Assert.AreEqual("PSP", dispute.ResolutionAuthority);
            Assert.AreEqual("MerchantResponseRequired", dispute.StatusDetails.ReasonCode);
            Assert.AreEqual("PD-98765", dispute.ProviderMetadata.ProviderDisputeId);
            Assert.AreEqual("Sandbox", dispute.ReleaseEnvironment);
            Assert.AreEqual(1, dispute.MerchantEvidences.Count);
            Assert.AreEqual("TrackingNumber", dispute.MerchantEvidences[0].EvidenceType);
            Assert.AreEqual("customer_signature_file_id", dispute.MerchantEvidences[0].FileId);
            Assert.AreEqual(null, dispute.MerchantEvidences[0].EvidenceText);
        }
    }
}
