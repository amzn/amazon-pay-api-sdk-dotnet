using Amazon.Pay.API.WebStore.AccountManagement;
using Amazon.Pay.API.WebStore.Types;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.AccountManagement
{
    [TestFixture]
    public class UpdateAmazonPayAccountRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitialized()
        {
            // act
            var request = new UpdateAmazonPayAccountRequest();

            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.BusinessInfo);
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var request = new UpdateAmazonPayAccountRequest();

            // act
            string json = request.ToJson();

            // assert
            Assert.AreEqual("{\"businessInfo\":{}}", json);
        }

        private UpdateAmazonPayAccountRequest CreateUpdatePayload()
        {
            var request = new UpdateAmazonPayAccountRequest()
            {
                BusinessInfo = {
                    BusinessAddress = {
                        AddressLine1 = "41",
                        AddressLine2 = "Nishigamo Kitaimaharacho",
                        City = "京都市",
                        StateOrRegion = "京都府",
                        PostalCode = "683-8821",
                        CountryCode = "JP"
                    }
                }
            };
            return request;
        }

        [Test]
        public void UpdateAmazonPayAccount()
        {
            // arrange
            var request = CreateUpdatePayload();

            // act
            string json = request.ToJson();

            // assert
            const string expectedJson = "{\"businessInfo\":{\"businessAddress\":{\"addressLine1\":\"41\",\"addressLine2\":\"Nishigamo Kitaimaharacho\",\"city\":\"京都市\",\"stateOrRegion\":\"京都府\",\"postalCode\":\"683-8821\",\"countryCode\":\"JP\"}}}";
	
            Assert.AreEqual(expectedJson, json);
        }

        [Test]
        public void UpdateAmazonPayAccountWithPrimaryContactPerson()
        {
            // arrange
            var request = CreateUpdatePayload();
            request.BusinessInfo.BusinessType = BusinessType.CORPORATE;
            request.BusinessInfo.CountryOfEstablishment = "JP";
            request.BusinessInfo.BusinessLegalName = "TestingSubmerchant";
            request.PrimaryContactPerson.DateOfBirth = "19971111";
            request.PrimaryContactPerson.PersonFullName = "Test";

            // act
            string json = request.ToJson();

            // assert
            const string expectedJson = "{\"businessInfo\":{\"businessType\":\"CORPORATE\",\"countryOfEstablishment\":\"JP\",\"businessLegalName\":\"TestingSubmerchant\",\"businessAddress\":{\"addressLine1\":\"41\",\"addressLine2\":\"Nishigamo Kitaimaharacho\",\"city\":\"京都市\",\"stateOrRegion\":\"京都府\",\"postalCode\":\"683-8821\",\"countryCode\":\"JP\"}},\"primaryContactPerson\":{\"personFullName\":\"Test\",\"dateOfBirth\":\"19971111\"}}";
            Assert.AreEqual(expectedJson, json);
        }
    }
}