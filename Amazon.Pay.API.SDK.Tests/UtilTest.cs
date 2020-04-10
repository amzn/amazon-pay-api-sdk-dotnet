using NUnit.Framework;

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
    }   
}
