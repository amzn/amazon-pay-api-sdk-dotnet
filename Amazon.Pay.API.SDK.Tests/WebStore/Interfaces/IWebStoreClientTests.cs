using System.Collections.Generic;
using Amazon.Pay.API.AuthorizationToken;
using Amazon.Pay.API.DeliveryTracker;
using Amazon.Pay.API.WebStore.Buyer;
using Amazon.Pay.API.WebStore.Charge;
using Amazon.Pay.API.WebStore.ChargePermission;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Interfaces;
using Amazon.Pay.API.WebStore.Refund;
using Amazon.Pay.API.WebStore.Types;
using Moq;
using NUnit.Framework;

namespace Amazon.Pay.API.SDK.Tests.WebStore.Interfaces
{
    // This is a test fixture to ensure that a method can be mocked and helps ensure it is on the interface.
    [TestFixture]
    public class IWebStoreClientTests
    {
        private Mock<IWebStoreClient> mockWebStoreClient;

        [OneTimeSetUp]
        public void Init()
        {
            this.mockWebStoreClient = new Mock<IWebStoreClient>(MockBehavior.Strict);
        }

        [SetUp]
        public void Setup()
        {
            this.mockWebStoreClient.Reset();
        }


        [Test]
        public void CreateCheckOutSessionCanBeMocked()
        {
            var checkoutSessionResponse = new CheckoutSessionResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.CreateCheckoutSession(It.IsAny<CreateCheckoutSessionRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(checkoutSessionResponse);

            var result = this.mockWebStoreClient.Object.CreateCheckoutSession(new CreateCheckoutSessionRequest("www.amazon.com", "1"), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(checkoutSessionResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.CreateCheckoutSession(It.IsAny<CreateCheckoutSessionRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void UpdateCheckoutSessionCanBeMocked()
        {
            var checkoutSessionResponse = new CheckoutSessionResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.UpdateCheckoutSession(It.IsAny<string>(), It.IsAny<UpdateCheckoutSessionRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(checkoutSessionResponse);

            var result = this.mockWebStoreClient.Object.UpdateCheckoutSession("SessionId", new UpdateCheckoutSessionRequest(), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(checkoutSessionResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.UpdateCheckoutSession(It.IsAny<string>(), It.IsAny<UpdateCheckoutSessionRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void CompleteCheckoutSessionCanBeMocked()
        {
            var checkoutSessionResponse = new CheckoutSessionResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.CompleteCheckoutSession(It.IsAny<string>(), It.IsAny<CompleteCheckoutSessionRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(checkoutSessionResponse);

            var result = this.mockWebStoreClient.Object.CompleteCheckoutSession("SessionId", new CompleteCheckoutSessionRequest(10, Types.Currency.USD), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(checkoutSessionResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.CompleteCheckoutSession(It.IsAny<string>(), It.IsAny<CompleteCheckoutSessionRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }
        
        [Test]
        public void FinalizeCheckoutSessionCanBeMocked()
        {
            var checkoutSessionResponse = new CheckoutSessionResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.FinalizeCheckoutSession(It.IsAny<string>(), It.IsAny<FinalizeCheckoutSessionRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(checkoutSessionResponse);

            var result = this.mockWebStoreClient.Object.FinalizeCheckoutSession("SessionId", new FinalizeCheckoutSessionRequest(10, Types.Currency.USD, PaymentIntent.Confirm), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(checkoutSessionResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.FinalizeCheckoutSession(It.IsAny<string>(), It.IsAny<FinalizeCheckoutSessionRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void GetChargePermissionCanBeMocked()
        {
            var chargePermissionResponse = new ChargePermissionResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.GetChargePermission(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(chargePermissionResponse);

            var result = this.mockWebStoreClient.Object.GetChargePermission("ChargePermissionId", new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(chargePermissionResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.GetChargePermission(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void UpdateChargePermissionCanBeMocked()
        {
            var chargePermissionResponse = new ChargePermissionResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.UpdateChargePermission(It.IsAny<string>(), It.IsAny<UpdateChargePermissionRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(chargePermissionResponse);

            var result = this.mockWebStoreClient.Object.UpdateChargePermission("ChargePermissionId", new UpdateChargePermissionRequest(),  new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(chargePermissionResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.UpdateChargePermission(It.IsAny<string>(), It.IsAny<UpdateChargePermissionRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void CloseChargePermissionCanBeMocked()
        {
            var chargePermissionResponse = new ChargePermissionResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.CloseChargePermission(It.IsAny<string>(), It.IsAny<CloseChargePermissionRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(chargePermissionResponse);

            var result = this.mockWebStoreClient.Object.CloseChargePermission("ChargePermissionId", new CloseChargePermissionRequest("Testing"), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(chargePermissionResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.CloseChargePermission(It.IsAny<string>(), It.IsAny<CloseChargePermissionRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void CreateChargeCanBeMocked()
        {
            var chargeResponse = new ChargeResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.CreateCharge(It.IsAny<CreateChargeRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(chargeResponse);

            var result = this.mockWebStoreClient.Object.CreateCharge(new CreateChargeRequest("ChargePermissionId", 100, Types.Currency.USD), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(chargeResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.CreateCharge(It.IsAny<CreateChargeRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void CaptureChargeCanBeMocked()
        {
            var chargeResponse = new ChargeResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.CaptureCharge(It.IsAny<string>(), It.IsAny<CaptureChargeRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(chargeResponse);

            var result = this.mockWebStoreClient.Object.CaptureCharge("chargeId", new CaptureChargeRequest(100, Types.Currency.USD), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(chargeResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.CaptureCharge(It.IsAny<string>(), It.IsAny<CaptureChargeRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void GetChargeCanBeMocked()
        {
            var chargeResponse = new ChargeResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.GetCharge(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(chargeResponse);

            var result = this.mockWebStoreClient.Object.GetCharge("chargeId", new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(chargeResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.GetCharge(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void CancelChargeCanBeMocked()
        {
            var chargeResponse = new ChargeResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.CancelCharge(It.IsAny<string>(), It.IsAny<CancelChargeRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(chargeResponse);

            var result = this.mockWebStoreClient.Object.CancelCharge("chargeId", new CancelChargeRequest("Testing"), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(chargeResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.CancelCharge(It.IsAny<string>(), It.IsAny<CancelChargeRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void GetRefundCanBeMocked()
        {
            var refundResponse = new RefundResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.GetRefund(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(refundResponse);

            var result = this.mockWebStoreClient.Object.GetRefund("RefundId", new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(refundResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.GetRefund(It.IsAny<string>(), It.IsAny <Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void CreateRefundCanBeMocked()
        {
            var refundResponse = new RefundResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.CreateRefund(It.IsAny<CreateRefundRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(refundResponse);

            var result = this.mockWebStoreClient.Object.CreateRefund(new CreateRefundRequest("ChargeId", 10, Types.Currency.USD), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(refundResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.CreateRefund(It.IsAny<CreateRefundRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void GetBuyerCanBeMocked()
        {
            var buyerResponse = new BuyerResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.GetBuyer(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(buyerResponse);

            var result = this.mockWebStoreClient.Object.GetBuyer("BuyerToken", new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(buyerResponse));
            this.mockWebStoreClient.Verify(mwsc => mwsc.GetBuyer(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void GenerateButtonSignatureWithStringParameterCanBeMocked()
        {
            var response = "response";
            this.mockWebStoreClient.Setup(mwsc => mwsc.GenerateButtonSignature(It.IsAny<string>())).Returns(response);

            var result = this.mockWebStoreClient.Object.GenerateButtonSignature("string");

            Assert.That(result, Is.EqualTo(response));
            this.mockWebStoreClient.Verify(mwsc => mwsc.GenerateButtonSignature(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GenerateButtonSignatureWithCreateCheckoutSessionRequestParameterCanBeMocked()
        {
            var response = "response";
            this.mockWebStoreClient.Setup(mwsc => mwsc.GenerateButtonSignature(It.IsAny<CreateCheckoutSessionRequest>())).Returns(response);

            var result = this.mockWebStoreClient.Object.GenerateButtonSignature(new CreateCheckoutSessionRequest("www.amazon.com", "1"));

            Assert.That(result, Is.EqualTo(response));
            this.mockWebStoreClient.Verify(mwsc => mwsc.GenerateButtonSignature(It.IsAny<CreateCheckoutSessionRequest>()), Times.Once);
        }

        [Test]
        public void GenerateButtonSignatureWithSignInRequestParameterCanBeMocked()
        {
            var response = "response";
            this.mockWebStoreClient.Setup(mwsc => mwsc.GenerateButtonSignature(It.IsAny<SignInRequest>())).Returns(response);

            var result = this.mockWebStoreClient.Object.GenerateButtonSignature(new SignInRequest("www.amazon.com", "1"));

            Assert.That(result, Is.EqualTo(response));
            this.mockWebStoreClient.Verify(mwsc => mwsc.GenerateButtonSignature(It.IsAny<SignInRequest>()), Times.Once);
        }

        [Test]
        public void SendDeliveryTrackingInformationCanBeMocked()
        {
            var response = new DeliveryTrackerResponse();
            this.mockWebStoreClient.Setup(mwsc => mwsc.SendDeliveryTrackingInformation(It.IsAny<DeliveryTrackerRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(response);

            var result = this.mockWebStoreClient.Object.SendDeliveryTrackingInformation(new DeliveryTrackerRequest("123456789", false, "1Z654686546835464", "UPS"));
            Assert.That(result, Is.EqualTo(response));
            this.mockWebStoreClient.Verify(mwsc => mwsc.SendDeliveryTrackingInformation(It.IsAny<DeliveryTrackerRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void GetAuthorizationTokenCanBeMocked()
        {
            var response = new AuthorizationTokenResponse();
            this.mockWebStoreClient.Setup(mwsc =>
                mwsc.GetAuthorizationToken(It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<Dictionary<string, string>>())).Returns(response);

            var result = this.mockWebStoreClient.Object.GetAuthorizationToken("123456789", "ASFW3OT8A35468");
            Assert.That(result, Is.EqualTo(response));
            this.mockWebStoreClient.Verify(mwsc => mwsc.GetAuthorizationToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

    }
}
