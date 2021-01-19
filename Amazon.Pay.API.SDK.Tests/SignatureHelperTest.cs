using Amazon.Pay.API.InStore.MerchantScan;
using Amazon.Pay.API.Types;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.Tests
{
    [TestFixture]
    public class SignatureHelperTest
    {
        private SignatureHelper signatureHelper;
        private readonly ApiConfiguration config = new ApiConfiguration
        (
            region: Region.Europe,
            environment: Types.Environment.Live,
            publicKeyId: "foo",
            privateKey: "-----BEGIN RSA PRIVATE KEY-----" // fake a private key ..);
        );
        private readonly string uriToTest = "http://pay-api.amazon.eu/";

        [Test]
        public void CreateStringToSignTest()
        {
            string canonicalRequest = "POST\n/live/in-store/v1/refund\naccept:application/json\ncontent-type:application/json\naccept;content-type\n95b0d65e9efb9f0b9e8c2f3b7744628c765477";
            var canonicalBuilder = new Mock<CanonicalBuilder>();
            canonicalBuilder.Setup(p => p.HashThenHexEncode(canonicalRequest)).Returns("95b0d65e9efb9f0b9e8c2f3b77");

            signatureHelper = new SignatureHelper(config, canonicalBuilder.Object);
            string actualStringTosign = signatureHelper.CreateStringToSign(canonicalRequest);
            string expectedStringToSign = "AMZN-PAY-RSASSA-PSS" + "\n" + "95b0d65e9efb9f0b9e8c2f3b77";

            canonicalBuilder.VerifyAll();
            Assert.AreEqual(expectedStringToSign, actualStringTosign);
        }

        [Test]
        public void CreateCanonicalRequestTest()
        {
            Uri uri = new Uri("https://pay-api.amazon.eu/sandbox/in-store/v1/merchantScan");

            var scanRequest = new MerchantScanRequest("UKhrmatMeKdlfY6b", "0b8fb271-2ae2-49a5-4901237", Currency.EUR, "DE", "FR");

            var preSignedHeaders = CreateHeaders(uri);
            string canonicalHeaderString = "accept:application/json" + "\n" + "content-type:application/json"
                                            + "\n" + "x-amz-pay-date:20181013T004102Z" + "\n" +
                                            "x-amz-pay-host:pay-api.amazon.eu" + "x -amz-pay-region:eu";

            var canonicalBuilder = new Mock<CanonicalBuilder>();
            canonicalBuilder.Setup(p => p.GetCanonicalizedURI("/sandbox/in-store/v1/merchantScan")).Returns("/sandbox/in-store/v1/merchantScan");
            canonicalBuilder.Setup(p => p.HashThenHexEncode(scanRequest.ToJson())).Returns("95b0d65e9efb9f0b9e8c2f3b77");
            canonicalBuilder.Setup(p => p.GetCanonicalizedHeaderString(preSignedHeaders)).Returns(canonicalHeaderString);
            canonicalBuilder.Setup(p => p.GetSignedHeadersString(preSignedHeaders)).Returns("accept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region");
            signatureHelper = new SignatureHelper(config, canonicalBuilder.Object);

            string expectedCanonicalRequest = "POST" + "\n" + "/sandbox/in-store/v1/merchantScan" +
                                                "\n" + "\n" + canonicalHeaderString + "\n"
                                                + "accept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region" + "\n"
                                                + "95b0d65e9efb9f0b9e8c2f3b77";

            var apiRequest = new ApiRequest(uri, HttpMethod.POST);
            apiRequest.Body = scanRequest;

            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, preSignedHeaders);

            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
        }

        [Test]
        public void ButtonPayloadAsJsonResultsInExpectedSignatureString()
        {
            var signatureHelper = new SignatureHelper(config, new CanonicalBuilder());
            var payload = "{\"storeId\":\"amzn1.application-oa2-client.xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\",\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://localhost/test/CheckoutReview.php\",\"checkoutResultReturnUrl\":\"https://localhost/test/CheckoutResult.php\"}}";

            var stringToSign = signatureHelper.CreateStringToSign(payload);

            Assert.AreEqual("AMZN-PAY-RSASSA-PSS\n8dec52d799607be40f82d5c8e7ecb6c171e6591c41b1111a576b16076c89381c", stringToSign);
        }

        [Test]
        public void CanMockDefaultHeadersTest()
        {
            var mockSignatureHelper = new Mock<ISignatureHelper>(MockBehavior.Strict);
            mockSignatureHelper.SetupAllProperties();
            mockSignatureHelper.Setup<Dictionary<string, List<string>>>(x => x.CreateDefaultHeaders(It.IsAny<Uri>()))
                .Returns(CreateHeaders(new Uri(uriToTest)));

            // Ensure the Signature Helper can be mocked
            var result = mockSignatureHelper.Object.CreateDefaultHeaders(new Uri(uriToTest));
            Assert.NotNull(result);
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void CanCreateDefaultHeaders()
        {
            string canonicalRequest = "POST\n/live/in-store/v1/refund\naccept:application/json\ncontent-type:application/json\naccept;content-type\n95b0d65e9efb9f0b9e8c2f3b7744628c765477";
            var canonicalBuilder = new Mock<CanonicalBuilder>();
            canonicalBuilder.Setup(p => p.HashThenHexEncode(canonicalRequest)).Returns("95b0d65e9efb9f0b9e8c2f3b77");

            signatureHelper = new SignatureHelper(config, canonicalBuilder.Object);
            var result = signatureHelper.CreateDefaultHeaders(new Uri(uriToTest));
            Assert.NotNull(result);
            Assert.AreEqual(5, result.Count);
            Assert.True(result.ContainsKey(Constants.Headers.Date));
            Assert.True(result.ContainsKey(Constants.Headers.Region));
            Assert.True(result.ContainsKey(Constants.Headers.Host));
            Assert.True(result.ContainsKey("accept"));
            Assert.True(result.ContainsKey("content-type"));

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
                 config.Region.ToShortform()
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
    }
}
