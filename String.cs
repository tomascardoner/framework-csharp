using System;

namespace CardonerSistemas
{
    static class String
    {
        /// <summary>
        /// Gets a sub-string given it's zero-based order position and separator
        /// </summary>
        /// <param name="mainString">String in where to search</param>
        /// <param name="orderPosition">Zero-based order index</param>
        /// <param name="separator">String separator</param>
        /// <returns></returns>
        static public string GetSubString(string mainString, int orderPosition, string separator)
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
    }
}
