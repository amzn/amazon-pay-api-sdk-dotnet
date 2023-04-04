using Amazon.Pay.API.InStore.MerchantScan;
using Amazon.Pay.API.Types;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Environment = Amazon.Pay.API.Types.Environment;

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
        private readonly ApiConfiguration configWithAlgorithm = new ApiConfiguration
        (
            region: Region.Europe,
            environment: Types.Environment.Live,
            publicKeyId: "foo",
            privateKey: "-----BEGIN RSA PRIVATE KEY-----", // fake a private key ..);
            algorithm: AmazonSignatureAlgorithm.V2
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
            var signatureHelperWithAlgorithm = new SignatureHelper(configWithAlgorithm, new CanonicalBuilder());
            var payload = "{\"storeId\":\"amzn1.application-oa2-client.xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\",\"webCheckoutDetails\":{\"checkoutReviewReturnUrl\":\"https://localhost/test/CheckoutReview.php\",\"checkoutResultReturnUrl\":\"https://localhost/test/CheckoutResult.php\"}}";

            var stringToSign = signatureHelper.CreateStringToSign(payload);
            var stringToSignWithAlgorithm = signatureHelperWithAlgorithm.CreateStringToSign(payload);

            Assert.AreEqual("AMZN-PAY-RSASSA-PSS\n8dec52d799607be40f82d5c8e7ecb6c171e6591c41b1111a576b16076c89381c", stringToSign);
            Assert.AreEqual("AMZN-PAY-RSASSA-PSS-V2\n8dec52d799607be40f82d5c8e7ecb6c171e6591c41b1111a576b16076c89381c", stringToSignWithAlgorithm);
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

        [Test]
        public void CanGenerateSignature()
        {
            // sample, pub/priv key pair for test only
            RsaKeyParameters pub1 = new RsaKeyParameters(false,
               new BigInteger("a56e4a0e701017589a5187dc7ea841d156f2ec0e36ad52a44dfeb1e61f7ad991d8c51056ffedb162b4c0f283a12a88a394dff526ab7291cbb307ceabfce0b1dfd5cd9508096d5b2b8b6df5d671ef6377c0921cb23c270a70e2598e6ff89d19f105acc2d3f0cb35f29280e1386b6f64c4ef22e1e1f20d0ce8cffb2249bd9a2137", 16),
               new BigInteger("010001", 16));

            RsaKeyParameters prv1 = new RsaPrivateCrtKeyParameters(
               new BigInteger("a56e4a0e701017589a5187dc7ea841d156f2ec0e36ad52a44dfeb1e61f7ad991d8c51056ffedb162b4c0f283a12a88a394dff526ab7291cbb307ceabfce0b1dfd5cd9508096d5b2b8b6df5d671ef6377c0921cb23c270a70e2598e6ff89d19f105acc2d3f0cb35f29280e1386b6f64c4ef22e1e1f20d0ce8cffb2249bd9a2137", 16),
               new BigInteger("010001", 16),
               new BigInteger("33a5042a90b27d4f5451ca9bbbd0b44771a101af884340aef9885f2a4bbe92e894a724ac3c568c8f97853ad07c0266c8c6a3ca0929f1e8f11231884429fc4d9ae55fee896a10ce707c3ed7e734e44727a39574501a532683109c2abacaba283c31b4bd2f53c3ee37e352cee34f9e503bd80c0622ad79c6dcee883547c6a3b325", 16),
               new BigInteger("e7e8942720a877517273a356053ea2a1bc0c94aa72d55c6e86296b2dfc967948c0a72cbccca7eacb35706e09a1df55a1535bd9b3cc34160b3b6dcd3eda8e6443", 16),
               new BigInteger("b69dca1cf7d4d7ec81e75b90fcca874abcde123fd2700180aa90479b6e48de8d67ed24f9f19d85ba275874f542cd20dc723e6963364a1f9425452b269a6799fd", 16),
               new BigInteger("28fa13938655be1f8a159cbaca5a72ea190c30089e19cd274a556f36c4f6e19f554b34c077790427bbdd8dd3ede2448328f385d81b30e8e43b2fffa027861979", 16),
               new BigInteger("1a8b38f398fa712049898d7fb79ee0a77668791299cdfa09efc0e507acb21ed74301ef5bfd48be455eaeb6e1678255827580a8e4e8e14151d1510a82a3f2e729", 16),
               new BigInteger("27156aba4126d24a81f3a528cbfb27f56886f840a9f6e86e17a44b94fe9319584b8e22fdde1e5a2e3bd8aa5ba8d8584194eb2190acf832b847f13a3d24a79f4d", 16));

            var message = "POST\n/live/in-store/v1/refund\naccept:application/json\ncontent-type:application/json\naccept;content-type\n95b0d65e9efb9f0b9e8c2f3b7744628c765477";
            byte[] bytesToSign = Encoding.UTF8.GetBytes(message);

            // Get string version of private key
            TextWriter textWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(prv1);
            pemWriter.Writer.Flush();
            var privateKey = textWriter.ToString();
            
            var helper =
                new SignatureHelper(new ApiConfiguration(Region.UnitedStates, Environment.Sandbox, "THISISONLYATEST", privateKey),
                    new CanonicalBuilder());

            var result = helper.GenerateSignature(message);
            var sigBytes = Convert.FromBase64String(result);

            PssSigner signer = new PssSigner(new RsaEngine(), new Sha256Digest(), 20);
            signer.Init(true, pub1);
            signer.BlockUpdate(bytesToSign, 0, bytesToSign.Length);
            var resultVerify = signer.VerifySignature(sigBytes);

            Assert.IsTrue(resultVerify);
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
