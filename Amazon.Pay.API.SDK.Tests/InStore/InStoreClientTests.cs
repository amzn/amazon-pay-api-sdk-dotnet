using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Amazon.Pay.API.DeliveryTracker;
using Amazon.Pay.API.InStore;
using Amazon.Pay.API.InStore.Charge;
using Amazon.Pay.API.InStore.MerchantScan;
using Amazon.Pay.API.InStore.Refund;
using Amazon.Pay.API.SDK.Tests.WebStore;
using Amazon.Pay.API.Types;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Environment = Amazon.Pay.API.Types.Environment;


namespace Amazon.Pay.API.SDK.Tests.InStore
{
    [TestFixture]
    public class InStoreClientTests
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
        public void CanInstantiateInStoreClient()
        {
            var client = new InStoreClient(payConfig);
            Assert.NotNull(client);
        }

        [Test]
        public void CanMerchantScan()
        {
             mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<MerchantScanResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<MerchantScanResponse>(request, headers, HttpMethod.POST, Constants.Resources.InStore.MerchantScan));

            var testRequest = new MerchantScanRequest("stringScannedData","scanReferenceId", Currency.USD, "merchantCOE");
            var result = mockClient.Object.MerchantScan(testRequest);
        }

        public T AssertPreProcessRequestFlow<T>(ApiRequest apiRequest, Dictionary<string, string> postSignedHeaders, HttpMethod method, string pathContains) where T : AmazonPayResponse, new()
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

            return new T();
        }

        [Test]
        public void CanCharge()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<ChargeResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<ChargeResponse>(request, headers, HttpMethod.POST,
                    $"{Constants.Resources.InStore.Charge}"));
            var testRequest = new Amazon.Pay.API.InStore.Charge.CreateChargeRequest("123456789", 10, Currency.USD, "referenceID")
            {
                SoftDescriptor = "_softDescriptor"
            };
            var result = mockClient.Object.Charge(testRequest);
        }

        
        [Test]
        public void CanRefund()
        {
            mockClient.Protected().As<IClientMapping>()
                .Setup(c => c.ProcessRequest<RefundResponse>(It.IsAny<ApiRequest>(),
                    It.IsAny<Dictionary<string, string>>()))
                .Returns((ApiRequest request, Dictionary<string, string> headers) => AssertPreProcessRequestFlow<RefundResponse>(request, headers, HttpMethod.POST,
                    $"{Constants.Resources.InStore.Refund}"));
            var testRequest = new Amazon.Pay.API.InStore.Refund.CreateRefundRequest("123456789", 10, Currency.USD, "referenceID");
            var result = mockClient.Object.Refund(testRequest);
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
    }
}