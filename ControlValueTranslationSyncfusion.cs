using System;

namespace CardonerSistemas
{
    static class ControlValueTranslationSyncfusion
    {

        #region De objectos a controles - Integer TextBox

        static internal void ByteToIntegerTextBox(Syncfusion.Windows.Forms.Tools.IntegerTextBox control, byte? value)
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

        static internal void ShortToIntegerTextBox(Syncfusion.Windows.Forms.Tools.IntegerTextBox control, short? value)
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

        static internal void IntToIntegerTextBox(Syncfusion.Windows.Forms.Tools.IntegerTextBox control, int? value)
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

        static internal void LongToIntegerTextBox(Syncfusion.Windows.Forms.Tools.IntegerTextBox control, long? value)
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

        #endregion

        #region De objectos a controles - Currency TextBox

        static internal void DecimalToCurrencyTextBox(Syncfusion.Windows.Forms.Tools.CurrencyTextBox control, Decimal? value)
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

        #region De objectos a controles - Percent TextBox

        static internal void DecimalToPercentTextBox(Syncfusion.Windows.Forms.Tools.PercentTextBox control, Decimal? value)
        {
            if (value.HasValue)
            {
                control.DoubleValue = (double)value.Value;
            }
            else
            {
                if (control.AllowNull)
                {
                    control.BindableValue = null;
                }
                else
                {
                    control.DoubleValue = control.MinValue;
                }
            }
        }

        #endregion

        #region De objectos a controles - Double TextBox

        static internal void DecimalToDoubleTextBox(Syncfusion.Windows.Forms.Tools.DoubleTextBox control, Decimal? value)
        {
            if (value.HasValue)
            {
                control.DoubleValue = Convert.ToDouble(value.Value);
            }
            else
            {
                if (control.AllowNull)
                {
                    control.BindableValue = null;
                }
                else
                {
                    control.DoubleValue = control.MinValue;
                }
            }
        }

        #endregion

        #region De controles a objectos - Integer TextBox

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

        #endregion

        #region De controles a objectos - Currency TextBox

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

        #region De controles a objectos - Percent TextBox

        static internal decimal? PercentTextBoxToDecimal(Syncfusion.Windows.Forms.Tools.PercentTextBox control)
        {
            if (control.AllowNull && control.IsNull)
            {
                return null;
            }
            else
            {
                return (decimal)control.DoubleValue;
            }
        }

        #endregion

        #region De controles a objectos - Double TextBox

        static internal decimal? DoubleTextBoxToDecimal(Syncfusion.Windows.Forms.Tools.DoubleTextBox control)
        {
            if (control.AllowNull && control.IsNull)
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(control.DoubleValue);
            }
        }

        #endregion

    }
}
