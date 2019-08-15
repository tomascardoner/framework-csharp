using System;
using System.Windows.Forms;

namespace CardonerSistemas
{
    public partial class ControlsOnScreenKeyboardNumeric : UserControl
    {
        TextBox mDestinationTextBox;

        public ControlsOnScreenKeyboardNumeric()
        {
            InitializeComponent();
        }

        public TextBox DestinationTextBox
        {
            get { return mDestinationTextBox; }
            set { mDestinationTextBox = value; }
        }

        private void KeyMouseUp(object sender, MouseEventArgs e)
        {
            ConvertButtonClickedToKeyOrder(sender as Button);

            if (mDestinationTextBox != null && mDestinationTextBox.Enabled && !mDestinationTextBox.ReadOnly)
            {
                mDestinationTextBox.Focus();
            }
            else
            {
                this.Parent.Focus();
            }
            this.OnClick(e);
        }

        private void ConvertButtonClickedToKeyOrder(Button buttonKey)
        {
            const string keyNamePrefix = "buttonKey";
            const string keyNameSufixDelete = "Backspace";
            const string keyNameSufixClear = "Clear";

            string keyNameSuffix;
            char keyCharValue;

            keyNameSuffix = buttonKey.Name.Substring(keyNamePrefix.Length);
            switch (keyNameSuffix)
            {
                case keyNameSufixDelete:
                    SendKeyOrder(CardonerSistemas.ConstantsKeys.BACKSPACE);
                    break;

                case keyNameSufixClear:
                    SendKeyOrder(CardonerSistemas.ConstantsKeys.ESC);
                    break;

                default:
                    keyCharValue = Convert.ToChar(keyNameSuffix);
                    SendKeyOrder(Convert.ToString(keyCharValue));
                    break;
            }
        }

        private void SendKeyOrder(string keysValue)
        {
            if(mDestinationTextBox != null)
            {
                if (mDestinationTextBox.Enabled && !mDestinationTextBox.ReadOnly)
                {
                    // Textbox is enabled for typing

                    // ensure that the textbox control has the focus
                    mDestinationTextBox.Focus();
                    // ensure that the insertion point is at the end
                    mDestinationTextBox.SelectionStart = mDestinationTextBox.TextLength;
                    // send the key pressed
                    SendKeys.Send(keysValue);
                }
                else
                {
                    // textbox is disabled or in read-only mode
                    switch (keysValue)
                    {
                        case CardonerSistemas.ConstantsKeys.BACKSPACE:
                            if (mDestinationTextBox.TextLength > 0)
                            {
                                mDestinationTextBox.Text = mDestinationTextBox.Text.Remove(mDestinationTextBox.TextLength - 1);
                            }
                            break;

                        case CardonerSistemas.ConstantsKeys.ESC:
                            mDestinationTextBox.Text = "";
                            break;

                        default:
                            if (mDestinationTextBox.TextLength < mDestinationTextBox.MaxLength)
                            {
                                mDestinationTextBox.Text += keysValue;
                            }
                            break;
                    }
                }
            }
        }
    }
}
