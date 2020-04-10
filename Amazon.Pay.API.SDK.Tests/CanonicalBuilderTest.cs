using Amazon.Pay.API.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Amazon.Pay.API.Tests
{
    [TestFixture]
    public class CanonicalBuilderTest
    {
        private ApiConfiguration config = new ApiConfiguration
        (
            region: Region.Europe,
            environment: Types.Environment.Live,
            publicKeyId: "foo",
            privateKey: "-----BEGIN RSA PRIVATE KEY-----" // fake a private key ..);
        );

        CanonicalBuilder canonicalBuilder = new CanonicalBuilder();
        readonly Uri uri = new Uri("https://pay-api.amazon.eu/sandbox/in-store/v1/merchantScan");

        [Test]
        public void GetCanonicalizedHeaderStringTest()
        {
            Dictionary<string, List<string>> preSignedHeaders = CreateHeaders(uri);
            string expectedCanonicalHeaderString = "accept:application/json\ncontent-type:application/json\nx-amz-pay-date:" + preSignedHeaders[Constants.Headers.Date][0] + "\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n";
            string actualCanonicalHeaderString = canonicalBuilder.GetCanonicalizedHeaderString(preSignedHeaders);

            Assert.AreEqual(expectedCanonicalHeaderString, actualCanonicalHeaderString);
        }

        [Test]
        public void GetCanonicalizedQueryStringTest()
        {
            Dictionary<string, List<string>> parametersMap = new Dictionary<string, List<string>>();
            List<string> amount = new List<string>
            {
                "100.50"
            };
            parametersMap.Add("amount", amount);

            List<string> info = new List<string>
            {
                "Information about order"
            };
            parametersMap.Add("customInformation", info);

            String expectedCanonicalQueryString = "amount=100.50&customInformation=Information%20about%20order";
            String actualCanonicalQueryString = canonicalBuilder.GetCanonicalizedQueryString(parametersMap);

            Assert.AreEqual(expectedCanonicalQueryString, actualCanonicalQueryString);
        }

        [Test]
        public void GetSignedHeadersStringTest()
        {
            Dictionary<string, List<string>> preSignedHeaders = CreateHeaders(uri);

            string expectedHeadersString = "accept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region";
            string actualHeadersString = canonicalBuilder.GetSignedHeadersString(preSignedHeaders);
            Assert.AreEqual(expectedHeadersString, actualHeadersString);
        }

        [Test]
        public void GetCanonicaledURITest()
        {
            string expectedURI = "/";
            string actualURI = canonicalBuilder.GetCanonicalizedURI("");
            Assert.AreEqual(expectedURI, actualURI);

            expectedURI = "/sandbox/in-store/v1/merchantScan";
            actualURI = canonicalBuilder.GetCanonicalizedURI("/sandbox/in-store/v1/merchantScan");
            Assert.AreEqual(expectedURI, actualURI);

            actualURI = canonicalBuilder.GetCanonicalizedURI("sandbox/in-store/v1/merchantScan");
            Assert.AreEqual(expectedURI, actualURI);
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
