﻿using System.Windows.Forms;

namespace CardonerSistemas
{
    static class ControlValueTranslation
    {

        #region Objectos a Controles - TextBox

        static internal string StringToTextBox(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            else
            {
                return value;
            }
        }

        static internal string MoneyToTextBox(decimal? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString(CardonerSistemas.Constants.FormatStringToCurrency);

            }
            else
            {
                return string.Empty;
            }
        }

        static internal string DecimalToTextBox(decimal? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString(CardonerSistemas.Constants.FormatStringToNumber);
            }
            else
            {
                return string.Empty;
            }
        }

        static internal string IntegerToTextBox(int? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString("{0:N}");
            }
            else
            {
                return string.Empty;
            }
        }

        static internal string ShortToTextBox(short? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString("{0:N}");
            }
            else
            {
                return string.Empty;
            }
        }

        static internal string ByteToTextBox(byte? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString("{0:N}");
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region Objectos a Controles - Otros

        static internal decimal IntegerToUpDown(int? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return decimal.Zero;
            }
        }

        static internal decimal ShortToUpDown(short? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return decimal.Zero;
            }
        }

        static internal decimal ByteToUpDown(byte? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return decimal.Zero;
            }
        }

        static internal CheckState BooleanToCheckBox(bool? value)
        {
            if (value.HasValue)
            {
                if (value.Value)
                {
                    return CheckState.Checked;

                }
                else
                {
                    return CheckState.Unchecked;
                }
            }
            else
            {
                return CheckState.Indeterminate;
            }
        }

        #endregion

        #region De Controles a Objectos - TextBox

        static internal string TextBoxToString(string value, bool trimText = true)
        {
            if (trimText)
            {
                value = value.Trim();
            }
            if (value.Length == 0)
            {
                return null;
            }
            else
            {
                return value;
            }
        }

        #endregion

        #region De Controles a Objectos - Otros

        static internal bool? CheckBoxToBoolean(CheckState state)
        {
            switch (state)
            {
                case CheckState.Unchecked:
                    return false;
                case CheckState.Checked:
                    return true;
                case CheckState.Indeterminate:
                    return null;
                default:
                    return null;
            }
        }

        #endregion

    }
}
