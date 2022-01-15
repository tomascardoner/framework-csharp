using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CardonerSistemas
{
    public static class String
    {
        /// <summary>
        /// Gets a sub-string given it's zero-based order position and separator
        /// </summary>
        /// <param name="mainString">String in where to search</param>
        /// <param name="orderPosition">Zero-based order index</param>
        /// <param name="separator">String separator</param>
        /// <returns></returns>
        public static string GetSubString(string mainString, int orderPosition, string separator)
        {
            // Splits the string into an array of substrings delimited by separator
            string[] stringSeparators = new string[] { separator };
            string[] subStrings = mainString.Split(stringSeparators, StringSplitOptions.None);

            if (orderPosition <= subStrings.GetUpperBound(0))
            {
                return subStrings[orderPosition];
            }
            else
            {
                return string.Empty;
            }
        }

        internal static int GetExtends(System.Drawing.Graphics graphicObject, string text, Font font)
        {
            SizeF size = graphicObject.MeasureString(text, font);
            return (int)Math.Ceiling(size.Width);
        }
        
        public static string RemoveDiacritics(this string s)
        {
            string normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Trims the string and then remove double spaces.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimAndReduce(this string value)
        {
            value = value.Trim(); 
            return RemoveDoubleSpaces(value);
        }

        /// <summary>
        /// Removes double spaces.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveDoubleSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        /// <summary>
        /// Removes spaces.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", string.Empty);
        }

        /// <summary>
        /// Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string value)
        {
            return Application.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        /// <summary>
        /// Converts the specified string to title case (also the words that are entirely in uppercase).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTitleCaseAll(this string value)
        {
            return Application.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }
    }
}
