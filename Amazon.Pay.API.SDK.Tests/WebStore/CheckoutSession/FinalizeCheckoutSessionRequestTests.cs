using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Types;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.CheckoutSession
{
    [TestFixture]
    public class FinalizeCheckoutSessionRequestTests
    {
        [Test]
        public void CanConvertToJsonMinimal()
        {
            // arrange
            var request = new FinalizeCheckoutSessionRequest(10, Currency.USD, PaymentIntent.Confirm);
            
            // act
            string json = request.ToJson();

            // assert
            Assert.AreEqual("{\"paymentIntent\":\"Confirm\",\"chargeAmount\":{\"amount\":10,\"currencyCode\":\"USD\"}}", json);
        }
        
        [Test]
        public void CanConstructWithAllPropertiesInitialized()
        {
            // act
            var request = new FinalizeCheckoutSessionRequest(10, Currency.USD, PaymentIntent.Confirm);
            
            // assert
            Assert.IsNotNull(request);
            Assert.IsNotNull(request.ShippingAddress);
            Assert.IsNotNull(request.ChargeAmount);
            Assert.IsNotNull(request.TotalOrderAmount);
            Assert.IsNotNull(request.PaymentIntent);
        }

        [Test]
        public void FinalizeCheckoutSessionResponseTest()
        {
            // arrange
            var request = new FinalizeCheckoutSessionRequest(amount: 10, currency: Currency.USD, PaymentIntent.Confirm);
            request.ShippingAddress.Name = "Susie Smith";
            request.ShippingAddress.AddressLine1 = "10 Ditka Ave";
            request.ShippingAddress.AddressLine2 = "Suite 2500";
            request.ShippingAddress.City = "Chicago";
            request.ShippingAddress.County = null;
            request.ShippingAddress.District = null;
            request.ShippingAddress.StateOrRegion = "IL";
            request.ShippingAddress.PostalCode = "60602";
            request.ShippingAddress.CountryCode = "US";
            request.ShippingAddress.PhoneNumber = "800-000-0000";
            request.TotalOrderAmount.Amount = 10;
            request.TotalOrderAmount.CurrencyCode = Currency.USD;
            request.CanHandlePendingAuthorization = false;

            // act
            string json = request.ToJson();
            // assert
            Assert.AreEqual("{\"paymentIntent\":\"Confirm\",\"canHandlePendingAuthorization\":false,\"shippingAddress\":{\"name\":\"Susie Smith\",\"phoneNumber\":\"800-000-0000\",\"addressLine1\":\"10 Ditka Ave\",\"addressLine2\":\"Suite 2500\",\"city\":\"Chicago\",\"stateOrRegion\":\"IL\",\"postalCode\":\"60602\",\"countryCode\":\"US\"},\"chargeAmount\":{\"amount\":10,\"currencyCode\":\"USD\"},\"totalOrderAmount\":{\"amount\":10,\"currencyCode\":\"USD\"}}", json);
        }
    }
    
}