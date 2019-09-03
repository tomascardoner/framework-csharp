using System.Windows.Forms;

namespace CardonerSistemas
{
    public partial class ControlsOnScreenKeyboardAlphanumeric : UserControl
    {
        private const int _KeyboardRows = 4;
        private const int _KeyboardColumns = 11;
        private const string _KeyButtonNamePrefix = "buttonKey";
        private const string _KeyButtonNameRowPrefix = "R";
        private const string _KeyButtonNameColumnPrefix = "C";

        private string[,] _KeyboardKeys = new string[,]
        {
            { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "BS" },
            { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "DEL" },
            { "A", "S", "D", "F", "G", "H", "J", "K", "L", "Ñ", "Ç" },
            { "Z", "X", "C", "V", "B", "N", "M", "SPACE", "SPACE", "'", "Ü" }
        };

        public ControlsOnScreenKeyboardAlphanumeric()
        {
            InitializeComponent();

            CreateKeyboard();
        }

        private void CreateKeyboard()
        {
            CreatePanel();
            CreateButtons();
        }

        private void CreatePanel()
        {
            panelKeyboard.RowCount = _KeyboardRows;
            panelKeyboard.ColumnCount = _KeyboardColumns;
        }

        private void CreateButtons()
        {
            // Rows
            for (int row = 0; row < _KeyboardRows; row++)
            {
                Button previousButton = null;
                int previousColumnSpan = 1;

                // Columns
                for (int column = 0; column < _KeyboardColumns; column++)
                {
                    if (previousButton != null && previousButton.Text == _KeyboardKeys[row, column])
                    {
                        previousColumnSpan++;
                        panelKeyboard.SetColumnSpan(previousButton, previousColumnSpan);
                    }
                    else
                    {
                        Button button = new Button();
                        button.Name = string.Format("{0}{1}{2}{3}{4}", _KeyButtonNamePrefix, row, _KeyButtonNameRowPrefix , column, _KeyButtonNameColumnPrefix);
                        button.Text = _KeyboardKeys[row, column];
                        panelKeyboard.Controls.Add(button, column, row);
                        button.Dock = DockStyle.Fill;
                        previousButton = button;
                        previousColumnSpan = 1;
                    }
                }
            }
        }
    }
}
