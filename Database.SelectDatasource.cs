﻿using System;
using System.Windows.Forms;

namespace CardonerSistemas.Database
{
    public partial class SelectDatasource : Form
    {
        public SelectDatasource()
        {
            InitializeComponent();
        }

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
            if (comboboxDataSource.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el origen de los datos.", CardonerSistemas.My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboboxDataSource.Focus();
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
