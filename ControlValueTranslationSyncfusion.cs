using System;

namespace CardonerSistemas
{
    static class ControlValueTranslationSyncfusion
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

    }
}
