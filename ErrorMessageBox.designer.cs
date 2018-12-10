namespace CardonerSistemas
{
    partial class ErrorMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardonerSistemas.ErrorMessageBox));
            this.textboxMessageData = new System.Windows.Forms.TextBox();
            this.textboxStackTraceData = new System.Windows.Forms.TextBox();
            this.labelStackTrace = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.labelSourceData = new System.Windows.Forms.Label();
            this.labelSource = new System.Windows.Forms.Label();
            this.pictureboxError = new System.Windows.Forms.PictureBox();
            this.checkboxDetail = new System.Windows.Forms.CheckBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelFriendlyMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxError)).BeginInit();
            this.SuspendLayout();
            // 
            // textboxMessageData
            // 
            this.textboxMessageData.BackColor = System.Drawing.SystemColors.Control;
            this.textboxMessageData.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxMessageData.Location = new System.Drawing.Point(119, 327);
            this.textboxMessageData.Margin = new System.Windows.Forms.Padding(4);
            this.textboxMessageData.MaxLength = 0;
            this.textboxMessageData.Multiline = true;
            this.textboxMessageData.Name = "textboxMessageData";
            this.textboxMessageData.ReadOnly = true;
            this.textboxMessageData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxMessageData.Size = new System.Drawing.Size(511, 106);
            this.textboxMessageData.TabIndex = 31;
            // 
            // textboxStackTraceData
            // 
            this.textboxStackTraceData.BackColor = System.Drawing.SystemColors.Control;
            this.textboxStackTraceData.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxStackTraceData.Location = new System.Drawing.Point(119, 208);
            this.textboxStackTraceData.Margin = new System.Windows.Forms.Padding(4);
            this.textboxStackTraceData.MaxLength = 0;
            this.textboxStackTraceData.Multiline = true;
            this.textboxStackTraceData.Name = "textboxStackTraceData";
            this.textboxStackTraceData.ReadOnly = true;
            this.textboxStackTraceData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxStackTraceData.Size = new System.Drawing.Size(511, 99);
            this.textboxStackTraceData.TabIndex = 29;
            // 
            // labelStackTrace
            // 
            this.labelStackTrace.AutoSize = true;
            this.labelStackTrace.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelStackTrace.Location = new System.Drawing.Point(17, 208);
            this.labelStackTrace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStackTrace.Name = "labelStackTrace";
            this.labelStackTrace.Size = new System.Drawing.Size(55, 17);
            this.labelStackTrace.TabIndex = 28;
            this.labelStackTrace.Text = "Origen:";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelMessage.Location = new System.Drawing.Point(16, 331);
            this.labelMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(86, 17);
            this.labelMessage.TabIndex = 30;
            this.labelMessage.Text = "Descripción:";
            // 
            // labelSourceData
            // 
            this.labelSourceData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSourceData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSourceData.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceData.Location = new System.Drawing.Point(119, 174);
            this.labelSourceData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSourceData.Name = "labelSourceData";
            this.labelSourceData.Size = new System.Drawing.Size(512, 21);
            this.labelSourceData.TabIndex = 27;
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelSource.Location = new System.Drawing.Point(17, 174);
            this.labelSource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(67, 17);
            this.labelSource.TabIndex = 26;
            this.labelSource.Text = "Contexto:";
            // 
            // pictureboxError
            // 
            this.pictureboxError.Image = ((System.Drawing.Image)(resources.GetObject("pictureboxError.Image")));
            this.pictureboxError.Location = new System.Drawing.Point(16, 34);
            this.pictureboxError.Margin = new System.Windows.Forms.Padding(4);
            this.pictureboxError.Name = "pictureboxError";
            this.pictureboxError.Size = new System.Drawing.Size(48, 48);
            this.pictureboxError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureboxError.TabIndex = 25;
            this.pictureboxError.TabStop = false;
            // 
            // checkboxDetail
            // 
            this.checkboxDetail.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkboxDetail.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkboxDetail.Location = new System.Drawing.Point(529, 132);
            this.checkboxDetail.Margin = new System.Windows.Forms.Padding(4);
            this.checkboxDetail.Name = "checkboxDetail";
            this.checkboxDetail.Size = new System.Drawing.Size(101, 30);
            this.checkboxDetail.TabIndex = 24;
            this.checkboxDetail.Text = "&Detalle >>";
            this.checkboxDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkboxDetail.CheckedChanged += new System.EventHandler(this.DetailChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonClose.Location = new System.Drawing.Point(417, 132);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(101, 30);
            this.buttonClose.TabIndex = 23;
            this.buttonClose.Text = "&Cerrar";
            this.buttonClose.Click += new System.EventHandler(this.FormClose);
            // 
            // labelFriendlyMessage
            // 
            this.labelFriendlyMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelFriendlyMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelFriendlyMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFriendlyMessage.Location = new System.Drawing.Point(95, 11);
            this.labelFriendlyMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFriendlyMessage.Name = "labelFriendlyMessage";
            this.labelFriendlyMessage.Size = new System.Drawing.Size(535, 105);
            this.labelFriendlyMessage.TabIndex = 22;
            // 
            // CS_ErrorMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 444);
            this.Controls.Add(this.textboxMessageData);
            this.Controls.Add(this.textboxStackTraceData);
            this.Controls.Add(this.labelStackTrace);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelSourceData);
            this.Controls.Add(this.labelSource);
            this.Controls.Add(this.pictureboxError);
            this.Controls.Add(this.checkboxDetail);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelFriendlyMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "CS_ErrorMessageBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CS_ErrorMessageBox";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textboxMessageData;
        internal System.Windows.Forms.TextBox textboxStackTraceData;
        internal System.Windows.Forms.Label labelStackTrace;
        internal System.Windows.Forms.Label labelMessage;
        internal System.Windows.Forms.Label labelSourceData;
        internal System.Windows.Forms.Label labelSource;
        internal System.Windows.Forms.PictureBox pictureboxError;
        internal System.Windows.Forms.CheckBox checkboxDetail;
        internal System.Windows.Forms.Button buttonClose;
        internal System.Windows.Forms.Label labelFriendlyMessage;
    }
}