namespace CardonerSistemas.Database
{
    partial class SelectDatasource
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDatasource));
            this.toolstripMain = new System.Windows.Forms.ToolStrip();
            this.buttonCancelar = new System.Windows.Forms.ToolStripButton();
            this.buttonAceptar = new System.Windows.Forms.ToolStripButton();
            this.comboboxDataSource = new System.Windows.Forms.ComboBox();
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
            this.toolstripMain.TabIndex = 8;
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
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
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
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // comboboxDataSource
            // 
            this.comboboxDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxDataSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboboxDataSource.FormattingEnabled = true;
            this.comboboxDataSource.Location = new System.Drawing.Point(66, 64);
            this.comboboxDataSource.Name = "comboboxDataSource";
            this.comboboxDataSource.Size = new System.Drawing.Size(285, 24);
            this.comboboxDataSource.TabIndex = 10;
            // 
            // pictureboxMain
            // 
            this.pictureboxMain.Image = ((System.Drawing.Image)(resources.GetObject("pictureboxMain.Image")));
            this.pictureboxMain.Location = new System.Drawing.Point(12, 49);
            this.pictureboxMain.Name = "pictureboxMain";
            this.pictureboxMain.Size = new System.Drawing.Size(48, 48);
            this.pictureboxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureboxMain.TabIndex = 9;
            this.pictureboxMain.TabStop = false;
            // 
            // SelectDatasource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 105);
            this.ControlBox = false;
            this.Controls.Add(this.toolstripMain);
            this.Controls.Add(this.comboboxDataSource);
            this.Controls.Add(this.pictureboxMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "SelectDatasource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione el origen de los datos";
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SelectDatasource_KeyPress);
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
        internal System.Windows.Forms.ComboBox comboboxDataSource;
        internal System.Windows.Forms.PictureBox pictureboxMain;
    }
}