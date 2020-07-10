using Amazon.Pay.API.WebStore.CheckoutSession;
using Amazon.Pay.API.WebStore.Types;
using NUnit.Framework;

namespace Amazon.Pay.API.Tests.WebStore.CheckoutSession
{
    [TestFixture]
    public class DeliverySpecificationsTests
    {
        [Test]
        public void OneCountryWithTwoStatesAndAnotherCountry()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );

            request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.Allowed;
            request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("US").AddStateOrRegionRestriction("WA").AddStateOrRegionRestriction("NY");
            request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("DE");

            // act
            var jsonResult = request.ToJson();
            var jsonResult2 = request.ToJson();

            // assert
            Assert.AreEqual(jsonResult, jsonResult2);
            Assert.IsTrue(jsonResult.Contains("\"deliverySpecifications\":{\"addressRestrictions\":{\"type\":\"Allowed\",\"restrictions\":{\"US\":{\"statesOrRegions\":[\"WA\",\"NY\"]},\"DE\":{}}}}"));
        }

        [Test]
        public void OneCountryWithOneStateAndOneZipCode()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );

            request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.Allowed;
            request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("DE").AddStateOrRegionRestriction("RLP").AddZipCodesRestriction("12345");

            // act
            var jsonResult = request.ToJson();
            var jsonResult2 = request.ToJson();

            // assert
            Assert.AreEqual(jsonResult, jsonResult2);
            Assert.IsTrue(jsonResult.Contains("addressRestrictions\":{\"type\":\"Allowed\",\"restrictions\":{\"DE\":{\"statesOrRegions\":[\"RLP\"],\"zipCodes\":[\"12345\"]}}}}"));
        }

        [Test]
        public void OneCountryWithMultipleStatesAndZipCodes()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );

            request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.Allowed;
            request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("DE").AddStateOrRegionRestriction("RLP")
                                                                                          .AddStateOrRegionRestriction("NRW")
                                                                                          .AddZipCodesRestriction("12345")
                                                                                          .AddZipCodesRestriction("34567");

            // act
            var jsonResult = request.ToJson();
            var jsonResult2 = request.ToJson();

            // assert
            Assert.AreEqual(jsonResult, jsonResult2);
            Assert.IsTrue(jsonResult.Contains("addressRestrictions\":{\"type\":\"Allowed\",\"restrictions\":{\"DE\":{\"statesOrRegions\":[\"RLP\",\"NRW\"],\"zipCodes\":[\"12345\",\"34567\"]}}}}"));
        }

        [Test]
        public void DenySingleCountry()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );

            request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.NotAllowed;
            request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("US");

            // act
            var jsonResult = request.ToJson();
            var jsonResult2 = request.ToJson();

            // assert
            Assert.AreEqual(jsonResult, jsonResult2);
            Assert.IsTrue(jsonResult.Contains("\"deliverySpecifications\":{\"addressRestrictions\":{\"type\":\"NotAllowed\",\"restrictions\":{\"US\":{}}}}"));
        }

        [Test]
        public void AllowSingleCountryOldFormat()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );

            var restriction = new Restriction();
            request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.Allowed;
            request.DeliverySpecifications.AddressRestrictions.Restrictions.Add("US", restriction);

            // act
            var jsonResult = request.ToJson();
            var jsonResult2 = request.ToJson();

            // assert
            Assert.AreEqual(jsonResult, jsonResult2);
            Assert.IsTrue(jsonResult.Contains("\"deliverySpecifications\":{\"addressRestrictions\":{\"type\":\"Allowed\",\"restrictions\":{\"US\":{}}}}"));
        }

        [Test]
        public void AllowSingleCountryButApplySpecialRestrictions()
        {
            // arrange
            var request = new CreateCheckoutSessionRequest
            (
                checkoutReviewReturnUrl: "https://example.com/review.html",
                storeId: "amzn1.application-oa2-client.000000000000000000000000000000000"
            );

            request.DeliverySpecifications.AddressRestrictions.Type = RestrictionType.Allowed;
            request.DeliverySpecifications.AddressRestrictions.AddCountryRestriction("DE");
            request.DeliverySpecifications.SpecialRestrictions.Add(SpecialRestriction.RestrictPackstations);
            request.DeliverySpecifications.SpecialRestrictions.Add(SpecialRestriction.RestrictPOBoxes);

            // act
            var jsonResult = request.ToJson();
            var jsonResult2 = request.ToJson();

            // assert
            Assert.AreEqual(jsonResult, jsonResult2);
            Assert.IsTrue(jsonResult.Contains("\"deliverySpecifications\":{\"specialRestrictions\":[\"RestrictPackstations\",\"RestrictPOBoxes\"],\"addressRestrictions\":{\"type\":\"Allowed\",\"restrictions\":{\"DE\":{}}}}"));
        }

    }
}
