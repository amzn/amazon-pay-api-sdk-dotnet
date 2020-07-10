using NUnit.Framework;
using System.Net;

namespace Amazon.Pay.API.Tests
{
    [TestFixture]
    public class UtilTest
    {
        [Test]
        public void UrlEncodeTest()
        {
            string expectedUrl = Util.UrlEncode("/live/in-store/v1/refund", true);
            Assert.AreEqual(expectedUrl, "/live/in-store/v1/refund");
        }

        [Test]
        public void UrlEncodeWithRedundantSlashTest()
        {
            string expectedUrl = Util.UrlEncode("//", true);
            Assert.AreEqual(expectedUrl, "/");
        }

        [Test]
        public void UrlEncodeWithSpaceTest()
        {
            string expectedUrl = Util.UrlEncode("/ /foo", true);
            Assert.AreEqual(expectedUrl, "/%20/foo");
        }

        [Test]
        public void UrlEncodeWithRedundantSlashesTest()
        {
            string expectedUrl = Util.UrlEncode("///foo//", true);
            Assert.AreEqual(expectedUrl, "/foo/");
        }

        [Test]
        public void UrlEncodeWithUnreservedCharactersTest()
        {
            string expectedUrl = Util.UrlEncode("/-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", true);
            Assert.AreEqual(expectedUrl, "/-._~0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
        }

        [Test]
        public void UrlEncodeWithUTF8CharactersTest()
        {
            string expectedUrl = Util.UrlEncode("/\u1234", true);
            Assert.AreEqual(expectedUrl, "/%E1%88%B4");
        }

        [Test]
        public void Ssl3OnlyIsObsoleteSecurityProtocol()
        {
            #pragma warning disable CS0618
            var protocolType = SecurityProtocolType.Ssl3;
            #pragma warning restore CS0618

            var result = Util.IsObsoleteSecurityProtocol(protocolType);

            Assert.IsTrue(result);
        }

        [Test]
        public void TlsOnlyIsObsoleteSecurityProtocol()
        {
            var protocolType = SecurityProtocolType.Tls;

            var result = Util.IsObsoleteSecurityProtocol(protocolType);

            Assert.IsTrue(result);
        }

        [Test]
        public void Tls11OnlyIsNotObsoleteSecurityProtocol()
        {
            var protocolType = SecurityProtocolType.Tls11;

            var result = Util.IsObsoleteSecurityProtocol(protocolType);

            Assert.IsFalse(result);
        }

        [Test]
        public void Tls12OnlyIsNotObsoleteSecurityProtocol()
        {
            var protocolType = SecurityProtocolType.Tls12;

            var result = Util.IsObsoleteSecurityProtocol(protocolType);

            Assert.IsFalse(result);
        }

        [Test]
        public void Ssl3AndTls1AreObsoleteSecurityProtocol()
        {
            #pragma warning disable CS0618
            var protocolType = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
            #pragma warning restore CS0618

            var result = Util.IsObsoleteSecurityProtocol(protocolType);

            Assert.IsTrue(result);
        }

        [Test]
        public void Ssl3AndTls1AndTls11AreNotObsoleteSecurityProtocol()
        {
            #pragma warning disable CS0618
            var protocolType = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            #pragma warning restore CS0618

            var result = Util.IsObsoleteSecurityProtocol(protocolType);

            Assert.IsFalse(result);
        }

        [Test]
        public void Tls1AndTls11AndTls12AreNotObsoleteSecurityProtocol()
        {
            var protocolType = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            var result = Util.IsObsoleteSecurityProtocol(protocolType);

            Assert.IsFalse(result);
        }
    }   
}
