using System.Windows.Forms;

namespace CardonerSistemas
{
    static class ComboBox
    {

        #region Declarations

        internal enum SelectedItemOptions
        {
            None,
            First,
            NoneOrFirstIfUnique,
            Last,
            Current,
            CurrentOrFirst,
            CurrentOrFirstIfUnique,
            CurrentOrLast,
            Value,
            ValueOrFirst,
            ValueOrFirstIfUnique,
            ValueOrLast
        }

        #endregion

        static internal void SetSelectedValue(System.Windows.Forms.ComboBox comboBox, SelectedItemOptions options = SelectedItemOptions.None, object valueToSelect = null, object valueForNull = null)
        {
            object selectedValue;

            if (comboBox.Items.Count > 0)
            {
                selectedValue = comboBox.SelectedValue;
                if (valueToSelect == null)
                {
                    valueToSelect = valueForNull;
                }

                switch (options)
                {
                    case SelectedItemOptions.None:
                        comboBox.SelectedIndex = -1;
                        break;
                    case SelectedItemOptions.First:
                        comboBox.SelectedIndex = 0;
                        break;
                    case SelectedItemOptions.NoneOrFirstIfUnique:
                        if (comboBox.Items.Count == 1)
                        {
                            comboBox.SelectedIndex = 0;
                        }
                        else
                        {
                            comboBox.SelectedIndex = -1;
                        }
                        break;
                    case SelectedItemOptions.Last:
                        comboBox.SelectedIndex = comboBox.Items.Count - 1;
                        break;
                    case SelectedItemOptions.Current:
                        comboBox.SelectedValue = selectedValue;
                        break;
                    case SelectedItemOptions.CurrentOrFirst:
                        comboBox.SelectedValue = selectedValue;
                        if (comboBox.SelectedValue == null)
                        {
                            comboBox.SelectedIndex = 0;
                        }
                        break;
                    case SelectedItemOptions.CurrentOrFirstIfUnique:
                        comboBox.SelectedValue = selectedValue;
                        if (comboBox.SelectedValue == null)
                        {
                            if (comboBox.Items.Count == 1)
                            {
                                comboBox.SelectedIndex = 0;
                            }
                            else
                            {
                                comboBox.SelectedIndex = -1;
                            }
                        }
                        break;
                    case SelectedItemOptions.CurrentOrLast:
                        comboBox.SelectedValue = selectedValue;
                        if (comboBox.SelectedValue == null)
                        {
                            comboBox.SelectedIndex = comboBox.Items.Count - 1;
                        }
                        break;
                    case SelectedItemOptions.Value:
                        if (valueToSelect == null)
                        {
                            comboBox.SelectedIndex = -1;
                        }
                        else
                        {
                            comboBox.SelectedValue = valueToSelect;
                        }
                        break;
                    case SelectedItemOptions.ValueOrFirst:
                        if (valueToSelect == null)
                        {
                            comboBox.SelectedIndex = 0;
                        }
                        else
                        {
                            comboBox.SelectedValue = valueToSelect;
                            if (comboBox.SelectedValue == null)
                            {
                                comboBox.SelectedIndex = 0;
                            }
                        }
                        break;
                    case SelectedItemOptions.ValueOrFirstIfUnique:
                        if (valueToSelect == null)
                        {
                            if (comboBox.Items.Count == 1)
                            {
                                comboBox.SelectedIndex = 0;
                            }
                            else
                            {
                                comboBox.SelectedIndex = -1;
                            }
                        }
                        else
                        {
                            comboBox.SelectedValue = selectedValue;
                            if (comboBox.SelectedValue == null)
                            {
                                if (comboBox.Items.Count == 1)
                                {
                                    comboBox.SelectedIndex = 0;
                                }
                                else
                                {
                                    comboBox.SelectedIndex = -1;
                                }
                            }
                        }
                        break;
                    case SelectedItemOptions.ValueOrLast:
                        if (valueToSelect == null)
                        {
                            if (comboBox.SelectedValue == null)
                            {
                                comboBox.SelectedIndex = comboBox.Items.Count - 1;
                            }
                        }
                        else
                        {
                            comboBox.SelectedValue = valueToSelect;
                            if (comboBox.SelectedValue == null)
                            {
                                comboBox.SelectedIndex = comboBox.Items.Count - 1;
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        static internal void SetItemByDisplayValue(System.Windows.Forms.ComboBox comboBox, string displayValue, SelectedItemOptions options = SelectedItemOptions.None)
        {
            if (comboBox.Items.Count == 0)
            {
                return;
            }

            int index = comboBox.FindStringExact(displayValue);
            if (index == -1)
            {
                switch (options)
                {
                    case SelectedItemOptions.None:
                        comboBox.SelectedIndex = -1;
                        break;
                    case SelectedItemOptions.First:
                        comboBox.SelectedIndex = 0;
                        break;
                    case SelectedItemOptions.NoneOrFirstIfUnique:
                        if (comboBox.Items.Count == 1)
                        {
                            comboBox.SelectedIndex = 0;
                        }
                        else
                        {
                            comboBox.SelectedIndex = -1;
                        }
                        break;
                    case SelectedItemOptions.Last:
                        comboBox.SelectedIndex = comboBox.Items.Count - 1;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                comboBox.SelectedIndex = index;
            }
        }

        static internal int GetMaxWidthOfItems(System.Windows.Forms.ComboBox comboBox)
        {
            int maxWidth = 0;
            int tempWidth;
            string itemText;

            if (!string.IsNullOrWhiteSpace(comboBox.DisplayMember))
            {
                foreach (var obj in comboBox.Items)
                {
                    itemText = (string)obj.GetType().GetProperty(comboBox.DisplayMember).GetValue(obj, null);

                    tempWidth = TextRenderer.MeasureText(itemText, comboBox.Font).Width;
                    if (tempWidth > maxWidth)
                    {
                        maxWidth = tempWidth;
                    }
                }
            }
            return maxWidth;
        }

    }
}
