using Amazon.Pay.API.Types;
using NUnit.Framework;
using System;

using Environment = Amazon.Pay.API.Types.Environment;

namespace Amazon.Pay.API.Tests
{
    [TestFixture]
    public class ApiUrlBuilderTests
    {
        private ApiUrlBuilder apiUrlBuilder;
        private ApiConfiguration payConfig;

        [SetUp]
        public void SetUp()
        {
            payConfig = new ApiConfiguration
            (
                region: Region.Europe,
                environment: Environment.Live,
                publicKeyId: "foo",
                privateKey: "-----BEGIN RSA PRIVATE KEY-----" // fake a private key ..);
            );
        }

        [Test]
        public void GetApiEndPointBaseUrlForUnitedStatesLive()
        {
            // arrange
            payConfig.Region = Region.UnitedStates;
            payConfig.Environment = Environment.Live;
            apiUrlBuilder = new ApiUrlBuilder(payConfig);
            Uri expectedURL = new Uri("https://pay-api.amazon.com/live/");

            // act
            Uri actualURL = apiUrlBuilder.GetApiEndPointBaseUrl();

            // assert
            Assert.AreEqual(expectedURL, actualURL);
        }

        [Test]
        public void GetApiEndPointBaseUrlForUnitedStatesSandbox()
        {
            // arrange
            payConfig.Region = Region.UnitedStates;
            payConfig.Environment = Environment.Sandbox;
            apiUrlBuilder = new ApiUrlBuilder(payConfig);
            Uri expectedURL = new Uri("https://pay-api.amazon.com/sandbox/");

            // act
            Uri actualURL = apiUrlBuilder.GetApiEndPointBaseUrl();

            // assert
            Assert.AreEqual(expectedURL, actualURL);
        }

        [Test]
        public void GetApiEndPointBaseUrlForEuropeSandbox()
        {
            // arrange
            payConfig.Region = Region.Europe;
            payConfig.Environment = Environment.Sandbox;
            apiUrlBuilder = new ApiUrlBuilder(payConfig);
            Uri expectedURL = new Uri("https://pay-api.amazon.eu/sandbox/");

            // act
            Uri actualURL = apiUrlBuilder.GetApiEndPointBaseUrl();

            // assert
            Assert.AreEqual(expectedURL, actualURL);
        }

        [Test]
        public void GetApiEndPointBaseUrlForJapanSandbox()
        {
            // arrange
            payConfig.Region = Region.Japan;
            payConfig.Environment = Environment.Sandbox;
            apiUrlBuilder = new ApiUrlBuilder(payConfig);
            Uri expectedURL = new Uri("https://pay-api.amazon.jp/sandbox/");

            // act
            Uri actualURL = apiUrlBuilder.GetApiEndPointBaseUrl();

            // assert
            Assert.AreEqual(expectedURL, actualURL);
        }

        [Test]
        public void GetFullPathForInStoreApiService()
        {
            // arrange
            payConfig.Region = Region.UnitedStates;
            payConfig.Environment = Environment.Sandbox;
            apiUrlBuilder = new ApiUrlBuilder(payConfig);
            Uri expectedURL = new Uri("https://pay-api.amazon.com/sandbox/v2/in-store/merchantScan/");

            // act
            Uri actualURL = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.InStore, Constants.Resources.InStore.MerchantScan);

            // assert
            Assert.AreEqual(expectedURL, actualURL);
        }

        [Test]
        public void GetFullPathForDeliveryTrackerApiService()
        {
            // arrange
            payConfig.Region = Region.UnitedStates;
            payConfig.Environment = Environment.Sandbox;
            apiUrlBuilder = new ApiUrlBuilder(payConfig);
            Uri expectedURL = new Uri("https://pay-api.amazon.com/sandbox/v2/deliveryTrackers/");

            // act
            Uri actualURL = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.DeliveryTracker);

            // assert
            Assert.AreEqual(expectedURL, actualURL);
        }

        [Test]
        public void GetFullPathForTokenExchangeApi()
        {
            // arrange
            payConfig.Region = Region.UnitedStates;
            payConfig.Environment = Environment.Sandbox;
            apiUrlBuilder = new ApiUrlBuilder(payConfig);
            Uri expectedURL = new Uri("https://pay-api.amazon.com/sandbox/v2/authorizationTokens/");

            // act
            Uri actualURL = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.TokenExchange);

            // assert
            Assert.AreEqual(expectedURL, actualURL);
        }
    }
}
