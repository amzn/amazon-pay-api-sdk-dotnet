using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Amazon.Pay.API.AuthorizationToken;
using Amazon.Pay.API.DeliveryTracker;
using Amazon.Pay.API.InStore;
using Amazon.Pay.API.InStore.Refund;
using Amazon.Pay.API.Types;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Environment = Amazon.Pay.API.Types.Environment;

namespace Amazon.Pay.API.SDK.Tests
{
    [TestFixture]
    public class ClientTests
    {
        private Mock<InStoreClient> mockClient;
        private ApiConfiguration payConfig;
        Mock<ISignatureHelper> mockSignatureHelper;

        [OneTimeSetUp]
        public void Init()
        {
            payConfig = new ApiConfiguration
            (
                region: Region.UnitedStates,
                environment: Environment.Sandbox,
                publicKeyId: "foo",
                privateKey: "-----BEGIN RSA PRIVATE KEY-----" // fake a private key ..);
            );
            mockClient = new Mock<InStoreClient>(MockBehavior.Strict, payConfig) { CallBase = true };
            mockSignatureHelper = new Mock<ISignatureHelper>(MockBehavior.Strict);
        }

        [SetUp]
        public void Setup()
        {
            mockClient.Reset();
            mockSignatureHelper.Reset();
            mockSignatureHelper.SetupAllProperties();
            mockSignatureHelper.Setup<Dictionary<string, List<string>>>(x => x.CreateDefaultHeaders(It.IsAny<Uri>()))
                .Returns(CreateHeaders(new Uri("http://pay-api.amazon.eu/")));
            mockSignatureHelper.Setup<string>(x => x.CreateCanonicalRequest(It.IsAny<ApiRequest>(), It.IsAny<Dictionary<string, List<string>>>()))
                .Returns("canonicalstring");
            mockSignatureHelper.Setup<string>(x => x.CreateStringToSign(It.IsAny<string>())).Returns("bar");
            mockSignatureHelper.Setup<string>(x => x.GenerateSignature(It.IsAny<string>())).Returns("baz");
            mockClient.Protected().As<IClientMapping>()
                .Setup<ISignatureHelper>(c => c.signatureHelper).Returns(mockSignatureHelper.Object);
        }

        [Test]
        public void CanSendDeliveryTrackingInformation()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<DeliveryTrackerResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<DeliveryTrackerResponse>(request, headers, HttpMethod.POST,
                    $"{Constants.Resources.DeliveryTracker}"));
            var testRequest = new DeliveryTrackerRequest("123456789", false, "1Z654686546835464", "UPS");
            var result = mockClient.Object.SendDeliveryTrackingInformation(testRequest);
        }

        [Test]
        public void CanGetAuthorizationToken()
        {
            var testMerchantId = "ASFW3OT8A35468";
            var testQueryParameters = new Dictionary<string, List<string>> { { "merchantId", new List<string>() { testMerchantId } } };
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<AuthorizationTokenResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<AuthorizationTokenResponse>(request, headers, HttpMethod.GET,
                    $"{Constants.Resources.TokenExchange}/123456789/", testQueryParameters));

            var result = mockClient.Object.GetAuthorizationToken("123456789", testMerchantId);
        }

        [Test]
        public void CanProcessRequestOk()
        {
            var refundResponse =
                "{\r\n     \"refundId\": \"S01-5105180-3221187-R022311\",\r\n     \"chargeId\": \"S01-5105180-3221187-C056351\",\r\n     \"refundAmount\": {\r\n         \"amount\": \"14.00\",\r\n         \"currencyCode\": \"USD\"\r\n     },\r\n     \"softDescriptor\": \"Descriptor\",\r\n     \"creationTimestamp\": \"20190714T155300Z\",\r\n     \"statusDetails\": {\r\n         \"state\": \"RefundInitiated\",\r\n         \"reasonCode\": null,\r\n         \"reasonDescription\": null,\r\n         \"lastUpdatedTimestamp\": \"20190714T155300Z\"\r\n     },\r\n     \"releaseEnvironment\": \"Sandbox\"\r\n}";

            var mockTestClient = new Mock<TestClient>(payConfig) {CallBase = true};
            mockTestClient.Protected().As<ITestClientMapping>()
                .Setup(c => c.SendRequest(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> postSignedHeaders) => AssertPreSendRequestFlow(request, postSignedHeaders, HttpStatusCode.OK, refundResponse));

            var testRequest = new Amazon.Pay.API.InStore.Refund.CreateRefundRequest("123456789", 10, Currency.USD, "referenceID");
            var apiUrlBuilder = new ApiUrlBuilder(payConfig);
            var apiPath = apiUrlBuilder.BuildFullApiPath(Constants.ApiServices.Default, Constants.Resources.InStore.Refund);
            var apiRequest = new ApiRequest(apiPath, HttpMethod.POST, testRequest, null);
            
            var result = mockTestClient.Object.Invoke<RefundResponse>(apiRequest, new Dictionary<string, string>());
            Assert.NotNull(result);
            Assert.AreEqual(refundResponse, result.RawResponse);
            Assert.AreEqual(testRequest.ToJson(), result.RawRequest);
            Assert.AreEqual("S01-5105180-3221187-R022311", result.RefundId);
            Assert.AreEqual(true, result.Success);
            Assert.NotNull(result.RequestId);
            Assert.AreEqual(0, result.Retries);
            Assert.AreEqual(HttpMethod.POST, result.Method);
            Assert.AreEqual(apiPath, result.Url);
            Assert.True(result.Duration > 0);
        }

        public HttpWebResponse AssertPreSendRequestFlow(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders, HttpStatusCode responseCode, string resultContent)
        {
            var resultContentBytes = Encoding.UTF8.GetBytes(resultContent);
            var mockHttpWebResponse = new Mock<HttpWebResponse>();
            mockHttpWebResponse.SetupAllProperties();
            mockHttpWebResponse.Setup(_ => _.StatusCode).Returns(responseCode);
            mockHttpWebResponse.Setup(_ => _.GetResponseStream()).Returns(new MemoryStream(resultContentBytes));
            WebHeaderCollection collection = new WebHeaderCollection();
            var headers = CreateHeaders(new Uri("http://pay-api.amazon.eu/"));
            foreach (var keyValuePair in headers) collection.Add(keyValuePair.Key, string.Join(",",keyValuePair.Value));
            collection.Add(Constants.Headers.RequestId, Guid.NewGuid().ToString());

            mockHttpWebResponse.Setup(_ => _.Headers).Returns(collection);
            return mockHttpWebResponse.Object;
        }

        public T AssertPreProcessRequestFlow<T>(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders, HttpMethod method, string pathContains, Dictionary<string, List<string>> queryParameters = null) where T : AmazonPayResponse, new()
        {
            var expectedHeaderCount = method.Equals(HttpMethod.POST) ? 8 : 7;
            // assert values setup to this point
            Assert.NotNull(postSignedHeaders);
            Assert.AreEqual(expectedHeaderCount, postSignedHeaders.Count);
            Assert.True(postSignedHeaders.ContainsKey("user-agent"));
            Assert.True(postSignedHeaders.ContainsKey("authorization"));
            if (method.Equals(HttpMethod.POST))
            {
                Assert.True(postSignedHeaders.ContainsKey("x-amz-pay-idempotency-key"));
            }

            var authHeader = postSignedHeaders["authorization"];
            var authHeaderValues = authHeader.Split(',').ToList();
            Assert.AreEqual(3, authHeaderValues.Count);
            Assert.True(authHeaderValues[0].Contains("PublicKeyId=foo"));
            Assert.True(authHeaderValues[1].Contains("SignedHeaders="));
            Assert.True(authHeaderValues[2].Contains("Signature=baz"));

            Assert.AreEqual(apiRequest.HttpMethod, method);
            Assert.True(apiRequest.Path.ToString().Contains(pathContains));

            if (queryParameters == null) return new T();

            Assert.IsNotNull(apiRequest.QueryParameters);
            foreach (var param in queryParameters)
            {
                Assert.True(apiRequest.QueryParameters.ContainsKey(param.Key));
                Assert.AreEqual(apiRequest.QueryParameters[param.Key], param.Value);
            }

            return new T();
        }

        private Dictionary<string, List<string>> CreateHeaders(Uri uri)
        {
            Dictionary<string, List<string>> headers = new Dictionary<string, List<string>>();

            List<string> acceptHeaderValue = new List<string>
            {
                "application/json"
            };
            headers.Add("accept", acceptHeaderValue);

            List<string> contentHeaderValue = new List<string>
            {
                "application/json"
            };
            headers.Add("content-type", contentHeaderValue);

            List<string> regionHeaderValue = new List<string>
            {
                payConfig.Region.ToShortform()
            };
            headers.Add(Constants.Headers.Region, regionHeaderValue);

            List<string> dateHeaderValue = new List<string>
            {
                "20181013T004102Z"
            };
            headers.Add(Constants.Headers.Date, dateHeaderValue);

            List<string> hostHeaderValue = new List<string>
            {
                uri.Host
            };
            headers.Add(Constants.Headers.Host, hostHeaderValue);

            return headers;
        }

        public interface IClientMapping
        {
            ISignatureHelper signatureHelper { get; }
            Dictionary<string, string> SignRequest(ApiRequest request);

            T ProcessRequest<T>(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders)
                where T : AmazonPayResponse, new();

            HttpWebResponse SendRequest(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders);

            string BuildAuthorizationHeader(Dictionary<string, List<string>> preSignedHeaders, string signature);
        }

        public class TestClient : Client
        {
            public TestClient(ApiConfiguration payConfiguration) : base(payConfiguration) { }

            public T Invoke<T>(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders)  where T : AmazonPayResponse, new()
            {
                return base.ProcessRequest<T>(apiRequest, postSignedHeaders);
            }
        }

        public interface ITestClientMapping
        {
            HttpWebResponse SendRequest(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders);
        }

    }
}