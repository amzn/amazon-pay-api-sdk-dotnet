using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Amazon.Pay.API.Types;
using FluentAssertions;
using NUnit.Framework;
using Environment = Amazon.Pay.API.Types.Environment;

namespace Amazon.Pay.API.SDK.Tests
{
    [TestFixture]
    public class PrivateKeyExceptionSafetyTests
    {
        private static string pem;
        private static string header;
        private static string footer;
        private static string body;
        private static string pkcs8Pem;

        [OneTimeSetUp]
        public void LoadKeys()
        {
            pem = TestKeys.Pkcs1Pem;
            pkcs8Pem = TestKeys.Pkcs8Pem;

            string[] lines = pem.Split('\n').Select(l => l.TrimEnd('\r')).ToArray();
            header = lines.First();
            footer = lines.Last();
            body = string.Join("\n", lines.Skip(1).Take(lines.Length - 2));
        }

        private static readonly string Dashes = new string('-', 5);

        private static string BeginLine(string type) => Dashes + "BEGIN " + type + Dashes;

        private static string EndLine(string type) => Dashes + "END " + type + Dashes;

        private static string Armor(string type, string content) => BeginLine(type) + "\n" + content + "\n" + EndLine(type);

        private static ApiConfiguration CreateConfig(string privateKey)
        {
            return new ApiConfiguration(Region.UnitedStates, Environment.Sandbox, "foo", privateKey);
        }

        private static void AssertDoesNotContainSecret(Exception exception, string secret)
        {
            var texts = new List<string>();
            for (Exception e = exception; e != null; e = e.InnerException)
            {
                texts.Add(e.Message ?? string.Empty);
                texts.Add(e.ToString());
                if (e is FileNotFoundException fnf)
                {
                    fnf.FileName.Should().BeNull("FileName must not carry the private key input");
                }
            }
            string allText = string.Join("\n", texts);

            const int fragmentLength = 12;
            foreach (string line in secret.Split('\n').Select(l => l.Trim()).Where(l => l.Length >= fragmentLength))
            {
                for (int i = 0; i + fragmentLength <= line.Length; i += fragmentLength / 2)
                {
                    string fragment = line.Substring(i, fragmentLength);
                    allText.Should().NotContain(fragment, "exception output must not contain any part of the private key input");
                }
            }
        }

        [TestCase("\n")]
        [TestCase("\r\n")]
        [TestCase("   ")]
        [TestCase("\t\n  ")]
        [TestCase("\uFEFF")]
        [TestCase("\uFEFF\n")]
        [TestCase("\n\uFEFF")]
        public void AcceptsPemWithLeadingWhitespaceOrBom(string prefix)
        {
            var config = CreateConfig(prefix + pem + "\n");

            config.PrivateKey.Should().Be(pem);
        }

        [Test]
        public void AcceptsPemFileWithSurroundingWhitespace()
        {
            using (var file = new TempKeyFile("\n  " + pem + "\n\n"))
            {
                CreateConfig(file.Path).PrivateKey.Should().Be(pem);
            }
        }

        [Test]
        public void AcceptsPemFileWithWindowsLineEndings()
        {
            string crlfPem = pem.Replace("\n", "\r\n");
            pem.Should().NotContain("\r");

            using (var file = new TempKeyFile(crlfPem))
            {
                var config = CreateConfig(file.Path);

                config.PrivateKey.Should().Be(crlfPem);
                new SignatureHelper(config, new CanonicalBuilder())
                    .GenerateSignature("AMZN-PAY-RSASSA-PSS\nabc").Should().NotBeNullOrEmpty();
            }
        }

        [TestCase("")]
        [TestCase("\n")]
        [TestCase("\uFEFF")]
        public void AcceptsPkcs8PrivateKeyAndCanSign(string prefix)
        {
            var config = CreateConfig(prefix + pkcs8Pem + "\n");

            config.PrivateKey.Should().StartWith(BeginLine("PRIVATE KEY"));
            new SignatureHelper(config, new CanonicalBuilder())
                .GenerateSignature("AMZN-PAY-RSASSA-PSS\nabc").Should().NotBeNullOrEmpty();
        }

        [Test]
        public void AcceptsPkcs8PrivateKeyFromFile()
        {
            using (var file = new TempKeyFile(pkcs8Pem))
            {
                CreateConfig(file.Path).PrivateKey.Should().Be(pkcs8Pem);
            }
        }

        [Test]
        public void TempKeyFileIsDeletedAfterUse()
        {
            string path;
            using (var file = new TempKeyFile(pem))
            {
                path = file.Path;
                File.Exists(path).Should().BeTrue();
            }

            File.Exists(path).Should().BeFalse();
        }

        [Test]
        public void KeyFromValidPemCanStillSign()
        {
            var config = CreateConfig("\uFEFF\n" + pem);
            var helper = new SignatureHelper(config, new CanonicalBuilder());

            helper.GenerateSignature("AMZN-PAY-RSASSA-PSS\nabc").Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Base64BodyWithoutArmorThrowsFileNotFoundWithoutLeakingKey()
        {
            Action invocation = () => CreateConfig(body);

            var exception = invocation.Should().Throw<FileNotFoundException>()
                .WithMessage("Provided private key file cannot be found").Which;
            AssertDoesNotContainSecret(exception, body);
        }

        [Test]
        public void SingleLineBase64BodyThrowsFileNotFoundWithoutLeakingKey()
        {
            string singleLine = body.Replace("\n", string.Empty);
            Action invocation = () => CreateConfig(singleLine);

            var exception = invocation.Should().Throw<FileNotFoundException>().Which;
            AssertDoesNotContainSecret(exception, singleLine);
        }

        [Test]
        public void GarbageBeforeArmorThrowsWithoutLeakingKey()
        {
            string input = "privateKey=" + pem;
            Action invocation = () => CreateConfig(input);

            var exception = invocation.Should().Throw<ArgumentException>().Which;
            AssertDoesNotContainSecret(exception, input);
        }

        [Test]
        public void NonExistingPathThrowsFileNotFoundWithoutLeakingPath()
        {
            string path = Path.Combine(Path.GetTempPath(), "secret-location-" + Guid.NewGuid().ToString("N"), "private.pem");
            Action invocation = () => CreateConfig(path);

            var exception = invocation.Should().Throw<FileNotFoundException>()
                .WithMessage("Provided private key file cannot be found").Which;
            AssertDoesNotContainSecret(exception, path);
        }

        [Test]
        public void FileWithInvalidContentThrowsWithoutLeakingContentOrPath()
        {
            using (var file = new TempKeyFile(body))
            {
                Action invocation = () => CreateConfig(file.Path);

                var exception = invocation.Should().Throw<ArgumentException>()
                    .WithMessage("Provided file does not contain a private key in the expected format*").Which;
                AssertDoesNotContainSecret(exception, body);
                AssertDoesNotContainSecret(exception, file.Path);
            }
        }

        [Test]
        public void FileWithCorruptKeyThrowsWithoutLeakingContentOrPath()
        {
            using (var file = new TempKeyFile(header + "\n" + body.Substring(0, body.Length / 2) + "\n" + footer))
            {
                Action invocation = () => CreateConfig(file.Path);

                var exception = invocation.Should().Throw<ArgumentException>().Which;
                exception.InnerException.Should().BeNull();
                AssertDoesNotContainSecret(exception, body);
                AssertDoesNotContainSecret(exception, file.Path);
            }
        }

        [Test]
        public void UnreadableFileThrowsWithoutLeakingPath()
        {
            using (var file = new TempKeyFile(pem))
            using (new FileStream(file.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                Action invocation = () => CreateConfig(file.Path);

                var exception = invocation.Should().Throw<ArgumentException>()
                    .WithMessage("Provided private key file could not be read*").Which;
                exception.InnerException.Should().BeNull();
                AssertDoesNotContainSecret(exception, file.Path);
                AssertDoesNotContainSecret(exception, body);
            }
        }

        [Test]
        public void NullPrivateKeyThrowsArgumentNullException()
        {
            Action invocation = () => CreateConfig(null);

            invocation.Should().Throw<ArgumentNullException>();
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase("\uFEFF\n")]
        public void EmptyPrivateKeyThrowsArgumentException(string input)
        {
            Action invocation = () => CreateConfig(input);

            invocation.Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetterOnExistingConfigDoesNotLeakKey()
        {
            var config = CreateConfig(pem);
            Action invocation = () => config.PrivateKey = body;

            var exception = invocation.Should().Throw<FileNotFoundException>().Which;
            AssertDoesNotContainSecret(exception, body);
        }

        [Test]
        public void ArmorOnlyPlaceholderIsRejected()
        {
            Action invocation = () => CreateConfig(BeginLine("RSA PRIVATE KEY"));

            invocation.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PublicKeyIsRejected()
        {
            Action invocation = () => CreateConfig(TestKeys.PublicKeyPem);

            invocation.Should().Throw<ArgumentException>().WithMessage("Unsupported private key format*");
        }

        [Test]
        public void EcPrivateKeyIsRejected()
        {
            Action invocation = () => CreateConfig(TestKeys.EcPrivateKeyPem);

            invocation.Should().Throw<ArgumentException>().WithMessage("Unsupported private key format*");
        }

        [Test]
        public void EcPkcs8PrivateKeyIsRejected()
        {
            Action invocation = () => CreateConfig(TestKeys.EcPkcs8Pem);

            invocation.Should().Throw<ArgumentException>().WithMessage("Unsupported private key format*");
        }

        [Test]
        public void OpenSshKeyGivesPemFormatHint()
        {
            Action invocation = () => CreateConfig(Armor("OPENSSH PRIVATE KEY", body));

            var exception = invocation.Should().Throw<ArgumentException>().WithMessage("*ssh-keygen -m PEM*").Which;
            AssertDoesNotContainSecret(exception, body);
        }

        [Test]
        public void ParseErrorMessageContainsNoParserDetails()
        {
            Action invocation = () => CreateConfig(header + "\n" + body.Substring(0, body.Length / 2) + "\n" + footer);

            invocation.Should().Throw<ArgumentException>()
                .Which.Message.Should().StartWith("Unable to parse the private key").And.NotContain("Exception");
        }

        private static IEnumerable<TestCaseData> MalformedPemCases()
        {
            yield return new TestCaseData("truncated body", (Func<string>)(() => header + "\n" + body.Substring(0, body.Length / 2) + "\n" + footer));
            yield return new TestCaseData("missing end marker", (Func<string>)(() => header + "\n" + body));
            yield return new TestCaseData("invalid base64", (Func<string>)(() => header + "\n" + body.Substring(0, 10) + "!" + body.Substring(11) + "\n" + footer));
            yield return new TestCaseData("unknown armor", (Func<string>)(() => Armor("FOO KEY", body)));
            yield return new TestCaseData("mismatched armor", (Func<string>)(() => Armor("PRIVATE KEY", body)));
            yield return new TestCaseData("encrypted armor", (Func<string>)(() => Armor("ENCRYPTED PRIVATE KEY", body)));
            yield return new TestCaseData("openssh armor", (Func<string>)(() => Armor("OPENSSH PRIVATE KEY", body)));
            yield return new TestCaseData("password protected", (Func<string>)(() => header + "\nProc-Type: 4,ENCRYPTED\nDEK-Info: AES-128-CBC,00112233445566778899AABBCCDDEEFF\n\n" + body + "\n" + footer));
            yield return new TestCaseData("public key armor", (Func<string>)(() => Armor("PUBLIC KEY", body)));
        }

        [TestCaseSource(nameof(MalformedPemCases))]
        public void MalformedPemIsRejectedWithoutLeakingKey(string caseName, Func<string> buildInput)
        {
            string input = buildInput();

            Action invocation = () => CreateConfig(input);

            var exception = invocation.Should().Throw<ArgumentException>(caseName).Which;
            exception.InnerException.Should().BeNull("parser exceptions must not be chained");
            AssertDoesNotContainSecret(exception, body);
        }
    }
}
