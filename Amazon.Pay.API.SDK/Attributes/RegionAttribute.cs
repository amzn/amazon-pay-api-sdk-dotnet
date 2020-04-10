using System;

namespace Amazon.Pay.API.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)] // can't limit to enum fields, so using field as best workaround
    public class RegionAttribute : Attribute
    {
        public RegionAttribute(string shortform, string domain)
        {
            ShortForm = shortform;
            Domain = domain;
        }

        public string ShortForm { get; }

        public string Domain { get; }
    }
}
