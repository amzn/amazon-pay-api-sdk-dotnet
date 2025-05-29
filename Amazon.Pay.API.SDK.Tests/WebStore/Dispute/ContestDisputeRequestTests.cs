using System;
using System.Collections.Generic;
using Amazon.Pay.API.WebStore.Dispute;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.Dispute
{
    [TestFixture]
    public class ContestDisputeRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitializedAsExpected()
        {
            // arrange
            var evidences = new List<MerchantEvidence>
            {
                new MerchantEvidence
                {
                    EvidenceType = "TrackingNumber",
                    FileId = null,
                    EvidenceText = "raw text supporting merchant evidence"
                },
                new MerchantEvidence
                {
                    EvidenceType = "CustomerSignature",
                    FileId = "customer_signature_file_id",
                    EvidenceText = null
                }
            };

            // act
            var request = new ContestDisputeRequest(evidences);

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.MerchantEvidences);
            Assert.AreEqual(2, request.MerchantEvidences.Count);
            Assert.AreEqual("TrackingNumber", request.MerchantEvidences[0].EvidenceType);
            Assert.AreEqual("raw text supporting merchant evidence", request.MerchantEvidences[0].EvidenceText);
            Assert.AreEqual("CustomerSignature", request.MerchantEvidences[1].EvidenceType);
            Assert.AreEqual("customer_signature_file_id", request.MerchantEvidences[1].FileId);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var evidences = new List<MerchantEvidence>
            {
                new MerchantEvidence
                {
                    EvidenceType = "TrackingNumber",
                    EvidenceText = "simple text"
                }
            };

            var request = new ContestDisputeRequest(evidences);

            // act
            string json = JsonConvert.SerializeObject(request);
            string expected = "{\"merchantEvidences\":[{\"evidenceType\":\"TrackingNumber\",\"fileId\":null,\"evidenceText\":\"simple text\"}]}";

            // assert
            Assert.AreEqual(expected, json);
        }

        [Test]
        public void CanConvertToJsonFull()
        {
            // arrange
            var evidences = new List<MerchantEvidence>
            {
                new MerchantEvidence
                {
                    EvidenceType = "TrackingNumber",
                    FileId = null,
                    EvidenceText = "raw text supporting merchant evidence"
                },
                new MerchantEvidence
                {
                    EvidenceType = "CustomerSignature",
                    FileId = "customer_signature_file_id",
                    EvidenceText = null
                }
            };

            var request = new ContestDisputeRequest(evidences);

            // act
            string json = JsonConvert.SerializeObject(request);
            string expected = "{\"merchantEvidences\":["
                            + "{\"evidenceType\":\"TrackingNumber\",\"fileId\":null,\"evidenceText\":\"raw text supporting merchant evidence\"},"
                            + "{\"evidenceType\":\"CustomerSignature\",\"fileId\":\"customer_signature_file_id\",\"evidenceText\":null}"
                            + "]}";

            // assert
            Assert.AreEqual(expected, json);
        }
    }
}