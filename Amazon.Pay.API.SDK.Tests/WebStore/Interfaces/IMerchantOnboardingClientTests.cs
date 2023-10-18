using System.Collections.Generic;
using Amazon.Pay.API.WebStore.AccountManagement;
using Amazon.Pay.API.WebStore.Types;
using Amazon.Pay.API.WebStore.Interfaces;
using Moq;
using NUnit.Framework;

namespace Amazon.Pay.API.SDK.Tests.WebStore.Interfaces
{
    // This is a test fixture to ensure that a method can be mocked and helps ensure it is on the interface.
    [TestFixture]
    public class IMerchantOnboardingClientTests
    {
        private Mock<IMerchantOnboardingClient> mockMerchantOnboardingClient;

        [OneTimeSetUp]
        public void Init()
        {
            this.mockMerchantOnboardingClient = new Mock<IMerchantOnboardingClient>(MockBehavior.Strict);
        }

        [SetUp]
        public void Setup()
        {
            this.mockMerchantOnboardingClient.Reset();
        }

        // ------------ Testing the Merchant Onboarding & Account Management APIs ---------------

        [Test]
        public void RegisterAmazonPayAccountCanBeMocked()
        {
            var registerAmazonPayAccountResponse = new RegisterAmazonPayAccountResponse();
            this.mockMerchantOnboardingClient.Setup(mwsc => mwsc.RegisterAmazonPayAccount(It.IsAny<RegisterAmazonPayAccountRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(registerAmazonPayAccountResponse);        
        
            var result = this.mockMerchantOnboardingClient.Object.RegisterAmazonPayAccount(new RegisterAmazonPayAccountRequest("ABCD12345", LedgerCurrency.JPY), new Dictionary<string, string>());
        
            Assert.That(result, Is.EqualTo(registerAmazonPayAccountResponse));
            this.mockMerchantOnboardingClient.Verify(mwsc => mwsc.RegisterAmazonPayAccount(It.IsAny<RegisterAmazonPayAccountRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }   
        
        [Test]
        public void UpdateAmazonPayAccountCanBeMocked()
        {
            var updateAmazonPayAccountResponse = new UpdateAmazonPayAccountResponse();
            this.mockMerchantOnboardingClient.Setup(mwsc => mwsc.UpdateAmazonPayAccount(It.IsAny<string>(), It.IsAny<UpdateAmazonPayAccountRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(updateAmazonPayAccountResponse);
            
            var result = this.mockMerchantOnboardingClient.Object.UpdateAmazonPayAccount("SessionId", new UpdateAmazonPayAccountRequest(), new Dictionary<string, string>());
            
            Assert.That(result, Is.EqualTo(updateAmazonPayAccountResponse));
            this.mockMerchantOnboardingClient.Verify(mwsc => mwsc.UpdateAmazonPayAccount(It.IsAny<string>(), It.IsAny<UpdateAmazonPayAccountRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }   
        
        [Test]
        public void DeleteAmazonPayAccountCanBeMocked()
        {
            var deleteAmazonPayAccountResponse = new DeleteAmazonPayAccountResponse();
            this.mockMerchantOnboardingClient.Setup(mwsc => mwsc.DeleteAmazonPayAccount("merchantAccountId", It.IsAny<Dictionary<string, string>>())).Returns(deleteAmazonPayAccountResponse);
            
            var result = this.mockMerchantOnboardingClient.Object.DeleteAmazonPayAccount("merchantAccountId", new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(deleteAmazonPayAccountResponse));
            this.mockMerchantOnboardingClient.Verify(mwsc => mwsc.DeleteAmazonPayAccount("merchantAccountId", It.IsAny<Dictionary<string, string>>()), Times.Once);
        }
    }
}