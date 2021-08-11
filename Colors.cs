using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace CardonerSistemas
{
    static class Colors
    {

        #region Hexadecimal validation

        // Hex RGB
        internal const string RegExHexadecimalRgbSingleDigits = "^#(?:[0-9a-fA-F]{3})$";
        internal const string RegExHexadecimalRgbDoubleDigits = "^#(?:[0-9a-fA-F]{6})$";
        internal const string RegExHexadecimalRgbBothDigits = "^#(?:[0-9a-fA-F]{3,6})$";

        // Hex ARGB
        internal const string RegExHexadecimalArgbSingleDigits = "^#(?:[0-9a-fA-F]{4})$";
        internal const string RegExHexadecimalArgbDoubleDigits = "^#(?:[0-9a-fA-F]{8})$";
        internal const string RegExHexadecimalArgbBothDigits = "^#(?:[0-9a-fA-F]{4,8})$";

        // Hex RGB or ARGB
        internal const string RegExHexadecimalRgbOrArgbSingleDigits = "^#(?:[0-9a-fA-F]{3,4})$";
        internal const string RegExHexadecimalRgbOrArgbDoubleDigits = "^#(?:[0-9a-fA-F]{6,8})$";
        internal const string RegExHexadecimalRgbOrArgbBothDigits = "^#(?:[0-9a-fA-F]{3,4,6,8})$";

        static internal bool IsValidHexColor(string value, string evaluateExpression)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(value, evaluateExpression, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Serialization Get

        static internal bool GetFromHexString(string value, ref Color color, string evaluateExpression = RegExHexadecimalRgbOrArgbBothDigits, string valueDefault = null)
        {
            if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(valueDefault))
            {
                value = valueDefault;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                value = value.Trim();
                if (value.StartsWith("#"))
                {
                    if (IsValidHexColor(value, evaluateExpression))
                    {
                        try
                        {
                            color = ColorTranslator.FromHtml(value);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            Error.ProcessError(ex, "Error al convertir el valor en un color.");
                        }
                    }
                }
            }
            return false;
        }

        static internal bool GetFromNameString(string value, ref Color color, string valueDefault = null)
        {
            if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(valueDefault))
            {
                value = valueDefault;
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.Trim();
                Color namedColor;
                namedColor = Color.FromName(value);
                if (namedColor.A + namedColor.R + namedColor.G + namedColor.B > 0)
                {
                    color = namedColor;
                    return true;
                }
            }
            return false;
        }

        static internal bool GetFromHexOrNameString(string value, ref Color color, string evaluateExpression = RegExHexadecimalRgbOrArgbBothDigits, string valueDefault = null)
        {
            if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(valueDefault))
            {
                value = valueDefault;
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.Trim();
                if (value.StartsWith("#"))
                {
                    return GetFromHexString(value, ref color, evaluateExpression, valueDefault);
                }
                else
                {
                    return GetFromNameString(value, ref color, valueDefault);
                }
            }
            return false;
        }

        #endregion

        #region Serialization Set

        static internal string SetToHexRgbString(Color color)
        {
            if (color.IsEmpty)
            {
                return string.Empty;
            }
            else
            {
                return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
            }
        }

        static internal string SetToHexArgbString(Color color)
        {
            if (color.IsEmpty)
            {
                return string.Empty;
            }
            else
            {
                return "#" + color.A.ToString("X2") + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
            }
        }

        static internal string SetToNamedOrHexRgbString(Color color)
        {
            if (color.IsEmpty)
            {
                return string.Empty;
            }
            else
            {
                if (color.IsKnownColor)
                {
                    return color.Name;
                }
                else
                {
                    return SetToHexRgbString(color);
                }
            }
        }

        static internal string SetToNamedOrHexArgbString(Color color)
        {
            if (color.IsEmpty)
            {
                return string.Empty;
            }
            else
            {
                if (color.IsKnownColor)
                {
                    return color.Name;
                }
                else
                {
                    return SetToHexArgbString(color);
                }
            }
        }

        #endregion

        #region Assignation

        static internal Color SetColor(Color? newColor, Color defaultColor)
        {
            if (newColor.HasValue)
            {
                return newColor.Value;
            }
            else
            {
                return defaultColor;
            }
        }

        #endregion

    }
}
