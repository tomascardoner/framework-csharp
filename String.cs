using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CardonerSistemas
{
    internal static class String
    {

        #region Gets or extract substrings

        /// <summary>
        /// Gets a sub-string given it's zero-based order position and separator
        /// </summary>
        /// <param name="value">String in where to search</param>
        /// <param name="orderPosition">Zero-based order index</param>
        /// <param name="separator">Separator</param>
        /// <returns>A string value representing the element at the specified position. If not element corresponds to the position, return an empty string.</returns>
        public static string GetSubString(this string value, int orderPosition, string separator)
        {
            // Splits the string into an array of substrings delimited by separator
            string[] stringSeparators = new string[] { separator };
            string[] subStrings = value.Split(stringSeparators, StringSplitOptions.None);

            if (orderPosition <= subStrings.GetUpperBound(0))
            {
                return subStrings[orderPosition];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the last sub-string given it's separator
        /// </summary>
        /// <param name="value">String in where to search</param>
        /// <param name="separator">Separator</param>
        /// <returns></returns>
        public static string GetLastSubString(this string value, string separator)
        {
            // Splits the string into an array of substrings delimited by separator
            string[] stringSeparators = new string[] { separator };
            string[] subStrings = value.Split(stringSeparators, StringSplitOptions.None);

            if (subStrings.GetUpperBound(0) > -1)
            {
                return subStrings[subStrings.GetUpperBound(0)];
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion Gets or extract substrings

        #region Sizes

        /// <summary>
        /// Measures the width of the specified string when drawn with the specified System.Drawing.Font.
        /// </summary>
        /// <param name="text">String to measure.</param>
        /// <param name="graphicObject"></param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <returns>This method returns the width,
        /// in the units specified by the System.Drawing.Graphics.PageUnit property, of the
        /// string specified by the text parameter as drawn with the font parameter.</returns>
        public static float GetExtendsF(this string text, System.Drawing.Graphics graphicObject, Font font)
        {
            return graphicObject.MeasureString(text, font).Width;
        }

        /// <summary>
        /// Measures the width of the specified string when drawn with the specified System.Drawing.Font.
        /// </summary>
        /// <param name="text">String to measure.</param>
        /// <param name="graphicObject"></param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <returns>This method returns the width rounded to int,
        /// in the units specified by the System.Drawing.Graphics.PageUnit property, of the
        /// string specified by the text parameter as drawn with the font parameter.</returns>
        public static int GetExtends(this string text, System.Drawing.Graphics graphicObject, Font font)
        {
            return (int)Math.Ceiling(GetExtendsF(text, graphicObject, font));
        }

        /// <summary>
        /// Measures the width of the specified strings when drawn with the specified System.Drawing.Font.
        /// </summary>
        /// <param name="texts">Strings to measure.</param>
        /// <param name="graphicObject"></param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <returns>This method returns the maximum width,
        /// in the units specified by the System.Drawing.Graphics.PageUnit property, of the
        /// strings specified by the texts parameter as drawn with the font parameter.</returns>
        public static float GetMaxExtendsF(string[] texts, System.Drawing.Graphics graphicObject, Font font)
        {
            float textExtend;
            float maxTextExtend = 0;

            foreach (string text in texts)
            {
                textExtend = GetExtendsF(text, graphicObject, font);
                if (textExtend > maxTextExtend)
                {
                    maxTextExtend = textExtend;
                }
            }
            return maxTextExtend;
        }

        /// <summary>
        /// Measures the width of the specified strings when drawn with the specified System.Drawing.Font.
        /// </summary>
        /// <param name="texts">Strings to measure.</param>
        /// <param name="graphicObject"></param>
        /// <param name="font">System.Drawing.Font that defines the text format of the string.</param>
        /// <returns>This method returns the maximum width rounded to int,
        /// in the units specified by the System.Drawing.Graphics.PageUnit property, of the
        /// strings specified by the texts parameter as drawn with the font parameter.</returns>
        public static int GetMaxExtends(string[] texts, System.Drawing.Graphics graphicObject, Font font)
        {
            int textExtend;
            int maxTextExtend = 0;

            foreach (string text in texts)
            {
                textExtend = GetExtends(text, graphicObject, font);
                if (textExtend > maxTextExtend)
                {
                    maxTextExtend = textExtend;
                }
            }
            return maxTextExtend;
        }

        #endregion Sizes

        #region Clean and format

        /// <summary>
        /// Replace diacritics characters with standard ones.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>This method returns a string with the diacritics characters replaced by standard ones.</returns>
        public static string ReplaceDiacritics(this string value)
        {
            string normalizedString = value.Normalize(NormalizationForm.FormD);
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
        /// Removes spaces.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", string.Empty);
        }

        /// <summary>
        /// Removes double spaces.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>This method returns a cleaned string for double spaces.</returns>
        public static string RemoveDoubleSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        /// <summary>
        /// Trims the string and then remove double spaces.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>This method returns a cleaned string for spaces at start and end and also double spaces inside.</returns>
        public static string TrimAndReduce(this string value)
        {
            value = value.Trim();
            return RemoveDoubleSpaces(value);
        }

        /// <summary>
        /// Replace end of a string with a new string.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceEnd(this string value, string oldValue, string newValue)
        {
            if (value.EndsWith(oldValue))
            {
                return value.Substring(0, value.Length - oldValue.Length) + newValue;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Removes non digits characters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveNonDigits(this string value)
        {
            return Regex.Replace(value, @"[^\d]", string.Empty);
        }

        /// <summary>
        /// Converts the first char of the specified string to upper case.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>This method returns the source string with the first character in uppercase.</returns>
        public static string FirstCharToUpperCase(this string value)
        {
            switch (value.Length)
            { 
                case 0:
                    return value;
                case 1:
                    return value.ToUpper();
                default:
                    return value[0].ToString().ToUpper() + value.Substring(1);
            }
        }

        /// <summary>
        /// Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms).
        /// </summary>
        /// <param name="value"></param>
        /// <returns>This method returns the source string with the first character of each word in uppercase.</returns>
        public static string ToTitleCase(this string value)
        {
            return System.Windows.Forms.Application.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        /// <summary>
        /// Converts the specified string to title case (also the words that are entirely in uppercase).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTitleCaseAll(this string value)
        {
            return value.ToLower().ToTitleCase();
        }

        #endregion Clean and format

        #region Evaluation

        /// <summary>
        /// Checks if all characters are numeric digits.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>This method returns true if all characters in string are digits, false if not.</returns>
        public static bool IsDigitsOnly(this string value)
        {
            foreach (char c in value)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        /// <summary>
        /// If the string is not empty, appends the valueToAppend, and then the valueToAppendAlways
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueToAppend"></param>
        /// <param name="valueToAppendAlways"></param>
        /// <returns></returns>
        public static string AppendIfNotEmpty(this string value, string valueToAppend, string valueToAppendAlways)
        { 
            return value + (string.IsNullOrEmpty(value) ? string.Empty : valueToAppend) + valueToAppendAlways;
        }

        public static string AppendIfNotEmpty(this string value, string valueToAppend)
        {
            return AppendIfNotEmpty(value, valueToAppend, string.Empty);
        }

        #endregion Evaluation

    }
}
