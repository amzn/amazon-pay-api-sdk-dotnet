using Amazon.Pay.API.Attributes;
using Amazon.Pay.API.Types;
using System.Linq;

namespace Amazon.Pay.API
{
    public static class RegionExtensions
    {
        private static RegionAttribute GetRegionAttribute(Region region)
        {
            var enumType = typeof(Region);
            var memberInfos = enumType.GetMember(region.ToString());
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
            var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(RegionAttribute), false);
            var attribute = (RegionAttribute)valueAttributes[0];

            return attribute;
        }

        /// <summary>
        /// Returns the Amazon Pay defined shortform for the region, e.g. 'eu' for 'Europe'.
        /// </summary>
        public static string ToShortform(this Region region)
        {
            var attribute = GetRegionAttribute(region);
            return attribute.ShortForm;
        }

        /// <summary>
        /// Returns the API endpoint domain for the given region.
        /// </summary>
        public static string ToDomain(this Region region)
        {
            var attribute = GetRegionAttribute(region);
            return attribute.Domain;
        }
    }
}
