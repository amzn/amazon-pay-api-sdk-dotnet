using Amazon.Pay.API.Attributes;

namespace Amazon.Pay.API.Types
{
    /// <summary>
    /// Amazon Signature Algorithms used for signature generation
    /// </summary>
    public enum AmazonSignatureAlgorithm
    {
        /// <summary>
        /// Default Algorithm with salt length 20
        /// </summary>
        [AmazonSignatureAlgorithm(name: "AMZN-PAY-RSASSA-PSS", saltLength: 20)]
        Default,

        /// <summary>
        /// V2 Algorithm with salt length 32
        /// </summary>
        [AmazonSignatureAlgorithm(name: "AMZN-PAY-RSASSA-PSS-V2", saltLength: 32)]
        V2,

    }
}