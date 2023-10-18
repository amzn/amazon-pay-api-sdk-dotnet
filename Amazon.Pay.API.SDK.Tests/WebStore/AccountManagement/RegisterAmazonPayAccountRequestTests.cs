using Amazon.Pay.API.WebStore.AccountManagement;
using Amazon.Pay.API.WebStore.Types;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.AccountManagement
{
    [TestFixture]
    public class RegisterAmazonPayAccountRequestTests
    {
        [Test]
        public void CanConstructWithAllPropertiesInitialized()
        {
            // act
            var request = new RegisterAmazonPayAccountRequest(uniqueReferenceId: "ABCDEF1234", ledgerCurrency: LedgerCurrency.JPY);            
            
            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.BusinessInfo);
            Assert.IsNotNull(request.PrimaryContactPerson);
        }

        private RegisterAmazonPayAccountRequest CreateIndividualAccountRequest()
        {
            var request = new RegisterAmazonPayAccountRequest(uniqueReferenceId: "ABCDEF12345", ledgerCurrency: LedgerCurrency.JPY)
            {
                BusinessInfo = {
                    BusinessType = BusinessType.INDIVIDUAL,
                    CountryOfEstablishment = "JP",
                    BusinessLegalName = "TestingSubmerchant",
                    BusinessAddress = {
	                    AddressLine1 = "450",
	                    AddressLine2 = "Noda",
	                    City = "香取市",
	                    StateOrRegion = "千葉県",
	                    PostalCode = "289-0314",
                        CountryCode = "JP"
                    }
                }
            };
            return request;
        }
        
        private RegisterAmazonPayAccountRequest CreateCorporateAccountRequest()
        {
            var request = new RegisterAmazonPayAccountRequest(uniqueReferenceId: "ABCDEF12345", ledgerCurrency: LedgerCurrency.JPY)
            {
                BusinessInfo = {
                    BusinessType = BusinessType.CORPORATE,
                    CountryOfEstablishment = "JP",
                    BusinessLegalName = "TestingSubmerchant",
                    BusinessAddress = {
	                    AddressLine1 = "450",
	                    AddressLine2 = "Noda",
	                    City = "香取市",
	                    StateOrRegion = "千葉県",
	                    PostalCode = "289-0314",
                        CountryCode = "JP"
                    }
                }
            };
            return request;
        }

        private RegisterAmazonPayAccountRequest CreateCorporateAccountRequestWithPoc()
        {
            var request = CreateCorporateAccountRequest();
            request.PrimaryContactPerson.PersonFullName = "Testing";
            request.PrimaryContactPerson.ResidentialAddress.AddressLine1 = "621-4";
            request.PrimaryContactPerson.ResidentialAddress.AddressLine2 = "Haramanda";
            request.PrimaryContactPerson.ResidentialAddress.City = "Arao City";
            request.PrimaryContactPerson.ResidentialAddress.StateOrRegion = "Kumamoto";
            request.PrimaryContactPerson.ResidentialAddress.PostalCode = "836-0806";
            request.PrimaryContactPerson.ResidentialAddress.CountryCode = "JP";
            return request;
        }

        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var request = new RegisterAmazonPayAccountRequest(uniqueReferenceId: "ABCDEF1234", ledgerCurrency: LedgerCurrency.JPY);
            
            // act
            string json = request.ToJson();
            
            // assert
            Assert.AreEqual("{\"uniqueReferenceId\":\"ABCDEF1234\",\"ledgerCurrency\":\"JPY\",\"businessInfo\":{}}", json);
        }

        [Test]
        public void RegisterAmazonPayAccountWithIndividualBusinessType()
        {
            // arrange
            var request = CreateIndividualAccountRequest();
            
            // act
            string json = request.ToJson();
            
            // assert
            const string expectedJson = "{\"uniqueReferenceId\":\"ABCDEF12345\",\"ledgerCurrency\":\"JPY\",\"businessInfo\":{\"businessType\":\"INDIVIDUAL\",\"countryOfEstablishment\":\"JP\",\"businessLegalName\":\"TestingSubmerchant\",\"businessAddress\":{\"addressLine1\":\"450\",\"addressLine2\":\"Noda\",\"city\":\"香取市\",\"stateOrRegion\":\"千葉県\",\"postalCode\":\"289-0314\",\"countryCode\":\"JP\"}}}";
            Assert.AreEqual(expectedJson, json);
        }
        
        [Test]
        public void RegisterAmazonPayAccountWithCorporateBusinessTypeAndWithoutPoc()
        {
            // arrange
            var request = CreateCorporateAccountRequest();
            
            // act
            string json = request.ToJson();
            
            // assert
            const string expectedJson = "{\"uniqueReferenceId\":\"ABCDEF12345\",\"ledgerCurrency\":\"JPY\",\"businessInfo\":{\"businessType\":\"CORPORATE\",\"countryOfEstablishment\":\"JP\",\"businessLegalName\":\"TestingSubmerchant\",\"businessAddress\":{\"addressLine1\":\"450\",\"addressLine2\":\"Noda\",\"city\":\"香取市\",\"stateOrRegion\":\"千葉県\",\"postalCode\":\"289-0314\",\"countryCode\":\"JP\"}}}";
            Assert.AreEqual(expectedJson, json);
        }
        
        [Test]
        public void RegisterAmazonPayAccountWithCorporateBusinessTypeAndPoc()
        {
            // arrange
            var request = CreateCorporateAccountRequestWithPoc();
            
            // act
            string json = request.ToJson();
            
            // assert
            const string expectedJson = "{\"uniqueReferenceId\":\"ABCDEF12345\",\"ledgerCurrency\":\"JPY\",\"businessInfo\":{\"businessType\":\"CORPORATE\",\"countryOfEstablishment\":\"JP\",\"businessLegalName\":\"TestingSubmerchant\",\"businessAddress\":{\"addressLine1\":\"450\",\"addressLine2\":\"Noda\",\"city\":\"香取市\",\"stateOrRegion\":\"千葉県\",\"postalCode\":\"289-0314\",\"countryCode\":\"JP\"}},\"primaryContactPerson\":{\"personFullName\":\"Testing\",\"residentialAddress\":{\"addressLine1\":\"621-4\",\"addressLine2\":\"Haramanda\",\"city\":\"Arao City\",\"stateOrRegion\":\"Kumamoto\",\"postalCode\":\"836-0806\",\"countryCode\":\"JP\"}}}"; 
            Assert.AreEqual(expectedJson, json);
        }
    }
}