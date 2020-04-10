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
