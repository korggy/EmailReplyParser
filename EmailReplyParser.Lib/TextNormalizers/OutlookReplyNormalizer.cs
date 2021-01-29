using System.Text.RegularExpressions;

namespace EmailReplyParser.Lib.TextNormalizers
{
	/// <summary>
	/// Reply underscores normaliser.
	/// </summary>
	public class OutlookReplyNormalizer : ITextNormalizer
	{
		public string Normalize(string text)
		{
			// Outlook's reply starts with From: and may be directly underneath the text.
			// In order to ensure that these fragments are split correctly,
			// make sure that all lines of underscores are preceded by
			// at least two newline characters.

			var fromSignatureRegex = new Regex(@"From:\s.+\n", RegexOptions.Multiline);
			return fromSignatureRegex.Replace(text, (match) => '\n' + match.Value, 1);
		}
	}
}
