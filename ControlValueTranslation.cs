﻿using System.Windows.Forms;
using System.Globalization;
using System;

namespace CardonerSistemas
{
    static class ControlValueTranslation
    {

        #region Declarations

        internal enum ChangeCase
        {
            None,
            Lower,
            Upper,
            TitleCase
        }

        #endregion

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
                return value.Value.ToString("N0");
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
                return value.Value.ToString("N0");
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
                return value.Value.ToString("N0");
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region De Objectos a Controles - TextBox (SyncFusion)

        static internal void ByteToIntegerTextBox(byte? value, Syncfusion.Windows.Forms.Tools.IntegerTextBox control)
        {
            if (value.HasValue)
            {
                control.IntegerValue = value.Value;
            }
            else
            {
                if (control.AllowNull)
                {
                    control.BindableValue = null;
                }
                else
                {
                    control.IntegerValue = control.MinValue;
                }
            }
        }

        static internal void ShortToIntegerTextBox(short? value, Syncfusion.Windows.Forms.Tools.IntegerTextBox control)
        {
            if (value.HasValue)
            {
                control.IntegerValue = value.Value;
            }
            else
            {
                if (control.AllowNull)
                {
                    control.BindableValue = null;
                }
                else
                {
                    control.IntegerValue = control.MinValue;
                }
            }
        }

        static internal void IntToIntegerTextBox(int? value, Syncfusion.Windows.Forms.Tools.IntegerTextBox control)
        {
            if (value.HasValue)
            {
                control.IntegerValue = value.Value;
            }
            else
            {
                if (control.AllowNull)
                {
                    control.BindableValue = null;
                }
                else
                {
                    control.IntegerValue = control.MinValue;
                }
            }
        }

        static internal void DecimalToCurrencyTextBox(Decimal? value, Syncfusion.Windows.Forms.Tools.CurrencyTextBox control)
        {
            if (value.HasValue)
            {
                control.DecimalValue = value.Value;
            }
            else
            {
                if (control.AllowNull)
                {
                    control.BindableValue = null;
                }
                else
                {
                    control.DecimalValue = control.MinValue;
                }
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

        static internal DateTime DateToDateTimePicker(DateTime? value, DateTimePicker controlToCheck = null)
        {
            if (controlToCheck != null)
            {
                controlToCheck.Checked = value.HasValue;
            }

            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return System.DateTime.Today;
            }
        }

        static internal string DatetimeToDatetimeShortTextbox(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToShortDateString() + " " + value.Value.ToShortTimeString();
            }
            else
            {
                return string.Empty;
            }
        }

        static internal string DatetimeToDateShortTextbox(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }

        static internal string DatetimeToTimeShortTextbox(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToShortTimeString();
            }
            else
            {
                return string.Empty;
            }
        }

        static internal System.Drawing.Image ImageToPictureBox(byte[] image)
        {
            if (image == null)
            {
                return null;
            }
            else
            {
                byte[] aFoto = image;
                System.IO.MemoryStream memstr = new System.IO.MemoryStream(aFoto, 0, aFoto.Length);
                memstr.Write(aFoto, 0, aFoto.Length);
                return System.Drawing.Image.FromStream(memstr, true);
            }
        }

        #endregion

        #region De Controles a Objectos - TextBox

        static internal string TextBoxToString(string value, bool trimText = true, ChangeCase changeCase = ChangeCase.None)
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
                switch (changeCase)
                {
                    case ChangeCase.None:
                        return value;
                    case ChangeCase.Lower:
                        return value.ToLower();
                    case ChangeCase.Upper:
                        return value.ToUpper();
                    case ChangeCase.TitleCase:
                        TextInfo textInfo = new CultureInfo(Application.CurrentCulture.Name, false).TextInfo;
                        return textInfo.ToTitleCase(value);
                    default:
                        return value;
                }
            }
        }

        static internal short? TextBoxToShort(string value)
        {
            value = value.Trim();

            if (value.Length == 0)
            {
                return null;
            }
            else
            {
                short returnValue;

                if (short.TryParse(value, out returnValue))
                {
                    return returnValue;
                }
                else
                {
                    return null;
                }
            }
        }

        static internal decimal? TextboxToDecimal(string value)
        {
            value = value.Trim();

            if (value.Length == 0)
            {
                return null;
            }
            else
            {
                decimal returnValue;

                if (decimal.TryParse(value, out returnValue))
                {
                    return returnValue;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region De Controles a Objectos - TextBox (SyncFusion)

        static internal byte? IntegerTextBoxToByte(Syncfusion.Windows.Forms.Tools.IntegerTextBox control)
        {
            if (control.AllowNull && control.IsNull)
            {
                return null;
            }
            else
            {
                return Convert.ToByte(control.IntegerValue);
            }
        }

        static internal short? IntegerTextBoxToShort(Syncfusion.Windows.Forms.Tools.IntegerTextBox control)
        {
            if (control.AllowNull && control.IsNull)
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(control.IntegerValue);
            }
        }

        static internal int? IntegerTextBoxToInt(Syncfusion.Windows.Forms.Tools.IntegerTextBox control)
        {
            if (control.AllowNull && control.IsNull)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(control.IntegerValue);
            }
        }

        //static internal double? FromControlDoubleTextBoxToObjectDouble(object bindableValue)
        //{
        //    if (bindableValue is null | !  bindableValue)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return Convert.ToDouble(bindableValue);
        //    }
        //}

        //static internal decimal? FromControlDoubleTextBoxToObjectDecimal(object bindableValue)
        //{
        //    if (bindableValue == null | !isnumeric(bindableValue))
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return Convert.ToDecimal(bindableValue);
        //    }
        //}

        static internal decimal? CurrencyTextBoxToDecimal(Syncfusion.Windows.Forms.Tools.CurrencyTextBox control)
        {
            if (control.AllowNull && control.IsNull)
            {
                return null;
            }
            else
            {
                return control.DecimalValue;
            }
        }

        #endregion

        #region De Controles a Objectos - Otros

        static internal byte? ComboBoxToByte(object selectedValue, byte valueForNull = CardonerSistemas.Constants.ByteFieldValueNotSpecified)
        {
            if (selectedValue == null)
            {
                return null;
            }
            else if (Convert.ToByte(selectedValue) == valueForNull)
            {
                return null;
            }
            else
            {
                return Convert.ToByte(selectedValue);
            }
        }

        static internal short? ComboBoxToShort(object selectedValue, short valueForNull = CardonerSistemas.Constants.ShortFieldValueNotSpecified)
        {
            if (selectedValue == null)
            {
                return null;
            }
            else if (Convert.ToInt16(selectedValue) == valueForNull)
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(selectedValue);
            }
        }

        static internal int? ComboBoxToInteger(object selectedValue, int valueForNull = CardonerSistemas.Constants.IntegerFieldValueNotSpecified)
        {
            if (selectedValue == null)
            {
                return null;
            }
            else if (Convert.ToInt32(selectedValue) == valueForNull)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(selectedValue);
            }
        }

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

        static internal DateTime? DateTimePickerToDate(DateTimePicker value)
        {
            if (value.Checked)
            {
                return value.Value;
            }
            else
            {
                return null;
            }
        }

        static internal byte[] PictureBoxToImage(System.Drawing.Image image)
        {
            if (image == null)
            {
                return null;
            }
            else
            {
                System.IO.MemoryStream memstr = new System.IO.MemoryStream();
                image.Save(memstr, image.RawFormat);
                byte[] aFoto = memstr.GetBuffer();
                return aFoto;
            }
        }

        #endregion

    }
}
