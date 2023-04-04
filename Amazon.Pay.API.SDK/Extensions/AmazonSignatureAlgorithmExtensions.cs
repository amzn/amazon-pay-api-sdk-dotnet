using Amazon.Pay.API.Attributes;
using Amazon.Pay.API.Types;
using System.Linq;


namespace Amazon.Pay.API
{
    public static class AmazonSignatureAlgorithmExtensions
    {
        private static AmazonSignatureAlgorithmAttribute GetAmazonSignatureAlgorithmAttribute(AmazonSignatureAlgorithm algorithm)
        {
            var enumType = typeof(AmazonSignatureAlgorithm);
            var memberInfos = enumType.GetMember(algorithm.ToString());
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
            var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(AmazonSignatureAlgorithmAttribute), false);
            var attribute = (AmazonSignatureAlgorithmAttribute)valueAttributes[0];

            return attribute;
        }

        /// <summary>
        /// Returns the name of the Amazon signature Algorithm, "AMZN-PAY-RSASSA-PSS-V2" for V2 and "AMZN-PAY-RSASSA-PSS" for Default
        /// </summary>
        public static string GetName(this AmazonSignatureAlgorithm algorithm)
        {
            var attribute = GetAmazonSignatureAlgorithmAttribute(algorithm);
            return attribute.Name;
        }

        /// <summary>
        /// Returns the salt length of the Amazon signature Algorithm, 32 for V2 and 20 for Default
        /// </summary>
        public static int GetSaltLength(this AmazonSignatureAlgorithm algorithm)
        {
            var attribute = GetAmazonSignatureAlgorithmAttribute(algorithm);
            return attribute.SaltLength;
        }
    }
}
