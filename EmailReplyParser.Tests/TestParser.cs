using System.IO;
using System.Reflection;

using Xunit;

namespace EmailReplyParser.Tests
{
    public class TestParser
    {
        private string LoadFile(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        [Theory]
        [InlineData("correct_sig.txt")]
        [InlineData("email_1_1.txt")]
        [InlineData("email_1_2.txt")]
        [InlineData("email_1_3.txt")]
        [InlineData("email_1_4.txt")]
        [InlineData("email_1_5.txt")]
        [InlineData("email_1_6.txt")]
        [InlineData("email_1_7.txt")]
        [InlineData("email_1_8.txt")]
        [InlineData("email_2_1.txt")]
        [InlineData("email_2_2.txt")]
        [InlineData("email_BlackBerry.txt")]
        [InlineData("email_bullets.txt")]
        [InlineData("email_iPhone.txt")]
        [InlineData("email_multi_word_sent_from_my_mobile_device.txt")]
        [InlineData("email_one_is_not_on.txt")]
        [InlineData("email_sent_from_my_not_signature.txt")]
        [InlineData("email_sig_delimiter_in_middle_of_line.txt")]
        [InlineData("greedy_on.txt")]
        [InlineData("pathological.txt")]
        public void VerifyParsedReply(string fileName)
        {
            var email = LoadFile(string.Format("EmailReplyParser.Tests.TestEmails.{0}", fileName));
            var expectedReply = LoadFile(string.Format("EmailReplyParser.Tests.TestEmailResults.{0}", fileName)).Replace("\r\n", "\n");

            var parser = new Lib.Parser();
            var reply = parser.ParseReply(email);

            Assert.Equal(expectedReply, reply);
        }
    }
}