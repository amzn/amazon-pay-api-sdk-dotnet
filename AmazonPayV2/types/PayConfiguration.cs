using AmazonPayV2.types;

namespace AmazonPayV2
{
    public class PayConfiguration
    {
        public Regions? Region { get; set; }
        public string PublicKeyId { get; set; }
        public string PrivateKey { get; set; }
        public Environments Environment { get; set; }
        public int MaxRetries { get; set; } = 3;
    }
}