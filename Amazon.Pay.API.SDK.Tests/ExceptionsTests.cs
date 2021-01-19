using System;
using Amazon.Pay.API.Exceptions;
using NUnit.Framework;
using FluentAssertions;

namespace Amazon.Pay.API.SDK.Tests
{
    [TestFixture]
    public class ExceptionsTests
    {
        [Test]
        public void CanThrowAmazonPayClientException()
        {
            Action invocation = () => ThrowAmazonPayClientException(null);

            invocation.Should().Throw<AmazonPayClientException>().WithMessage("test exception");
        }

        [Test]
        public void CanThrowAmazonPayClientExceptionNested()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Action invocation = () => ThrowAmazonPayClientException(ex);

                invocation.Should().Throw<AmazonPayClientException>().WithMessage("test exception");
            }
        }

        [Test]
        public void CanThrowApiConfigurationException()
        {
            Action invocation = () => ThrowApiConfigurationException(null);

            invocation.Should().Throw<ApiConfigurationException>().WithMessage("test exception");
        }

        [Test]
        public void CanThrowApiConfigurationExceptionNested()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Action invocation = () => ThrowApiConfigurationException(ex);

                invocation.Should().Throw<ApiConfigurationException>().WithMessage("test exception");
            }
        }

        public void ThrowAmazonPayClientException(Exception ex = null)
        {
            if (ex == null)
                throw new AmazonPayClientException("test exception");

            throw new AmazonPayClientException("test exception", ex);
        }

        public void ThrowApiConfigurationException(Exception ex = null)
        {
            if (ex == null)
                throw new ApiConfigurationException("test exception");

            throw new ApiConfigurationException("test exception", ex);
        }
    }
}