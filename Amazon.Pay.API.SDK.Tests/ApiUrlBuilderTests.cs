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
        private ApiUrlBuilder apiUrlBuilderWithAlgorithm;
        private ApiConfiguration payConfig;
        private ApiConfiguration payConfigWithAlgorithm;
        // Fake Public keys
        private const string livePublicKeyId = "LIVE-XXXXXXXXXXXXXXXXXXXXXXXX";
        private const string sandboxPublicKeyId = "SANDBOX-XXXXXXXXXXXXXXXXXXXXXXXX";

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

            payConfigWithAlgorithm = new ApiConfiguration
            (
                region: Region.Europe,
                environment: Environment.Live,
                publicKeyId: "foo",
                privateKey: "-----BEGIN RSA PRIVATE KEY-----", // fake a private key ..);
                algorithm: AmazonSignatureAlgorithm.V2
            );
        }

        [Test]
        public void GetApiEndPointBaseUrlForUnitedStatesLive()
        {
            GetApiEndPointBaseUrlForUnitedStatesLive(payConfig);
            GetApiEndPointBaseUrlForUnitedStatesLive(payConfigWithAlgorithm);
        }

        [Test]
        public void GetApiEndPointBaseUrlForUnitedStatesSandbox()
        {
            GetApiEndPointBaseUrlForUnitedStatesSandbox(payConfig);
            GetApiEndPointBaseUrlForUnitedStatesSandbox(payConfigWithAlgorithm);
        }

        [Test]
        public void GetApiEndPointBaseUrlForEuropeSandbox()
        {
            GetApiEndPointBaseUrlForEuropeSandbox(payConfig);
            GetApiEndPointBaseUrlForEuropeSandbox(payConfigWithAlgorithm);
        }

        [Test]
        public void GetApiEndPointBaseUrlForJapanSandbox()
        {
            GetApiEndPointBaseUrlForJapanSandbox(payConfig);
            GetApiEndPointBaseUrlForJapanSandbox(payConfigWithAlgorithm);
        }

        [Test]
        public void GetFullPathForInStoreApiService()
        {
            GetFullPathForInStoreApiService(payConfig);
            GetFullPathForInStoreApiService(payConfigWithAlgorithm);
        }

        [Test]
        public void GetFullPathForDeliveryTrackerApiService()
        {
            GetFullPathForDeliveryTrackerApiService(payConfig);
            GetFullPathForDeliveryTrackerApiService(payConfigWithAlgorithm);
        }

        [Test]
        public void GetFullPathForTokenExchangeApi()
        {
            GetFullPathForTokenExchangeApi(payConfig);
            GetFullPathForTokenExchangeApi(payConfigWithAlgorithm);
        }

        [Test]
        public void GetApiUnifiedEndPointBaseUrlForUnitedStates()
        {
            GetFullPathForTokenExchangeApi(payConfig);
            GetFullPathForTokenExchangeApi(payConfigWithAlgorithm);
        }

        [Test]
        public void GetApiUnifiedEndPointBaseUrlForForEurope()
        {
            string expectedURL = "https://pay-api.amazon.eu/";

            // Scenario 1 : Testing Unified endpoint base URL by passing Live specific PublicKeyId for Europe
            VerifyUnifiedEndpointBaseURL(Region.Europe, livePublicKeyId, expectedURL);

            // Scenario 2 : Testing Unified endpoint base URL by passing Sandbox specific PublicKeyId for Europe
            VerifyUnifiedEndpointBaseURL(Region.Europe, sandboxPublicKeyId, expectedURL);
        }

        [Test]
        public void GetApiUnifiedEndPointBaseUrlForForJapan()
        {
            string expectedURL = "https://pay-api.amazon.jp/";

            // Scenario 1 : Testing Unified endpoint base URL by passing Live specific PublicKeyId for Japan
            VerifyUnifiedEndpointBaseURL(Region.Japan, livePublicKeyId, expectedURL);

            // Scenario 2 : Testing Unified endpoint base URL by passing Sandbox specific PublicKeyId for Japan
            VerifyUnifiedEndpointBaseURL(Region.Japan, sandboxPublicKeyId, expectedURL);
        }

        // Generic method used to verify Unified Endpoint Base URL
        public void VerifyUnifiedEndpointBaseURL(Region region, string publicKeyId, string url)
        {
            // Configuration
            payConfig.Region = region;
            payConfig.PublicKeyId = publicKeyId;
            payConfigWithAlgorithm.Region = region;
            payConfigWithAlgorithm.PublicKeyId = publicKeyId;
            apiUrlBuilder = new ApiUrlBuilder(payConfig);
            apiUrlBuilderWithAlgorithm = new ApiUrlBuilder(payConfigWithAlgorithm);

            // Building URL
            Uri expectedURL = new Uri(url);
            Uri actualURL = apiUrlBuilder.GetApiEndPointBaseUrl();
            Uri actualURLWithAlgorithm = apiUrlBuilderWithAlgorithm.GetApiEndPointBaseUrl();

            // Assertion
            Assert.AreEqual(expectedURL, actualURL);
            Assert.AreEqual(expectedURL, actualURLWithAlgorithm);
        }

        [Test]
        public void GetUnifiedEndpointFullPathForInStoreApiService()
        {
            string expectedUnitedStatesURL = "https://pay-api.amazon.com/v2/in-store/merchantScan/";

            // Testing Unified endpoint full path by passing environment specific PublicKeyId for UnitedStates
            VerifyUnifiedEndpointFullPath(Region.UnitedStates, livePublicKeyId, expectedUnitedStatesURL,
                Constants.ApiServices.InStore, Constants.Resources.InStore.MerchantScan);
            VerifyUnifiedEndpointFullPath(Region.UnitedStates, sandboxPublicKeyId, expectedUnitedStatesURL,
                Constants.ApiServices.InStore, Constants.Resources.InStore.MerchantScan);

            string expectedEuropeURL = "https://pay-api.amazon.eu/v2/in-store/merchantScan/";

            // Testing Unified endpoint full path by passing environment specific PublicKeyId for Europe
            VerifyUnifiedEndpointFullPath(Region.Europe, livePublicKeyId, expectedEuropeURL,
                Constants.ApiServices.InStore, Constants.Resources.InStore.MerchantScan);
            VerifyUnifiedEndpointFullPath(Region.Europe, sandboxPublicKeyId, expectedEuropeURL,
                Constants.ApiServices.InStore, Constants.Resources.InStore.MerchantScan);

            string expectedJapanURL = "https://pay-api.amazon.jp/v2/in-store/merchantScan/";

            // Testing Unified endpoint full path by passing environment specific PublicKeyId for Japan
            VerifyUnifiedEndpointFullPath(Region.Japan, livePublicKeyId, expectedJapanURL,
                Constants.ApiServices.InStore, Constants.Resources.InStore.MerchantScan);
            VerifyUnifiedEndpointFullPath(Region.Japan, sandboxPublicKeyId, expectedJapanURL,
                Constants.ApiServices.InStore, Constants.Resources.InStore.MerchantScan);
        }

        [Test]
        public void GetUnifiedEndpointFullPathForDeliveryTrackerApiService()
        {
            string expectedUnitedStatesURL = "https://pay-api.amazon.com/v2/deliveryTrackers/";

            // Testing Unified endpoint full path by passing environment specific PublicKeyId for UnitedStates
            VerifyUnifiedEndpointFullPath(Region.UnitedStates, livePublicKeyId, expectedUnitedStatesURL,
                Constants.ApiServices.Default, Constants.Resources.DeliveryTracker);
            VerifyUnifiedEndpointFullPath(Region.UnitedStates, sandboxPublicKeyId, expectedUnitedStatesURL,
                Constants.ApiServices.Default, Constants.Resources.DeliveryTracker);

            string expectedEuropeURL = "https://pay-api.amazon.eu/v2/deliveryTrackers/";

            // Testing Unified endpoint full path by passing environment specific PublicKeyId for Europe
            VerifyUnifiedEndpointFullPath(Region.Europe, livePublicKeyId, expectedEuropeURL,
                Constants.ApiServices.Default, Constants.Resources.DeliveryTracker);
            VerifyUnifiedEndpointFullPath(Region.Europe, sandboxPublicKeyId, expectedEuropeURL,
                Constants.ApiServices.Default, Constants.Resources.DeliveryTracker);

            string expectedJapanURL = "https://pay-api.amazon.jp/v2/deliveryTrackers/";

            // Testing Unified endpoint full path by passing environment specific PublicKeyId for Japan
            VerifyUnifiedEndpointFullPath(Region.Japan, livePublicKeyId, expectedJapanURL,
                Constants.ApiServices.Default, Constants.Resources.DeliveryTracker);
            VerifyUnifiedEndpointFullPath(Region.Japan, sandboxPublicKeyId, expectedJapanURL,
                Constants.ApiServices.Default, Constants.Resources.DeliveryTracker);
        }

        [Test]
        public void GetUnifiedEndpointFullPathForTokenExchangeApi()
        {
            string expectedUnitedStatesURL = "https://pay-api.amazon.com/v2/authorizationTokens/";

            // Testing Unified endpoint full path by passing environment specific PublicKeyId for UnitedStates
            VerifyUnifiedEndpointFullPath(Region.UnitedStates, livePublicKeyId, expectedUnitedStatesURL,
                Constants.ApiServices.Default, Constants.Resources.TokenExchange);
            VerifyUnifiedEndpointFullPath(Region.UnitedStates, sandboxPublicKeyId, expectedUnitedStatesURL,
                Constants.ApiServices.Default, Constants.Resources.TokenExchange);

            string expectedEuropeURL = "https://pay-api.amazon.eu/v2/authorizationTokens/";

            // Testing Unified endpoint full path by passing environment specific PublicKeyId for Europe
            VerifyUnifiedEndpointFullPath(Region.Europe, livePublicKeyId, expectedEuropeURL,
                Constants.ApiServices.Default, Constants.Resources.TokenExchange);
            VerifyUnifiedEndpointFullPath(Region.Europe, sandboxPublicKeyId, expectedEuropeURL,
                Constants.ApiServices.Default, Constants.Resources.TokenExchange);

            string expectedJapanURL = "https://pay-api.amazon.jp/v2/authorizationTokens/";

            // Testing Unified endpoint full path by passing environment specific PublicKeyId for Japan
            VerifyUnifiedEndpointFullPath(Region.Japan, livePublicKeyId, expectedJapanURL,
                Constants.ApiServices.Default, Constants.Resources.TokenExchange);
            VerifyUnifiedEndpointFullPath(Region.Japan, sandboxPublicKeyId, expectedJapanURL,
                Constants.ApiServices.Default, Constants.Resources.TokenExchange);
        }

        // Generic method used to verify Unified Endpoint Full Path
        public void VerifyUnifiedEndpointFullPath(Region region, string publicKeyId, string url, string apiService, string resource)
        {
            // Configuration
            payConfig.Region = region;
            payConfig.PublicKeyId = publicKeyId;
            payConfigWithAlgorithm.Region = region;
            payConfigWithAlgorithm.PublicKeyId = publicKeyId;
            apiUrlBuilder = new ApiUrlBuilder(payConfig);
            apiUrlBuilderWithAlgorithm = new ApiUrlBuilder(payConfigWithAlgorithm);

            // Building URL
            Uri expectedURL = new Uri(url);
            Uri actualURL = apiUrlBuilder.BuildFullApiPath(apiService, resource);
            Uri actualURLWithAlgorithm = apiUrlBuilderWithAlgorithm.BuildFullApiPath(apiService, resource);

            // Assertion
            Assert.AreEqual(expectedURL, actualURL);
            Assert.AreEqual(expectedURL, actualURLWithAlgorithm);
        }

        public void GetApiEndPointBaseUrlForUnitedStatesLive(ApiConfiguration payConfig)
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

        public void GetApiEndPointBaseUrlForUnitedStatesSandbox(ApiConfiguration payConfig)
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

        public void GetApiEndPointBaseUrlForEuropeSandbox(ApiConfiguration payConfig)
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

        public void GetApiEndPointBaseUrlForJapanSandbox(ApiConfiguration payConfig)
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

        public void GetFullPathForInStoreApiService(ApiConfiguration payConfig)
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

        public void GetFullPathForDeliveryTrackerApiService(ApiConfiguration payConfig)
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

        public void GetFullPathForTokenExchangeApi(ApiConfiguration payConfig)
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
