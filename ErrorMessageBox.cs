using System;
using System.Windows.Forms;

namespace CardonerSistemas
{
    public partial class ErrorMessageBox : Form
    {
        public ErrorMessageBox()
        {
            InitializeComponent();

            this.Text = CardonerSistemas.My.Application.Info.Title;

            DetailChanged(checkboxDetail, new EventArgs());
        }

        private void DetailChanged(object sender, EventArgs e)
        {
            if (checkboxDetail.Checked)
            { this.Height = textboxMessageData.Location.Y + textboxMessageData.Height + 50; }
            else
            { this.Height = checkboxDetail.Location.Y + checkboxDetail.Height + 50; }

            this.CenterToScreen();
        }

        private void FormClose(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
