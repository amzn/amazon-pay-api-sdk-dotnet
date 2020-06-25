using NUnit.Framework;
using Amazon.Pay.API.Types;
using Amazon.Pay.API.WebStore.Types;
using Newtonsoft.Json;
using System.Threading;
using System.Globalization;

namespace Amazon.Pay.API.Tests
{
    [TestFixture]
    public class DecimalJsonConverterTests
    {
        // note: technically we should test DecimalJsonConverter directly but as Price is the central element using it, this is sufficient as well

        [Test]
        public void ConvertNumberWithoutFraction()
        {
            // arrange
            var price = new Price(99, Currency.EUR);

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99,\"currencyCode\":\"EUR\"}", jsonString);
        }

        [Test]
        public void ConvertNumberZeroFraction()
        {
            // arrange
            var price = new Price(99.0m, Currency.EUR);

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99,\"currencyCode\":\"EUR\"}", jsonString);
        }

        [Test]
        public void ConvertNumberDoubleZeroFraction()
        {
            // arrange
            var price = new Price(99.00m, Currency.EUR);

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99,\"currencyCode\":\"EUR\"}", jsonString);
        }

        [Test]
        public void ConverNumberWithFraction()
        {
            // arrange
            var price = new Price(99.99m, Currency.EUR);

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99.99,\"currencyCode\":\"EUR\"}", jsonString);
        }

        [Test]
        public void ConverNumberWithFractionWithUsLocale()
        {
            // arrange
            var price = new Price(99.99m, Currency.EUR);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99.99,\"currencyCode\":\"EUR\"}", jsonString);
        }

        [Test]
        public void ConverNumberWithFractionWithBritishLocale()
        {
            // arrange
            var price = new Price(99.99m, Currency.EUR);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99.99,\"currencyCode\":\"EUR\"}", jsonString);
        }

        [Test]
        public void ConverNumberWithFractionWithGermanLocale()
        {
            // arrange
            var price = new Price(99.99m, Currency.EUR);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99.99,\"currencyCode\":\"EUR\"}", jsonString);
        }

        [Test]
        public void ConverNumberWithFractionWithFrenchLocale()
        {
            // arrange
            var price = new Price(99.99m, Currency.EUR);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99.99,\"currencyCode\":\"EUR\"}", jsonString);
        }

        [Test]
        public void ConverNumberWithFractionWithItalianLocale()
        {
            // arrange
            var price = new Price(99.99m, Currency.EUR);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("it-IT");

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99.99,\"currencyCode\":\"EUR\"}", jsonString);
        }

        [Test]
        public void ConverNumberWithFractionWithSpanishLocale()
        {
            // arrange
            var price = new Price(99.99m, Currency.EUR);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99.99,\"currencyCode\":\"EUR\"}", jsonString);
        }

        [Test]
        public void ConverNumberWithFractionWithJaJpLocale()
        {
            // arrange
            var price = new Price(99.99m, Currency.EUR);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");

            // act
            var jsonString = JsonConvert.SerializeObject(price);

            // assert
            Assert.AreEqual("{\"amount\":99.99,\"currencyCode\":\"EUR\"}", jsonString);
        }
    }
}
