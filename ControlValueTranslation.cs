using System.Windows.Forms;
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

        #region Objectos a controles - TextBox

        // Valores string
        internal static void ValueToTextBox(System.Windows.Forms.TextBox textBox, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                textBox.Text = string.Empty;
            }
            else
            {
                textBox.Text = value;
            }
        }

        internal static void ValueToTextBox(System.Windows.Forms.MaskedTextBox maskedTextBox, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                maskedTextBox.Text = string.Empty;
            }
            else
            {
                maskedTextBox.Text = value;
            }
        }

        // Valores numéricos - función
        private static string ValueToTextBox(long? value, bool formatNumber = true, string textToShowOnZeroOrNull = "")
        {
            if (value.HasValue)
            {
                if (value.Value == 0 && textToShowOnZeroOrNull != "")
                {
                    return textToShowOnZeroOrNull;
                }
                else
                {
                    if (formatNumber)
                    {
                        return value.Value.ToString(CardonerSistemas.Constants.FormatStringToNumberInteger);
                    }
                    else
                    {
                        return value.Value.ToString();
                    }
                }
            }
            else
            {
                return textToShowOnZeroOrNull;
            }
        }

        // Valores numéricos - con formato
        internal static void ValueToTextBox(System.Windows.Forms.TextBox textBox, long? value, bool formatNumber = true, string textToShowOnZeroOrNull = "")
        {
            textBox.Text = ValueToTextBox(value, formatNumber, textToShowOnZeroOrNull);
        }

        internal static void ValueToTextBox(System.Windows.Forms.MaskedTextBox maskedTextBox, long? value, bool formatNumber = true, string textToShowOnZeroOrNull = "")
        {
            maskedTextBox.Text = ValueToTextBox(value, formatNumber, textToShowOnZeroOrNull);
        }

        // Valores numéricos - con zeros a la izquierda
        internal static void ValueToTextBox(System.Windows.Forms.TextBox textBox, long? value, byte totalDigits)
        {
            if (!value.HasValue)
            {
                value = 0;
            }
            textBox.Text = value.Value.ToString("D" + totalDigits.ToString());
        }

        internal static void ValueToTextBox(System.Windows.Forms.MaskedTextBox maskedTextBox, long? value, byte totalDigits)
        {
            if (!value.HasValue)
            {
                value = 0;
            }
            maskedTextBox.Text = value.Value.ToString("D" + totalDigits.ToString());
        }

        #endregion

        #region Objectos a controles - ComboBox

        static internal void ValueToComboBox(System.Windows.Forms.ComboBox comboBox, string value, ComboBoxExtension.SelectedItemOptions selectedItemOptions = ComboBoxExtension.SelectedItemOptions.ValueOrFirstIfUnique, string valueForNull = "")
        {
            ComboBoxExtension.SetSelectedValue(comboBox, selectedItemOptions, value, valueForNull);
        }

        static internal void ValueToComboBox(System.Windows.Forms.ComboBox comboBox, byte? value, ComboBoxExtension.SelectedItemOptions selectedItemOptions = ComboBoxExtension.SelectedItemOptions.ValueOrFirstIfUnique, byte valueForNull = CardonerSistemas.Constants.ByteFieldValueNotSpecified)
        {
            ComboBoxExtension.SetSelectedValue(comboBox, selectedItemOptions, value, valueForNull);
        }

        static internal void ValueToComboBox(System.Windows.Forms.ComboBox comboBox, short? value, ComboBoxExtension.SelectedItemOptions selectedItemOptions = ComboBoxExtension.SelectedItemOptions.ValueOrFirstIfUnique, short valueForNull = CardonerSistemas.Constants.ShortFieldValueNotSpecified)
        {
            ComboBoxExtension.SetSelectedValue(comboBox, selectedItemOptions, value, valueForNull);
        }

        static internal void ValueToComboBox(System.Windows.Forms.ComboBox comboBox, int? value, ComboBoxExtension.SelectedItemOptions selectedItemOptions = ComboBoxExtension.SelectedItemOptions.ValueOrFirstIfUnique, int valueForNull = CardonerSistemas.Constants.IntegerFieldValueNotSpecified)
        {
            ComboBoxExtension.SetSelectedValue(comboBox, selectedItemOptions, value, valueForNull);
        }

        #endregion

        #region Objectos a controles - Otros

        internal static void IntegerToUpDown(System.Windows.Forms.NumericUpDown numericUpDown, int? value)
        {
            if (value.HasValue)
            {
                numericUpDown.Value = value.Value;
            }
            else
            {
                numericUpDown.Value = decimal.Zero;
            }
        }

        internal static void ShortToUpDown(System.Windows.Forms.NumericUpDown numericUpDown, short? value)
        {
            if (value.HasValue)
            {
                numericUpDown.Value = value.Value;
            }
            else
            {
                numericUpDown.Value = decimal.Zero;
            }
        }

        internal static void ByteToUpDown(System.Windows.Forms.NumericUpDown numericUpDown, byte? value)
        {
            if (value.HasValue)
            {
                numericUpDown.Value = value.Value;
            }
            else
            {
                numericUpDown.Value = decimal.Zero;
            }
        }

        internal static void ValueToCheckBox(System.Windows.Forms.CheckBox checkBox, bool? value)
        {
            if (value.HasValue)
            {
                if (value.Value)
                {
                    checkBox.CheckState = CheckState.Checked;

                }
                else
                {
                    checkBox.CheckState = CheckState.Unchecked;
                }
            }
            else
            {
                checkBox.CheckState = CheckState.Indeterminate;
            }
        }

        internal static void ValueToDateTimePicker(System.Windows.Forms.DateTimePicker dateTimePicker, System.DateTime? value)
        {
            if (value.HasValue)
            {
                dateTimePicker.Value = value.Value;
            }
            else
            {
                dateTimePicker.Value = System.DateTime.Today;
            }
            dateTimePicker.Checked = value.HasValue;
        }

        internal static void ValueToTextBoxAsShortDateTime(System.Windows.Forms.TextBox textBox, System.DateTime? value)
        {
            if (value.HasValue)
            {
                textBox.Text = value.Value.ToShortDateString() + " " + value.Value.ToShortTimeString();
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }

        internal static void ValueToTextBoxAsShortDate(System.Windows.Forms.TextBox textBox, System.DateTime? value)
        {
            if (value.HasValue)
            {
                textBox.Text = value.Value.ToShortDateString();
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }

        internal static void ValueToTextBoxAsShortTime(System.Windows.Forms.TextBox textBox, System.DateTime? value)
        {
            if (value.HasValue)
            {
                textBox.Text = value.Value.ToShortTimeString();
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }

        internal static void ValueToPictureBox(System.Windows.Forms.PictureBox pictureBox, byte[] image)
        {
            if (image == null)
            {
                pictureBox.Image = null;
            }
            else
            {
                byte[] aFoto = image;
                using (System.IO.MemoryStream memstr = new System.IO.MemoryStream(aFoto, 0, aFoto.Length))
                {
                    memstr.Write(aFoto, 0, aFoto.Length);
                    pictureBox.Image = System.Drawing.Image.FromStream(memstr, true);
                }
            }
        }

        #endregion

        #region Controles a objectos - TextBox

        private static string TextBoxToString(string value, bool trimText = true, ChangeCase changeCase = ChangeCase.None)
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

        internal static string TextBoxToString(System.Windows.Forms.TextBox textBox, bool trimText = true, ChangeCase changeCase = ChangeCase.None)
        { 
            return TextBoxToString(textBox.Text, trimText, changeCase);
        }

        internal static string TextBoxToString(System.Windows.Forms.MaskedTextBox maskedTextBox, bool trimText = true, ChangeCase changeCase = ChangeCase.None)
        {
            return TextBoxToString(maskedTextBox.Text, trimText, changeCase);
        }

        private static long? TextBoxToLong(string value)
        {
            value = value.Trim();
            if (value.Length == 0)
            {
                return null;
            }
            else
            {
                if (long.TryParse(value, out long returnValue))
                {
                    return returnValue;
                }
                else
                {
                    return null;
                }
            }
        }

        internal static long? TextBoxToLong(System.Windows.Forms.TextBox textBox)
        {
            return TextBoxToLong(textBox.Text);
        }

        internal static long? TextBoxToLong(System.Windows.Forms.MaskedTextBox maskedTextBox)
        {
            return TextBoxToLong(maskedTextBox.Text);
        }

        private static int? TextBoxToInt(string value)
        {
            value = value.Trim();
            if (value.Length == 0)
            {
                return null;
            }
            else
            {
                if (int.TryParse(value, out int returnValue))
                {
                    return returnValue;
                }
                else
                {
                    return null;
                }
            }
        }

        internal static int? TextBoxToInt(System.Windows.Forms.TextBox textBox)
        {
            return TextBoxToInt(textBox.Text);
        }

        internal static int? TextBoxToInt(System.Windows.Forms.MaskedTextBox maskedTextBox)
        {
            return TextBoxToInt(maskedTextBox.Text);
        }

        private static short? TextBoxToShort(string value)
        {
            value = value.Trim();
            if (value.Length == 0)
            {
                return null;
            }
            else
            {
                if (short.TryParse(value, out short returnValue))
                {
                    return returnValue;
                }
                else
                {
                    return null;
                }
            }
        }

        internal static short? TextBoxToShort(System.Windows.Forms.TextBox textBox)
        {
            return TextBoxToShort(textBox.Text);
        }

        internal static short? TextBoxToShort(System.Windows.Forms.MaskedTextBox maskedTextBox)
        {
            return TextBoxToShort(maskedTextBox.Text);
        }

        private static byte? TextBoxToByte(string value)
        {
            value = value.Trim();
            if (value.Length == 0)
            {
                return null;
            }
            else
            {
                if (byte.TryParse(value, out byte returnValue))
                {
                    return returnValue;
                }
                else
                {
                    return null;
                }
            }
        }

        internal static byte? TextBoxToByte(System.Windows.Forms.TextBox textBox)
        {
            return TextBoxToByte(textBox.Text);
        }

        internal static byte? MaskedTextBoxToByte(System.Windows.Forms.MaskedTextBox maskedTextBox)
        {
            return TextBoxToByte(maskedTextBox.Text);
        }

        #endregion

        #region Controles a objetos - ComboBox

        internal static string ComboBoxToString(System.Windows.Forms.ComboBox comboBox, string valueForNull = "")
        {
            if (comboBox.SelectedValue == null)
            {
                return null;
            }
            else if (Convert.ToString(comboBox.SelectedValue) == valueForNull)
            {
                return null;
            }
            else
            {
                return Convert.ToString(comboBox.SelectedValue);
            }
        }

        internal static byte? ComboBoxToByte(System.Windows.Forms.ComboBox comboBox, byte valueForNull = Constants.ByteFieldValueNotSpecified)
        {
            if (comboBox.SelectedValue == null)
            {
                return null;
            }
            else if (Convert.ToByte(comboBox.SelectedValue) == valueForNull)
            {
                return null;
            }
            else
            {
                return Convert.ToByte(comboBox.SelectedValue);
            }
        }

        internal static short? ComboBoxToShort(System.Windows.Forms.ComboBox comboBox, short valueForNull = CardonerSistemas.Constants.ShortFieldValueNotSpecified)
        {
            if (comboBox.SelectedValue == null)
            {
                return null;
            }
            else if (Convert.ToInt16(comboBox.SelectedValue) == valueForNull)
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(comboBox.SelectedValue);
            }
        }

        internal static int? ComboBoxToInt(System.Windows.Forms.ComboBox comboBox, int valueForNull = CardonerSistemas.Constants.IntegerFieldValueNotSpecified)
        {
            if (comboBox.SelectedValue == null)
            {
                return null;
            }
            else if (Convert.ToInt32(comboBox.SelectedValue) == valueForNull)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(comboBox.SelectedValue);
            }
        }

        internal static bool? CheckBoxToBoolean(System.Windows.Forms.CheckBox checkBox)
        {
            switch (checkBox.CheckState)
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

        #region Controles a objectos - Otros

        internal static System.DateTime? DateTimePickerToDate(DateTimePicker value)
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

        internal static System.DateTime? DateTimePickersToDateTime(DateTimePicker dateValue, DateTimePicker timeValue)
        {
            if (dateValue.Checked && timeValue.Checked)
            {
                return new DateTime(dateValue.Value.Year, dateValue.Value.Month, dateValue.Value.Day, timeValue.Value.Hour, timeValue.Value.Minute, timeValue.Value.Second);
            }
            else if (dateValue.Checked)
            {
                return new DateTime(dateValue.Value.Year, dateValue.Value.Month, dateValue.Value.Day);
            }
            else if (timeValue.Checked)
            {
                return new DateTime().AddHours(timeValue.Value.Hour).AddMinutes(timeValue.Value.Minute).AddSeconds(timeValue.Value.Second);
            }
            else
            {
                return null;
            }
        }

        internal static byte[] PictureBoxToImage(System.Drawing.Image image)
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