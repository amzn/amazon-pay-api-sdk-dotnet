using NUnit.Framework;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using AmazonPayV2.types;
using AmazonPayV2;

namespace Tests
{
    [TestFixture]
    public class CreateStringToSignTest
    {
        private static readonly string TestFile = TestContext.CurrentContext.TestDirectory + Path.DirectorySeparatorChar + "testdata.js";
        private static readonly List<JObject> TestCases = new List<JObject>();
        private static readonly PayConfiguration config = new PayConfiguration
        {
            Region = Regions.eu,
            PublicKeyId = ""
        };
        private static readonly CanonicalBuilder canonicalBuilder = new CanonicalBuilder();
        private static readonly SignatureHelper signatureHelper = new SignatureHelper(config, canonicalBuilder);


        [SetUp]
        public void Init()
        {
            StreamReader TestFileStream = File.OpenText(TestFile);
            string fileTestContent = TestFileStream.ReadToEnd();
            TestFileStream.Close();
            JArray allTestcases = JArray.Parse(fileTestContent);
            foreach (JObject test in allTestcases)
            {
                TestCases.Add(test);
            }
        }

        [Test]
        public void ValidateAllTestCases()
        {
            foreach (JObject testCase in TestCases)
            {
                string name = (string)testCase.SelectToken("name");
                HTTPMethods method = (HTTPMethods)System.Enum.Parse(typeof(HTTPMethods), testCase.SelectToken("method").ToString().ToUpper());
                Uri uri = new Uri((string)testCase.SelectToken("uri"));
                string payload = (string)testCase.SelectToken("payload");
                Dictionary<string, List<string>> queryParams = GetParameters((JObject)testCase.SelectToken("parameters"));
                Dictionary<string, List<string>> preSignedHeaders = CreateHeaders(uri);

                string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(uri, method, queryParams, payload, preSignedHeaders);
                string expectedCanonicalRequest = (string)testCase.SelectToken("canonicalRequest");

                string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);
                string expectedStringToSign = (string)testCase.SelectToken("stringToSign");

                Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest, "Test Case Name : " + name);
                Assert.AreEqual(expectedStringToSign, actualStringToSign, "Test Case Name : " + name);
            }
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
                 config.Region.ToString()
             };
            headers.Add("X-Amz-Pay-Region", regionHeaderValue);

            List<string> dateHeaderValue = new List<string>
             {
                 "20180524T223710Z"
             };
            headers.Add("X-Amz-Pay-Date", dateHeaderValue);

            List<string> hostHeaderValue = new List<string>
             {
                 "pay-api.amazon.eu"
             };
            headers.Add("X-Amz-Pay-Host", hostHeaderValue);

            return headers;
        }

        private Dictionary<string, List<string>> GetParameters(JObject jObject)
        {
            Dictionary<string, List<string>> parameters = new Dictionary<string, List<string>>();
            foreach (JProperty property in jObject.Properties())
            {
                List<string> values = property.Value.ToObject<List<string>>();
                parameters.Add(property.Name, values);               
            }

            return parameters;
        }
    }
}
