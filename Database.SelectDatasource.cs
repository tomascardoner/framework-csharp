using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardonerSistemas.Database
{
    public partial class SelectDatasource : Form
    {
        public SelectDatasource()
        {
            InitializeComponent();
        }

        private void SelectDatasource_KeyPress(object sender, KeyPressEventArgs e)
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

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            if (comboboxDataSource.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el origen de los datos.", CardonerSistemas.My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboboxDataSource.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
