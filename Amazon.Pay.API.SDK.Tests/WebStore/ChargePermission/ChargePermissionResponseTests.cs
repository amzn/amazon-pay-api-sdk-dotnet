using Amazon.Pay.API.WebStore.ChargePermission;
using Amazon.Pay.API.WebStore.Types;
using NUnit.Framework;

namespace Amazon.Pay.API.SDK.Tests.WebStore.ChargePermission
{
    [TestFixture]
    public class ChargePermissionResponseTests
    {
        [Test]
        public void CanSupportNullExpirationTimestamp()
        {
            var response = "{\"chargePermissionId\":\"S00-0000000-0000000\",\"amazonPayToken\":\"000000000000AMZN\",\"chargePermissionReferenceId\":null,\"platformId\":null,\"buyer\":{\"name\":\"Test Buyer SBX\",\"email\":\"testemail@email.com\",\"buyerId\":\"amzn1.account.AAAAAAAAAAAAAAAAAAAAAAAAAAAA\",\"primeMembershipTypes\":null,\"phoneNumber\":\"1234567890\"},\"shippingAddress\":null,\"billingAddress\":{\"name\":\"Christopher C. Conn\",\"addressLine1\":\"4996 Rockford Mountain Lane\",\"addressLine2\":null,\"addressLine3\":null,\"city\":\"Appleton\",\"county\":null,\"district\":null,\"stateOrRegion\":\"WI\",\"postalCode\":\"54911\",\"countryCode\":\"US\",\"phoneNumber\":\"1234567890\"},\"paymentPreferences\":[{\"paymentDescriptor\":null}],\"statusDetails\":{\"state\":\"Chargeable\",\"reasons\":null,\"lastUpdatedTimestamp\":\"20211215T152829Z\"},\"creationTimestamp\":\"20211215T152829Z\",\"expirationTimestamp\":null,\"merchantMetadata\":{\"merchantReferenceId\":\"REPLACEMENT-TOKEN-350-3\",\"merchantStoreName\":\"SYF\",\"noteToBuyer\":null,\"customInformation\":null},\"releaseEnvironment\":\"Sandbox\",\"limits\":{\"amountLimit\":null,\"amountBalance\":null},\"chargePermissionType\":\"OneTime\",\"recurringMetadata\":{\"frequency\":null,\"amount\":null},\"presentmentCurrency\":\"USD\"}";
            var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyyMMddTHHmmssZ" };
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChargePermissionResponse>(response, dateConverter);

            Assert.IsNull(result.ExpirationTimestamp);
        }

        [Test]
        public void TestChargePermissionTypeAsOneTime()
        {
            // ChargePermissionType - One Time
            var response = "{\"chargePermissionId\":\"S00-0000000-0000000\",\"amazonPayToken\":\"000000000000AMZN\",\"chargePermissionReferenceId\":null,\"platformId\":null,\"buyer\":{\"name\":\"Test Buyer SBX\",\"email\":\"testemail@email.com\",\"buyerId\":\"amzn1.account.AAAAAAAAAAAAAAAAAAAAAAAAAAAA\",\"primeMembershipTypes\":null,\"phoneNumber\":\"1234567890\"},\"shippingAddress\":null,\"billingAddress\":{\"name\":\"Christopher C. Conn\",\"addressLine1\":\"4996 Rockford Mountain Lane\",\"addressLine2\":null,\"addressLine3\":null,\"city\":\"Appleton\",\"county\":null,\"district\":null,\"stateOrRegion\":\"WI\",\"postalCode\":\"54911\",\"countryCode\":\"US\",\"phoneNumber\":\"1234567890\"},\"paymentPreferences\":[{\"paymentDescriptor\":null}],\"statusDetails\":{\"state\":\"Chargeable\",\"reasons\":null,\"lastUpdatedTimestamp\":\"20211215T152829Z\"},\"creationTimestamp\":\"20211215T152829Z\",\"expirationTimestamp\":null,\"merchantMetadata\":{\"merchantReferenceId\":\"REPLACEMENT-TOKEN-350-3\",\"merchantStoreName\":\"SYF\",\"noteToBuyer\":null,\"customInformation\":null},\"releaseEnvironment\":\"Sandbox\",\"limits\":{\"amountLimit\":null,\"amountBalance\":null},\"chargePermissionType\":\"OneTime\",\"recurringMetadata\":{\"frequency\":null,\"amount\":null},\"presentmentCurrency\":\"USD\"}";
            var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyyMMddTHHmmssZ" };
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChargePermissionResponse>(response, dateConverter);

            Assert.AreEqual(result.ChargePermissionType, ChargePermissionType.OneTime);
        }

        [Test]
        public void TestChargePermissionTypeAsRecurring()
        {
            // ChargePermissionType - Recurring
            var response = "{\"chargePermissionId\":\"S00-0000000-0000000\",\"amazonPayToken\":\"000000000000AMZN\",\"chargePermissionReferenceId\":null,\"platformId\":null,\"buyer\":{\"name\":\"Test Buyer SBX\",\"email\":\"testemail@email.com\",\"buyerId\":\"amzn1.account.AAAAAAAAAAAAAAAAAAAAAAAAAAAA\",\"primeMembershipTypes\":null,\"phoneNumber\":\"1234567890\"},\"shippingAddress\":null,\"billingAddress\":{\"name\":\"Christopher C. Conn\",\"addressLine1\":\"4996 Rockford Mountain Lane\",\"addressLine2\":null,\"addressLine3\":null,\"city\":\"Appleton\",\"county\":null,\"district\":null,\"stateOrRegion\":\"WI\",\"postalCode\":\"54911\",\"countryCode\":\"US\",\"phoneNumber\":\"1234567890\"},\"paymentPreferences\":[{\"paymentDescriptor\":null}],\"statusDetails\":{\"state\":\"Chargeable\",\"reasons\":null,\"lastUpdatedTimestamp\":\"20211215T152829Z\"},\"creationTimestamp\":\"20211215T152829Z\",\"expirationTimestamp\":null,\"merchantMetadata\":{\"merchantReferenceId\":\"REPLACEMENT-TOKEN-350-3\",\"merchantStoreName\":\"SYF\",\"noteToBuyer\":null,\"customInformation\":null},\"releaseEnvironment\":\"Sandbox\",\"limits\":{\"amountLimit\":null,\"amountBalance\":null},\"chargePermissionType\":\"Recurring\",\"recurringMetadata\":{\"frequency\":null,\"amount\":null},\"presentmentCurrency\":\"USD\"}";
            var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyyMMddTHHmmssZ" };
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChargePermissionResponse>(response, dateConverter);

            Assert.AreEqual(result.ChargePermissionType, ChargePermissionType.Recurring);
        }

        [Test]
        public void TestChargePermissionTypeAsPaymentMethodOnFile()
        {
            // ChargePermissionType - PaymentMethodOnFile
            var response = "{\"chargePermissionId\":\"S00-0000000-0000000\",\"amazonPayToken\":\"000000000000AMZN\",\"chargePermissionReferenceId\":null,\"platformId\":null,\"buyer\":{\"name\":\"Test Buyer SBX\",\"email\":\"testemail@email.com\",\"buyerId\":\"amzn1.account.AAAAAAAAAAAAAAAAAAAAAAAAAAAA\",\"primeMembershipTypes\":null,\"phoneNumber\":\"1234567890\"},\"shippingAddress\":null,\"billingAddress\":{\"name\":\"Christopher C. Conn\",\"addressLine1\":\"4996 Rockford Mountain Lane\",\"addressLine2\":null,\"addressLine3\":null,\"city\":\"Appleton\",\"county\":null,\"district\":null,\"stateOrRegion\":\"WI\",\"postalCode\":\"54911\",\"countryCode\":\"US\",\"phoneNumber\":\"1234567890\"},\"paymentPreferences\":[{\"paymentDescriptor\":null}],\"statusDetails\":{\"state\":\"Chargeable\",\"reasons\":null,\"lastUpdatedTimestamp\":\"20211215T152829Z\"},\"creationTimestamp\":\"20211215T152829Z\",\"expirationTimestamp\":null,\"paymentMethodOnFileMetadata\":{\"setupOnly\":true},\"merchantMetadata\":{\"merchantReferenceId\":\"REPLACEMENT-TOKEN-350-3\",\"merchantStoreName\":\"SYF\",\"noteToBuyer\":null,\"customInformation\":null},\"releaseEnvironment\":\"Sandbox\",\"limits\":{\"amountLimit\":null,\"amountBalance\":null},\"chargePermissionType\":\"PaymentMethodOnFile\",\"recurringMetadata\":{\"frequency\":null,\"amount\":null},\"presentmentCurrency\":\"USD\"}";
            var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyyMMddTHHmmssZ" };
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ChargePermissionResponse>(response, dateConverter);

            Assert.AreEqual(result.ChargePermissionType, ChargePermissionType.PaymentMethodOnFile);
        }
    }
}
