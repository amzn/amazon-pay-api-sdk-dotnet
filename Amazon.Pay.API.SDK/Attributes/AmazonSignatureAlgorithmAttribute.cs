using System;

namespace Amazon.Pay.API.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)] // can't limit to enum fields, so using field as best workaround
    public class AmazonSignatureAlgorithmAttribute : Attribute
    {
        public AmazonSignatureAlgorithmAttribute(string name, int saltLength)
        {
            Name = name;
            SaltLength = saltLength;
        }

        public string Name { get; }

        public int SaltLength { get; }
    }
}
