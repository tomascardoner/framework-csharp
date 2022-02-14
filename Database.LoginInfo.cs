using System;
using System.Windows.Forms;

namespace CardonerSistemas.Database
{
    public partial class LoginInfo : Form
    {
        public LoginInfo()
        {
            InitializeComponent();
        }

        internal string Usuario { get; set; }
        internal string Password { get; set; }

        private void This_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Return:
                    buttonAceptar.PerformClick();
                    break;
                case (char)Keys.Escape:
                    buttonCancelar.PerformClick();
                    break;
                default:
                    break;
            }
        }

        private void ButtonAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textboxUsuario.Text))
            {
                MessageBox.Show("Debe ingresar el usuario.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                textboxUsuario.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void ButtonCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}