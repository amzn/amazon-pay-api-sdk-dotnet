using System;
using AmazonPayV2;
using AmazonPayV2.types;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class UtilTest
    {
        [Test]
        public void UrlEncodeTest()
        {
            string expectedUrl = Util.UrlEncode("/live/in-store/v1/refund", true);
            Assert.AreEqual(expectedUrl, "/live/in-store/v1/refund");
        }

        [Test]
        public void UrlEncodeWithRedundantSlashTest()
        {
            string expectedUrl = Util.UrlEncode("//", true);
            Assert.AreEqual(expectedUrl, "/");
        }

        [Test]
        public void UrlEncodeWithSpaceTest()
        {
            string expectedUrl = Util.UrlEncode("/ /foo", true);
            Assert.AreEqual(expectedUrl, "/%20/foo");
        }

        [Test]
        public void UrlEncodeWithRedundantSlashesTest()
        {
            string expectedUrl = Util.UrlEncode("///foo//", true);
            Assert.AreEqual(expectedUrl, "/foo/");
        }

        [Test]
        public void UrlEncodeWithUnreservedCharactersTest()
        {
            string expectedUrl = Util.UrlEncode("/-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", true);
            Assert.AreEqual(expectedUrl, "/-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
        }

        [Test]
        public void UrlEncodeWithUTF8CharactersTest()
        {
            string expectedUrl = Util.UrlEncode("/\u1234", true);
            Assert.AreEqual(expectedUrl, "/%E1%88%B4");
        }

        [Test]
        public void GetServiceURITest()
        {
            PayConfiguration payConfig1 = new PayConfiguration
            {
                Region = Regions.eu,
                Environment = Environments.sandbox
            };

            string expectedURL = "https://pay-api.amazon.eu/sandbox/";
            string actualURL = Util.GetServiceURI(payConfig1);
            Assert.AreEqual(expectedURL, actualURL);

        }

        [Test]
        public void CheckAndModifyIfBadgePayRequestTestWithCorrectBadgeId()
        {
            JObject scanRequestWithBadgeId = MockScanRequest("1234567");
            JObject actualScanRequest = Util.CheckAndModifyIfBadgePayRequest(scanRequestWithBadgeId);
            Assert.AreEqual("AMZ-EMP-1234567", actualScanRequest["scanData"].ToString());

            scanRequestWithBadgeId = MockScanRequest("!12345678976");
            actualScanRequest = Util.CheckAndModifyIfBadgePayRequest(scanRequestWithBadgeId);
            Assert.AreEqual("AMZ-EMP-12345678976", actualScanRequest["scanData"].ToString());

            scanRequestWithBadgeId = MockScanRequest("10658219");
            actualScanRequest = Util.CheckAndModifyIfBadgePayRequest(scanRequestWithBadgeId);
            Assert.AreEqual("AMZ-EMP-1234567", actualScanRequest["scanData"].ToString());

            scanRequestWithBadgeId = MockScanRequest("!10658219");
            actualScanRequest = Util.CheckAndModifyIfBadgePayRequest(scanRequestWithBadgeId);
            Assert.AreEqual("AMZ-EMP-1234567", actualScanRequest["scanData"].ToString());
        }

        [Test]
        public void CheckAndModifyIfBadgePayRequestTestWithoutBadgeId()
        {
            JObject scanRequestWithoutBadgeId = MockScanRequest("UKhrmatMeKdlfY6b");
            JObject actualScanRequest = Util.CheckAndModifyIfBadgePayRequest(scanRequestWithoutBadgeId);
            Assert.AreEqual("UKhrmatMeKdlfY6b", actualScanRequest["scanData"].ToString());

            scanRequestWithoutBadgeId = MockScanRequest("!123MeKd");
            actualScanRequest = Util.CheckAndModifyIfBadgePayRequest(scanRequestWithoutBadgeId);
            Assert.AreEqual("!123MeKd", actualScanRequest["scanData"].ToString());
        }

        [Test]
        public void CheckAndModifyIfBadgePayRequestTestWithEmptyScanData()
        {
            JObject scanRequestWithBadgeId = MockScanRequest("");
            JObject actualScanRequest = Util.CheckAndModifyIfBadgePayRequest(scanRequestWithBadgeId);

            Assert.AreEqual("", actualScanRequest["scanData"]);
        }

        [Test]
        public void CheckAndModifyIfBadgePayRequestTestWithNullScanData()
        {
            JObject scanRequestWithBadgeId = MockScanRequest("mock");
            scanRequestWithBadgeId.Remove("scanData");
            JObject actualScanRequest = Util.CheckAndModifyIfBadgePayRequest(scanRequestWithBadgeId);

            Assert.IsNull(actualScanRequest["scanData"]);
        }

        private JObject MockScanRequest(string scanData)
        {
            JObject storeLocation = new JObject
            {
                new JProperty("countryCode", "US")
            };
            JObject scanPayload = new JObject
            {
                new JProperty("scanData", scanData),
                new JProperty("scanReferenceId", "0b8fb271-2ae2-49a5"),
                new JProperty("merchantCOE", "US"),
                new JProperty("ledgerCurrency", "USD"),
                new JProperty("storeLocation", storeLocation)
            };

            return scanPayload;
        }
    }   
}
