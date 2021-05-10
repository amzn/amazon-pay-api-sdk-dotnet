using System;
using System.Collections.Generic;
using Amazon.Pay.API.WebStore.Buyer;
using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Types;
using Moq;
using NUnit.Framework;
using System.Reflection;

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
        public void CanConstructWithAllPropertiesAndAllScopesInitialized()
        {
            // act
            SignInScope[] scopes = new SignInScope[] {
                SignInScope.Name, SignInScope.Email,
                SignInScope.PostalCode, SignInScope.ShippingAddress,
                SignInScope.PhoneNumber,
                SignInScope.BillingAddress,
                SignInScope.PrimeStatus
            };
            var request = new SignInRequest
            (
                signInReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000",
                signInScopes: scopes
            );

            // assert
            Assert.IsNotNull(request);
            Assert.AreEqual("https://example.com/review.html", request.SignInReturnUrl);
            Assert.AreEqual("amzn1.application-oa2-client.000000000000000000000000000000000", request.StoreId);
            Assert.Contains(SignInScope.Name, request.SignInScopes);
            Assert.Contains(SignInScope.Email, request.SignInScopes);
            Assert.Contains(SignInScope.PostalCode, request.SignInScopes);
            Assert.Contains(SignInScope.ShippingAddress, request.SignInScopes);
            Assert.Contains(SignInScope.PhoneNumber, request.SignInScopes);
            Assert.Contains(SignInScope.BillingAddress, request.SignInScopes);
            Assert.Contains(SignInScope.PrimeStatus, request.SignInScopes);
        }

        [Test]
        public void CanConvertToJson()
        {
            // arrange
            var request = new SignInRequest("https://example.com/review.html", "amzn1.application-oa2-client.000000000000000000000000000000000", SignInScope.Name, SignInScope.Email);

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"signInReturnUrl\":\"https://example.com/review.html\",\"signInScopes\":[\"name\",\"email\"]}", json);
        }

        [Test]
        public void CanConvertToJsonAllScopes()
        {
            // arrange
            var request = new SignInRequest(
                "https://example.com/review.html",
                "amzn1.application-oa2-client.000000000000000000000000000000000",
                SignInScope.Name, SignInScope.Email,
                SignInScope.PostalCode,
                SignInScope.ShippingAddress,
                SignInScope.PhoneNumber,
                SignInScope.BillingAddress,
                SignInScope.PrimeStatus);

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"signInReturnUrl\":\"https://example.com/review.html\",\"signInScopes\":[\"name\",\"email\",\"postalCode\",\"shippingAddress\",\"phoneNumber\",\"billingAddress\",\"primeStatus\"]}", json);
        }

        [Test]
        public void TestBillingAddressInResponseByPassingBillingAddressInScope()
        {
            // arrange
            var request = new SignInRequest("https://example.com/review.html", "amzn1.application-oa2-client.000000000000000000000000000000000", SignInScope.BillingAddress);

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"signInReturnUrl\":\"https://example.com/review.html\",\"signInScopes\":[\"billingAddress\"]}", json);
        }

        [Test]
        public void TestPrimeMemberShipStausInResponseByPassingPrimeStatusInScope()
        {
            // arrange
            var request = new SignInRequest("https://example.com/review.html", "amzn1.application-oa2-client.000000000000000000000000000000000", SignInScope.PrimeStatus);

            // act
            string json = request.ToJson();
            string json2 = request.ToJson();

            // assert
            Assert.AreEqual(json, json2);
            Assert.AreEqual("{\"storeId\":\"amzn1.application-oa2-client.000000000000000000000000000000000\",\"signInReturnUrl\":\"https://example.com/review.html\",\"signInScopes\":[\"primeStatus\"]}", json);
        }

        [Test]
        public void TestBuyerResponseHasPrimeMembershipType()
        {
            var membershipTypes = new List<string>() {"PRIME_GENERAL", "PRIME_STUDENT", "PRIME_GENERAL_US"};
            var buyer = new BuyerResponse();
            var I = buyer.GetType().GetProperty(nameof(BuyerResponse.PrimeMembershipTypes), BindingFlags.Public | BindingFlags.Instance);
            I.SetValue(buyer, membershipTypes);
            Assert.True(buyer.HasPrimeMembershipType(PrimeMembershipType.PRIME_GENERAL));
            Assert.True(buyer.HasPrimeMembershipType(PrimeMembershipType.PRIME_GENERAL_US));
            Assert.True(buyer.HasPrimeMembershipType(PrimeMembershipType.PRIME_STUDENT));
            Assert.False(buyer.HasPrimeMembershipType(PrimeMembershipType.NONE));

            I.SetValue(buyer, new List<string>());
            Assert.True(buyer.HasPrimeMembershipType(PrimeMembershipType.NONE));
            Assert.False(buyer.HasPrimeMembershipType(PrimeMembershipType.PRIME_GENERAL));
            Assert.False(buyer.HasPrimeMembershipType(PrimeMembershipType.PRIME_GENERAL_US));
            Assert.False(buyer.HasPrimeMembershipType(PrimeMembershipType.PRIME_STUDENT));

            I.SetValue(buyer, null);
            Assert.Throws<UnauthorizedAccessException>(() =>buyer.HasPrimeMembershipType(PrimeMembershipType.NONE));
            Assert.Throws<UnauthorizedAccessException>(() =>buyer.HasPrimeMembershipType(PrimeMembershipType.PRIME_GENERAL));
            Assert.Throws<UnauthorizedAccessException>(() =>buyer.HasPrimeMembershipType(PrimeMembershipType.PRIME_GENERAL_US));
            Assert.Throws<UnauthorizedAccessException>(() =>buyer.HasPrimeMembershipType(PrimeMembershipType.PRIME_STUDENT));
        }
    }
}

