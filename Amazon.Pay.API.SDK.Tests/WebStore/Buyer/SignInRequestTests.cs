using Amazon.Pay.API.WebStore.Buyer;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Types;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.Buyer
{
    [TestFixture]
    public class SignInRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitialized()
        {
            // act
            var request = new SignInRequest
            (
                signInReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );

            // assert
            Assert.IsNotNull(request);
            Assert.AreEqual("https://example.com/review.html", request.SignInReturnUrl);
            Assert.AreEqual("amzn1.application-oa2-client.000000000000000000000000000000000", request.StoreId);
        }

        [Test]
        public void CanConstruct2WithAllPropertiesInitialized()
        {
            // act
            var request = new SignInRequest
            (
                signInReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000",
                signInScopes: SignInScope.Name
            ); 
            

            // assert
            Assert.IsNotNull(request);
            Assert.AreEqual("https://example.com/review.html", request.SignInReturnUrl);
            Assert.AreEqual("amzn1.application-oa2-client.000000000000000000000000000000000", request.StoreId);
            Assert.Contains(SignInScope.Name, request.SignInScopes);
        }

        [Test]
        public void CanConvertToJson()
        {
            // arrange
            var request = new SignInRequest("https://example.com/review.html", "amzn1.application-oa2-client.000000000000000000000000000000000", SignInScope.Name, SignInScope.Email);

            // act
            string json = request.ToJson();

            // assert
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"signInReturnUrl\":\"https://example.com/review.html\",\"signInScopes\":[\"name\",\"email\"]}", json);
        }
    }
}
