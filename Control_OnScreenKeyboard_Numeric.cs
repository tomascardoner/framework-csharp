using System;
using System.Windows.Forms;

namespace CSTransporteKiosk
{
    public partial class CS_Control_OnScreenKeyboard_Numeric : UserControl
    {
        TextBox mDestinationTextBox;

        public CS_Control_OnScreenKeyboard_Numeric()
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
            KeyClicked(sender as Button);
        }

        private void KeyClicked(Button buttonKey)
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
                    SendKeyPress(CardonerSistemas.ConstantsKeys.BACKSPACE);
                    break;
                case keyNameSufixClear:
                    break;
                default:
                    keyCharValue = Convert.ToChar(keyNameSuffix);
                    SendKeyPress(Convert.ToString(keyCharValue));
                    break;
            }
        }

        private void SendKeyPress(string keysValue)
        {
            if(mDestinationTextBox != null)
            {
                mDestinationTextBox.Focus();
                SendKeys.Send(keysValue);
            }
        }
    }
}
