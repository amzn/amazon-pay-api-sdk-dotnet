using System;
using System.IO;
using Amazon.Pay.API.Types;
using FluentAssertions;
using NUnit.Framework;
using Environment = Amazon.Pay.API.Types.Environment;

namespace Amazon.Pay.API.SDK.Tests
{
    [TestFixture]
    public class ApiConfigurationTests
    {
        [Test]
        public void CanInstantiateApiConfiguration()
        {
            var payConfig = new ApiConfiguration
            (
                region: Region.UnitedStates,
                environment: Environment.Sandbox,
                publicKeyId: "foo",
                privateKey: "-----BEGIN RSA PRIVATE KEY-----" // fake a private key ..);
            );
            Assert.NotNull(payConfig);
            Assert.AreEqual(Region.UnitedStates, payConfig.Region);
            Assert.AreEqual(Environment.Sandbox, payConfig.Environment);
            Assert.AreEqual("foo", payConfig.PublicKeyId);
            Assert.AreEqual(3, payConfig.MaxRetries);
            Assert.AreEqual(Constants.ApiVersion, payConfig.ApiVersion);
            payConfig.PrivateKey.Should().StartWith("-----BEGIN RSA");
        }

        [Test]
        public void CanSetMaxRetries()
        {
            var payConfig = new ApiConfiguration
            (
                region: Region.UnitedStates,
                environment: Environment.Sandbox,
                publicKeyId: "foo",
                privateKey: "-----BEGIN RSA PRIVATE KEY-----" // fake a private key ..);
            );
            Assert.NotNull(payConfig);
            payConfig.MaxRetries = 2;
            Assert.AreEqual(2, payConfig.MaxRetries);
        }

        [Test]
        public void CanSetPrivateKey()
        {
            var payConfig = new ApiConfiguration
            (
                region: Region.UnitedStates,
                environment: Environment.Sandbox,
                publicKeyId: "foo",
                privateKey: "-----BEGIN RSA PRIVATE KEY-----" // fake a private key ..);
            );
            Assert.NotNull(payConfig);
            payConfig.PrivateKey = "-----BEGIN RSA PRIVATE KEY-----\nMIIEvA";
            payConfig.PrivateKey.Should().EndWith("MIIEvA");
        }

        [Test]
        public void CanThrowPrivateKeyFileNotFoundException()
        {
            Action invocation = () => ThrowFileNotFoundException("null");

            invocation.Should().Throw<FileNotFoundException>().WithMessage("Provided private key file cannot be found");
        }

        public void ThrowFileNotFoundException(string privateKey)
        {
            var payConfig = new ApiConfiguration
            (
                region: Region.UnitedStates,
                environment: Environment.Sandbox,
                publicKeyId: "foo",
                privateKey: privateKey 
            );
        }
    }
}