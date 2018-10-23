namespace MisskeyDriveSync.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.mainTabControl = new System.Windows.Forms.TabControl();
			this.generalTabPage = new System.Windows.Forms.TabPage();
			this.accountsTabPage = new System.Windows.Forms.TabPage();
			this.removeAccountButton = new System.Windows.Forms.Button();
			this.addAccountButton = new System.Windows.Forms.Button();
			this.accountListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.versionInfoTabPage = new System.Windows.Forms.TabPage();
			this.versionLabel = new System.Windows.Forms.Label();
			this.appNameLabel = new System.Windows.Forms.Label();
			this.debugTabPage = new System.Windows.Forms.TabPage();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.NotificationAreaMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainTabControl.SuspendLayout();
			this.accountsTabPage.SuspendLayout();
			this.versionInfoTabPage.SuspendLayout();
			this.debugTabPage.SuspendLayout();
			this.NotificationAreaMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTabControl
			// 
			this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mainTabControl.Controls.Add(this.generalTabPage);
			this.mainTabControl.Controls.Add(this.accountsTabPage);
			this.mainTabControl.Controls.Add(this.versionInfoTabPage);
			this.mainTabControl.Controls.Add(this.debugTabPage);
			this.mainTabControl.Location = new System.Drawing.Point(12, 12);
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.Padding = new System.Drawing.Point(15, 3);
			this.mainTabControl.SelectedIndex = 0;
			this.mainTabControl.Size = new System.Drawing.Size(600, 384);
			this.mainTabControl.TabIndex = 0;
			// 
			// generalTabPage
			// 
			this.generalTabPage.Location = new System.Drawing.Point(4, 22);
			this.generalTabPage.Name = "generalTabPage";
			this.generalTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
			this.generalTabPage.Size = new System.Drawing.Size(592, 358);
			this.generalTabPage.TabIndex = 0;
			this.generalTabPage.Text = "全般";
			this.generalTabPage.UseVisualStyleBackColor = true;
			// 
			// accountsTabPage
			// 
			this.accountsTabPage.Controls.Add(this.removeAccountButton);
			this.accountsTabPage.Controls.Add(this.addAccountButton);
			this.accountsTabPage.Controls.Add(this.accountListView);
			this.accountsTabPage.Location = new System.Drawing.Point(4, 22);
			this.accountsTabPage.Name = "accountsTabPage";
			this.accountsTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
			this.accountsTabPage.Size = new System.Drawing.Size(592, 358);
			this.accountsTabPage.TabIndex = 1;
			this.accountsTabPage.Text = "アカウント";
			this.accountsTabPage.UseVisualStyleBackColor = true;
			// 
			// removeAccountButton
			// 
			this.removeAccountButton.Enabled = false;
			this.removeAccountButton.Location = new System.Drawing.Point(93, 242);
			this.removeAccountButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.removeAccountButton.Name = "removeAccountButton";
			this.removeAccountButton.Size = new System.Drawing.Size(65, 26);
			this.removeAccountButton.TabIndex = 3;
			this.removeAccountButton.Text = "削除";
			this.removeAccountButton.UseVisualStyleBackColor = true;
			this.removeAccountButton.Click += new System.EventHandler(this.removeAccountButton_Click);
			// 
			// addAccountButton
			// 
			this.addAccountButton.Location = new System.Drawing.Point(20, 242);
			this.addAccountButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.addAccountButton.Name = "addAccountButton";
			this.addAccountButton.Size = new System.Drawing.Size(65, 26);
			this.addAccountButton.TabIndex = 1;
			this.addAccountButton.Text = "追加";
			this.addAccountButton.UseVisualStyleBackColor = true;
			this.addAccountButton.Click += new System.EventHandler(this.addAccountButton_Click);
			// 
			// accountListView
			// 
			this.accountListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.accountListView.GridLines = true;
			this.accountListView.Location = new System.Drawing.Point(20, 27);
			this.accountListView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.accountListView.Name = "accountListView";
			this.accountListView.Size = new System.Drawing.Size(360, 201);
			this.accountListView.TabIndex = 0;
			this.accountListView.UseCompatibleStateImageBehavior = false;
			this.accountListView.View = System.Windows.Forms.View.Details;
			this.accountListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "アカウント名";
			this.columnHeader1.Width = 300;
			// 
			// versionInfoTabPage
			// 
			this.versionInfoTabPage.Controls.Add(this.versionLabel);
			this.versionInfoTabPage.Controls.Add(this.appNameLabel);
			this.versionInfoTabPage.Location = new System.Drawing.Point(4, 22);
			this.versionInfoTabPage.Name = "versionInfoTabPage";
			this.versionInfoTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
			this.versionInfoTabPage.Size = new System.Drawing.Size(592, 358);
			this.versionInfoTabPage.TabIndex = 2;
			this.versionInfoTabPage.Text = "バージョン情報";
			this.versionInfoTabPage.UseVisualStyleBackColor = true;
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.versionLabel.Location = new System.Drawing.Point(30, 102);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(71, 15);
			this.versionLabel.TabIndex = 1;
			this.versionLabel.Text = "Version: -";
			// 
			// appNameLabel
			// 
			this.appNameLabel.AutoSize = true;
			this.appNameLabel.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.appNameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.appNameLabel.Location = new System.Drawing.Point(29, 43);
			this.appNameLabel.Name = "appNameLabel";
			this.appNameLabel.Size = new System.Drawing.Size(339, 21);
			this.appNameLabel.TabIndex = 0;
			this.appNameLabel.Text = "Misskey ドライブ for Windows Desktop";
			// 
			// debugTabPage
			// 
			this.debugTabPage.Controls.Add(this.textBox1);
			this.debugTabPage.Controls.Add(this.button1);
			this.debugTabPage.Controls.Add(this.button2);
			this.debugTabPage.Location = new System.Drawing.Point(4, 22);
			this.debugTabPage.Name = "debugTabPage";
			this.debugTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
			this.debugTabPage.Size = new System.Drawing.Size(592, 358);
			this.debugTabPage.TabIndex = 3;
			this.debugTabPage.Text = "debug";
			this.debugTabPage.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(32, 75);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(180, 19);
			this.textBox1.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(32, 30);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(32, 100);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(434, 404);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(85, 26);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(340, 404);
			this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(85, 26);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.applyButton.Location = new System.Drawing.Point(528, 404);
			this.applyButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(85, 26);
			this.applyButton.TabIndex = 3;
			this.applyButton.Text = "適用";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// NotificationAreaMenu
			// 
			this.NotificationAreaMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.NotificationAreaMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem,
            this.toolStripSeparator1,
            this.終了XToolStripMenuItem});
			this.NotificationAreaMenu.Name = "NotificationAreaMenu";
			this.NotificationAreaMenu.Size = new System.Drawing.Size(131, 54);
			// 
			// 設定ToolStripMenuItem
			// 
			this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
			this.設定ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.設定ToolStripMenuItem.Text = "設定(&S)...";
			this.設定ToolStripMenuItem.Click += new System.EventHandler(this.SettingToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(127, 6);
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.終了XToolStripMenuItem.Text = "終了(&X)";
			this.終了XToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(624, 442);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.mainTabControl);
			this.MinimumSize = new System.Drawing.Size(639, 478);
			this.Name = "MainForm";
			this.Text = "Misskey ドライブ";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.mainTabControl.ResumeLayout(false);
			this.accountsTabPage.ResumeLayout(false);
			this.versionInfoTabPage.ResumeLayout(false);
			this.versionInfoTabPage.PerformLayout();
			this.debugTabPage.ResumeLayout(false);
			this.debugTabPage.PerformLayout();
			this.NotificationAreaMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl mainTabControl;
		private System.Windows.Forms.TabPage generalTabPage;
		private System.Windows.Forms.TabPage accountsTabPage;
		private System.Windows.Forms.TabPage versionInfoTabPage;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.Label appNameLabel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TabPage debugTabPage;
		private System.Windows.Forms.ContextMenuStrip NotificationAreaMenu;
		private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.ListView accountListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button addAccountButton;
		private System.Windows.Forms.Button removeAccountButton;
	}
}

