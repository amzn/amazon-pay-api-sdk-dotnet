using System;
using System.Collections.Generic;
using Amazon.Pay.API.WebStore.Dispute;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.WebStore.Interfaces;
using Amazon.Pay.API.WebStore.File;
using Moq;
using NUnit.Framework;

namespace Amazon.Pay.API.SDK.Tests.WebStore.Interfaces
{
    // This is a test fixture to ensure that a method can be mocked and helps ensure it is on the interface.
    [TestFixture]
    public class IDisputesClientTests
    {
        private Mock<IDisputesClient> mockDisputesClient;

        [OneTimeSetUp]
        public void Init()
        {
            this.mockDisputesClient = new Mock<IDisputesClient>(MockBehavior.Strict);
        }

        [SetUp]
        public void Setup()
        {
            this.mockDisputesClient.Reset();
        }

        // ------------ Testing the Disputes APIs ---------------

        [Test]
        public void CreateDisputeCanBeMocked()
        {
            var disputeResponse = new DisputeResponse();
            var createDisputeRequest = new CreateDisputeRequest(
                chargeId: "chargeId",
                chargeAmount: 1,
                currencyCode: Types.Currency.JPY
            )
            {
                ProviderMetadata = new ProviderMetadata
                {
                    ProviderDisputeId = "A2CQXQ2TUSYG7J"
                },
                FilingReason = "ProductNotReceived",
                FilingTimestamp = DateTime.UtcNow, // Current UTC datetime
                MerchantResponseDeadline = DateTime.UtcNow.AddDays(14), // 14 days from now
                StatusDetails = new StatusDetailsRequest
                {
                    State = "ActionRequired",
                    ReasonCode = "MerchantResponseRequired"
                }
            };

            this.mockDisputesClient.Setup(mwsc => mwsc.CreateDispute(It.IsAny<CreateDisputeRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(disputeResponse);

            var result = this.mockDisputesClient.Object.CreateDispute(createDisputeRequest, new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(disputeResponse));
            this.mockDisputesClient.Verify(mwsc => mwsc.CreateDispute(It.IsAny<CreateDisputeRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void GetDisputeCanBeMocked()
        {
            var disputeResponse = new DisputeResponse();
            
            this.mockDisputesClient.Setup(mwsc => mwsc.GetDispute(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(disputeResponse);

            var result = this.mockDisputesClient.Object.GetDispute("disputeId", new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(disputeResponse));
            this.mockDisputesClient.Verify(mwsc => mwsc.GetDispute(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);

        }
        
        [Test]
        public void UpdateDisputeCanBeMocked()
        {
            var disputeResponse = new DisputeResponse();
            var updateDisputeRequest = new UpdateDisputeRequest(
                    state: "UnderReview",
                    reasonCode: "MerchantResponseRequired",
                    closureTimestamp: DateTime.UtcNow
            );

            this.mockDisputesClient.Setup(mwsc => mwsc.UpdateDispute(It.IsAny<string>(), It.IsAny<UpdateDisputeRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(disputeResponse);

            var result = this.mockDisputesClient.Object.UpdateDispute("disputeId", updateDisputeRequest, new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(disputeResponse));
            this.mockDisputesClient.Verify(mwsc => mwsc.UpdateDispute(It.IsAny<string>(), It.IsAny<UpdateDisputeRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void ContestDisputeCanBeMocked()
        {
            var disputeResponse = new DisputeResponse();
            var merchantEvidences = new List<MerchantEvidence>{
                new MerchantEvidence
                {
                    EvidenceType = "TrackingNumber",
                    FileId = "file_123456789",
                    EvidenceText = "Customer confirmed receipt over email on 2025-04-25."
                }
            };
            var contestDisputeRequest = new ContestDisputeRequest(merchantEvidences);

            this.mockDisputesClient.Setup(mwsc => mwsc.ContestDispute(It.IsAny<string>(), It.IsAny<ContestDisputeRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(disputeResponse);

            var result = this.mockDisputesClient.Object.ContestDispute("disputeId", contestDisputeRequest, new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(disputeResponse));
            this.mockDisputesClient.Verify(mwsc => mwsc.ContestDispute(It.IsAny<string>(), It.IsAny<ContestDisputeRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        // ------------ Testing the Files APIs ---------------

        public void UploadFileCanBeMocked()
        {
            var fileResponse = new FileResponse();
            var uploadFileRequest = new UploadFileRequest(
                type: "jpg",
                purpose: "disputeEvidence"
            );

            this.mockDisputesClient.Setup(mwsc => mwsc.UploadFile(It.IsAny<UploadFileRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(fileResponse);

            var result = this.mockDisputesClient.Object.UploadFile(uploadFileRequest, new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(fileResponse));
            this.mockDisputesClient.Verify(mwsc => mwsc.UploadFile(It.IsAny<UploadFileRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }
    }
}