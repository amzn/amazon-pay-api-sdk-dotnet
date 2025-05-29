using Amazon.Pay.API.WebStore.File;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.File
{
    [TestFixture]
    public class UploadFileRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitializedAsExpected()
        {
            // arrange
            var fileType = "jpg";
            var purpose = "disputeEvidence";

            // act
            var request = new UploadFileRequest(fileType, purpose);

            // assert
            Assert.IsNotNull(request);
            Assert.AreEqual(fileType, request.Type);
            Assert.AreEqual(purpose, request.Purpose);
        }

        [Test]
        public void CanConvertToJson()
        {
            // arrange
            var request = new UploadFileRequest("pdf", "disputeEvidence");

            // act
            var json = JsonConvert.SerializeObject(request);
            var expected = "{\"type\":\"pdf\",\"purpose\":\"disputeEvidence\"}";

            // assert
            Assert.AreEqual(expected, json);
        }
    }
}