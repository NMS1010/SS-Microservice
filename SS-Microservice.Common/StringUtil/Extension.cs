using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SS_Microservice.Common.StringUtil
{
    public static class Extension
    {
        public static string RemoveAccents(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            char[] chars = text
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
                != UnicodeCategory.NonSpacingMark).ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public static string Slugify(this string phrase)
        {
            string output = phrase.RemoveAccents().ToLower();

            output = Regex.Replace(output, @"[^A-Za-z0-9\s-]", "");

            output = Regex.Replace(output, @"\s+", " ").Trim();

            output = Regex.Replace(output, @"\s", "-");

            return output;
        }

        public static string GenerateUniqueCode(this string str)
        {
            var guid = Guid.NewGuid().ToString();

            return guid.Replace("-", string.Empty).ToUpper();
        }
    }
}