namespace CardonerSistemas.Database
{
    partial class LoginInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginInfo));
            this.toolstripMain = new System.Windows.Forms.ToolStrip();
            this.buttonCancelar = new System.Windows.Forms.ToolStripButton();
            this.buttonAceptar = new System.Windows.Forms.ToolStripButton();
            this.textboxPassword = new System.Windows.Forms.TextBox();
            this.textboxUsuario = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.pictureboxMain = new System.Windows.Forms.PictureBox();
            this.toolstripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // toolstripMain
            // 
            this.toolstripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolstripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonCancelar,
            this.buttonAceptar});
            this.toolstripMain.Location = new System.Drawing.Point(0, 0);
            this.toolstripMain.Name = "toolstripMain";
            this.toolstripMain.Size = new System.Drawing.Size(363, 39);
            this.toolstripMain.TabIndex = 4;
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonCancelar.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancelar.Image")));
            this.buttonCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(89, 36);
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.Click += new System.EventHandler(this.ButtonCancelar_Click);
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonAceptar.Image = ((System.Drawing.Image)(resources.GetObject("buttonAceptar.Image")));
            this.buttonAceptar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAceptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(84, 36);
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.Click += new System.EventHandler(this.ButtonAceptar_Click);
            // 
            // textboxPassword
            // 
            this.textboxPassword.Location = new System.Drawing.Point(136, 73);
            this.textboxPassword.MaxLength = 128;
            this.textboxPassword.Name = "textboxPassword";
            this.textboxPassword.Size = new System.Drawing.Size(215, 20);
            this.textboxPassword.TabIndex = 3;
            this.textboxPassword.UseSystemPasswordChar = true;
            // 
            // textboxUsuario
            // 
            this.textboxUsuario.Location = new System.Drawing.Point(136, 43);
            this.textboxUsuario.MaxLength = 20;
            this.textboxUsuario.Name = "textboxUsuario";
            this.textboxUsuario.Size = new System.Drawing.Size(215, 20);
            this.textboxUsuario.TabIndex = 1;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(66, 76);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(64, 13);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Contraseña:";
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Location = new System.Drawing.Point(66, 46);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(46, 13);
            this.labelUsuario.TabIndex = 0;
            this.labelUsuario.Text = "Usuario:";
            // 
            // pictureboxMain
            // 
            this.pictureboxMain.Image = ((System.Drawing.Image)(resources.GetObject("pictureboxMain.Image")));
            this.pictureboxMain.Location = new System.Drawing.Point(12, 42);
            this.pictureboxMain.Name = "pictureboxMain";
            this.pictureboxMain.Size = new System.Drawing.Size(48, 48);
            this.pictureboxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureboxMain.TabIndex = 13;
            this.pictureboxMain.TabStop = false;
            // 
            // LoginInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 105);
            this.ControlBox = false;
            this.Controls.Add(this.textboxPassword);
            this.Controls.Add(this.textboxUsuario);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUsuario);
            this.Controls.Add(this.pictureboxMain);
            this.Controls.Add(this.toolstripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "LoginInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione el origen de los datos";
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.This_KeyPress);
            this.toolstripMain.ResumeLayout(false);
            this.toolstripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip toolstripMain;
        internal System.Windows.Forms.ToolStripButton buttonCancelar;
        internal System.Windows.Forms.ToolStripButton buttonAceptar;
        internal System.Windows.Forms.Label labelPassword;
        internal System.Windows.Forms.Label labelUsuario;
        internal System.Windows.Forms.PictureBox pictureboxMain;
        internal System.Windows.Forms.TextBox textboxPassword;
        internal System.Windows.Forms.TextBox textboxUsuario;
    }
}