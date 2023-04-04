using NUnit.Framework;
using System;
using System.Collections.Generic;
using Amazon.Pay.API.Types;

namespace Amazon.Pay.API.Tests
{
    [TestFixture]
    public class CreateStringToSignTest
    {
        private static readonly ApiConfiguration dummyConfig = new ApiConfiguration
        (
            region: Region.Europe,
            environment: Types.Environment.Live,
            publicKeyId: "foo",
            privateKey: "-----BEGIN RSA PRIVATE KEY-----" // fake a private key ..);
        );
        private static readonly ApiConfiguration dummyConfigWithAlgorithm = new ApiConfiguration
        (
        region: Region.Europe,
            environment: Types.Environment.Live,
            publicKeyId: "foo",
            privateKey: "-----BEGIN RSA PRIVATE KEY-----", // fake a private key ..);
            algorithm: AmazonSignatureAlgorithm.V2
        );

        private static readonly CanonicalBuilder canonicalBuilder = new CanonicalBuilder();
        private static readonly SignatureHelper signatureHelper = new SignatureHelper(dummyConfig, canonicalBuilder);
        private static readonly SignatureHelper signatureHelperWithAlgorithm = new SignatureHelper(dummyConfigWithAlgorithm, canonicalBuilder);
        private static Dictionary<string, List<string>> defaultHeaders;


        [SetUp]
        public void Init()
        {
            defaultHeaders = CreateHeaders();
        }

        [Test]
        public void SimpleGet()
        {
            SimpleGet(signatureHelper, dummyConfig.Algorithm.GetName());
            SimpleGet(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void GetWithTooManySlashesInUri()
        {
            GetWithTooManySlashesInUri(signatureHelper, dummyConfig.Algorithm.GetName());
            GetWithTooManySlashesInUri(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void GetWithUnreservedCharacters()
        {
            GetWithTooManySlashesInUri(signatureHelper, dummyConfig.Algorithm.GetName());
            GetWithTooManySlashesInUri(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void GetWithHighAsciiCharacterInParameterString()
        {
            GetWithHighAsciiCharacterInParameterString(signatureHelper, dummyConfig.Algorithm.GetName());
            GetWithHighAsciiCharacterInParameterString(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void SimplePost()
        {
            SimplePost(signatureHelper, dummyConfig.Algorithm.GetName());
            SimplePost(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void PostWithQueryParameter()
        {
            PostWithQueryParameter(signatureHelper, dummyConfig.Algorithm.GetName());
            PostWithQueryParameter(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void GetWithMultipleQueryValuesInSingleParameter()
        {
            GetWithMultipleQueryValuesInSingleParameter(signatureHelper, dummyConfig.Algorithm.GetName());
            GetWithMultipleQueryValuesInSingleParameter(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void GetWithMultipleQueryParameters()
        {
            GetWithMultipleQueryParameters(signatureHelper, dummyConfig.Algorithm.GetName());
            GetWithMultipleQueryParameters(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void GetWithMultipleQueryParametersSorted()
        {
            GetWithMultipleQueryParametersSorted(signatureHelper, dummyConfig.Algorithm.GetName());
            GetWithMultipleQueryParametersSorted(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void GetWithMultipleQueryValuesInSingleParameterSorted()
        {
            GetWithMultipleQueryValuesInSingleParameterSorted(signatureHelper, dummyConfig.Algorithm.GetName());
            GetWithMultipleQueryValuesInSingleParameterSorted(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void GetWithReservedCharactersInQueryParameter()
        {
            GetWithReservedCharactersInQueryParameter(signatureHelper, dummyConfig.Algorithm.GetName());
            GetWithReservedCharactersInQueryParameter(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void PostWithSpecialCharactersInQueryParameter()
        {
            PostWithSpecialCharactersInQueryParameter(signatureHelper, dummyConfig.Algorithm.GetName());
            PostWithSpecialCharactersInQueryParameter(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        [Test]
        public void PostWithSpacesInQueryParameter()
        {
            PostWithSpacesInQueryParameter(signatureHelper, dummyConfig.Algorithm.GetName());
            PostWithSpacesInQueryParameter(signatureHelperWithAlgorithm, dummyConfigWithAlgorithm.Algorithm.GetName());
        }

        private Dictionary<string, List<string>> CreateHeaders()
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
                 dummyConfig.Region.ToShortform()
             };
            headers.Add(Constants.Headers.Region, regionHeaderValue);

            List<string> dateHeaderValue = new List<string>
             {
                 "20180524T223710Z"
             };
            headers.Add(Constants.Headers.Date, dateHeaderValue);

            List<string> hostHeaderValue = new List<string>
             {
                 "pay-api.amazon.eu"
             };
            headers.Add(Constants.Headers.Host, hostHeaderValue);

            return headers;
        }

        public void SimpleGet(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.GET;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            var expectedCanonicalRequest = "GET\n/\n\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\n13fdf6db844bdfb9e9c0e27a4251ca04e60c29ca2132249c5dd1cb09c26e22f5";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);

            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void GetWithTooManySlashesInUri(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.GET;
            var uri = new Uri("http://pay-api.amazon.eu///foo//");
            var apiRequest = new ApiRequest(uri, method);
            var expectedCanonicalRequest = "GET\n/foo/\n\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\nd8e13e1857bc9b5056cc8ccb2699812faa2c68960e00483b1390fdaf4a991cc4";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);

            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void GetWithUnreservedCharacters(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.GET;
            var uri = new Uri("http://pay-api.amazon.eu/-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
            var apiRequest = new ApiRequest(uri, method);
            var expectedCanonicalRequest = "GET\n/-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz\n\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\n585e034d38ed3d64c0cd77a9f357a4b4a0fc093eebe06f4b06f66845e3543038";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);

            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void GetWithHighAsciiCharacterInParameterString(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.GET;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            apiRequest.QueryParameters.Add("\u1234", new List<string>() { "bar" });

            var expectedCanonicalRequest = "GET\n/\n%E1%88%B4=bar\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\nf0597d2fdcf97baf28002461796e916a74e35073b44042296d2ed45bacf6ecf0";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);


            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void SimplePost(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.POST;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            var expectedCanonicalRequest = "POST\n/\n\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\n322307b5dae22c3d59350c3de9202a488f337f674c8f430b186282008264bd2b";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);


            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void PostWithQueryParameter(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.POST;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            apiRequest.QueryParameters.Add("foo", new List<string>() { "bar" });
            var expectedCanonicalRequest = "POST\n/\nfoo=bar\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\n9d61b5abddbfdcaf7810a8e83dfffe0a5d5f272d2676c0550d974d5383e94cd5";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);


            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void GetWithMultipleQueryValuesInSingleParameter(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.GET;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            apiRequest.QueryParameters.Add("foo", new List<string>() { "b", "a" });
            var expectedCanonicalRequest = "GET\n/\nfoo=a&foo=b\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\n937de37fb8aed8af4214c373580c72f40c525202d1975df6836ba6ed0062b902";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);


            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void GetWithMultipleQueryParameters(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.GET;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            apiRequest.QueryParameters.Add("a", new List<string>() { "foo" });
            apiRequest.QueryParameters.Add("b", new List<string>() { "foo" });
            var expectedCanonicalRequest = "GET\n/\na=foo&b=foo\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\n457de2535eedbd6ef25f5c8ec4653ee885b99838d722189928f4fdf8d3c3ef4f";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);


            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void GetWithMultipleQueryParametersSorted(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.GET;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            apiRequest.QueryParameters.Add("A.1", new List<string>() { "foo" });
            apiRequest.QueryParameters.Add("A.10", new List<string>() { "foo" });
            apiRequest.QueryParameters.Add("A.2", new List<string>() { "foo" });
            var expectedCanonicalRequest = "GET\n/\nA.1=foo&A.10=foo&A.2=foo\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\n43ae5290e6742d30e3822b62b6d759afedafbe01534f778175c336b455d632e7";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);


            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void GetWithMultipleQueryValuesInSingleParameterSorted(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.GET;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            apiRequest.QueryParameters.Add("foo", new List<string>() { "Zoo", "aha" });
            var expectedCanonicalRequest = "GET\n/\nfoo=Zoo&foo=aha\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\n7a4d89bd832cb93f2102f66bc8d3a0bc5f224c0bb758eab261dcd34a8ed2f117";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);


            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void GetWithReservedCharactersInQueryParameter(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.GET;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            apiRequest.QueryParameters.Add("-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", new List<string>() { "-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" });
            var expectedCanonicalRequest = "GET\n/\n-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz=-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\naa69ca06d93d9220f9aa16314d8556e18e1b0ae39802cd169bd3af6c08cd7164";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);

            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void PostWithSpecialCharactersInQueryParameter(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.POST;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            apiRequest.QueryParameters.Add("@#$%^&+=/,?><`\";:\\|][{} ", new List<string>() { "@#$%^&+=/,?><`\";:\\|][{} " });
            var expectedCanonicalRequest = "POST\n/\n%40%23%24%25%5E%26%2B%3D%2F%2C%3F%3E%3C%60%22%3B%3A%5C%7C%5D%5B%7B%7D%20=%40%23%24%25%5E%26%2B%3D%2F%2C%3F%3E%3C%60%22%3B%3A%5C%7C%5D%5B%7B%7D%20\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\n50709f5220d06911feee2d6aa8bf8453c8517f44527c0912e62b905d26c7e08f";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);

            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }

        public void PostWithSpacesInQueryParameter(SignatureHelper signatureHelper, string algorithm)
        {
            // arrange
            var method = HttpMethod.POST;
            var uri = new Uri("http://pay-api.amazon.eu/");
            var apiRequest = new ApiRequest(uri, method);
            apiRequest.QueryParameters.Add("f oo", new List<string>() { "b ar" });
            var expectedCanonicalRequest = "POST\n/\nf%20oo=b%20ar\naccept:application/json\ncontent-type:application/json\nx-amz-pay-date:20180524T223710Z\nx-amz-pay-host:pay-api.amazon.eu\nx-amz-pay-region:eu\n\naccept;content-type;x-amz-pay-date;x-amz-pay-host;x-amz-pay-region\ne3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            var expectedStringToSign = algorithm + "\n79481e4e38a053f7be8bda701c0d302d39df18bf468578b133c3be8e93fea86a";

            // act
            string actualCanonicalRequest = signatureHelper.CreateCanonicalRequest(apiRequest, defaultHeaders);
            string actualStringToSign = signatureHelper.CreateStringToSign(actualCanonicalRequest);

            // assert
            Assert.AreEqual(expectedCanonicalRequest, actualCanonicalRequest);
            Assert.AreEqual(expectedStringToSign, actualStringToSign);
        }
    }
}
