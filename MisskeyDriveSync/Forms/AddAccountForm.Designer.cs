namespace MisskeyDriveSync.Forms
{
    partial class AddAccountForm
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
            this.authButton = new System.Windows.Forms.Button();
            this.domainBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.waitingLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // authButton
            // 
            this.authButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.authButton.Location = new System.Drawing.Point(204, 128);
            this.authButton.Margin = new System.Windows.Forms.Padding(6);
            this.authButton.Name = "authButton";
            this.authButton.Size = new System.Drawing.Size(94, 32);
            this.authButton.TabIndex = 3;
            this.authButton.Text = "連携";
            this.authButton.UseVisualStyleBackColor = true;
            this.authButton.Click += new System.EventHandler(this.authButton_Click);
            // 
            // domainBox
            // 
            this.domainBox.Location = new System.Drawing.Point(24, 42);
            this.domainBox.Name = "domainBox";
            this.domainBox.Size = new System.Drawing.Size(274, 22);
            this.domainBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "ドメイン名:";
            // 
            // waitingLabel
            // 
            this.waitingLabel.AutoSize = true;
            this.waitingLabel.Font = new System.Drawing.Font("MS UI Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.waitingLabel.ForeColor = System.Drawing.Color.Teal;
            this.waitingLabel.Location = new System.Drawing.Point(21, 87);
            this.waitingLabel.Name = "waitingLabel";
            this.waitingLabel.Size = new System.Drawing.Size(230, 17);
            this.waitingLabel.TabIndex = 7;
            this.waitingLabel.Text = "アプリ連携の許可を待っています...";
            this.waitingLabel.Visible = false;
            // 
            // AddAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 175);
            this.Controls.Add(this.waitingLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.domainBox);
            this.Controls.Add(this.authButton);
            this.Name = "AddAccountForm";
            this.Text = "AddAccountForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button authButton;
        private System.Windows.Forms.TextBox domainBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label waitingLabel;
    }
}