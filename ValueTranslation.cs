using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class ValueTranslation
    {

        #region Objectos a Controles - TextBox

        static internal string ObjectStringToTextBox(string value)
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

        static internal string ObjectMoneyToTextBox(decimal? value)
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

        static internal string ObjectDecimalToTextBox(decimal? value)
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

        static internal string ObjectIntegerToTextBox(int? value)
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

        static internal string ObjectShortToTextBox(short? value)
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

        static internal string ObjectByteToTextBox(byte? value)
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

        static internal decimal FromObjectIntegerToUpDown(int? value)
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

        static internal decimal FromObjectShortToUpDown(short? value)
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

        static internal decimal ObjectByteToUpDown(byte? value)
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

        static internal CheckState ObjectBooleanToCheckBox(bool? value)
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

    }
}
