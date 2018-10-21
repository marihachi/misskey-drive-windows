using Disboard.Misskey;
using MisskeyDriveSync.Models;
using MisskeyDriveSync.Properties;
using MisskeyDriveSync.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MisskeyDriveSync.Forms
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private NotifyIcon NotificationAreaIcon { get; set; }

		private bool IsExitApp = false;

		#region Refresh methods

		private void refreshVersionInfo()
		{
			var assembly = System.Reflection.Assembly.GetExecutingAssembly();
			var version = assembly.GetName().Version;

			this.versionLabel.Text = $"Version: {version.Major}.{version.Minor}";
		}

		#endregion Refresh methods

		#region Process Methods

		private void processCannel()
		{

		}

		private void processApply()
		{
			// TODO: save settings
		}

		#endregion Process Methods

		private void MainForm_Load(object sender, EventArgs e)
		{
#if !DEBUG
			this.mainTabControl.TabPages.Remove(this.debugTabPage);
#endif

			refreshVersionInfo();

			this.NotificationAreaIcon = new NotifyIcon()
			{
				Icon = Resources.mds,
				Visible = true,
				Text = "Misskey ドライブ",
				ContextMenuStrip = this.NotificationAreaMenu
			};
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			processApply();
			Hide();
		}

		private void applyButton_Click(object sender, EventArgs e)
		{
			processApply();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			processCannel();
			Hide();
		}

		private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Show();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.IsExitApp = true;
			Close();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing && !this.IsExitApp)
			{
				e.Cancel = true;
				processCannel();
				Hide();
			}
		}

		private MisskeyClient client;

		private async void button1_Click(object sender, EventArgs e)
		{
			var apps = new List<MisskeyApp>();

			Console.WriteLine("create app");

			// アプリ新規作成
			this.client = new MisskeyClient("misskey.xyz");
			await MisskeyService.CreateApp(this.client);
			apps.Add(MisskeyAppConversion.ToAppModel(this.client));
			var savedApp = apps[0];

			Console.WriteLine($"(APP SAVE) Domain: {apps[0].Domain}, ClientId: {apps[0].ClientId}, Secret: {apps[0].Secret}");

			Console.WriteLine("close");

			// 終了を再現
			apps.Clear();
			this.client = null;

			Console.WriteLine("load app");

			// アプリ読み込み
			var loadedApp = savedApp;
			this.client = MisskeyAppConversion.FromAppModel(loadedApp);
			apps.Add(MisskeyAppConversion.ToAppModel(this.client));

			Console.WriteLine($"(APP LOAD) Domain: {apps[0].Domain}, ClientId: {apps[0].ClientId}, Secret: {apps[0].Secret}");

			// ----

			Console.WriteLine("auth account");

			// アカウント認証
			await MisskeyService.Authorize(this.client);
			var savedAccount = MisskeyAccountConversion.ToAccountModel(this.client);

			Console.WriteLine($"(ACC SAVE) Domain: {savedAccount.Domain}, ClientId: {savedAccount.ClientId}, AccessToken: {savedAccount.AccessToken}");

			Console.WriteLine("load account");

			// アカウント読み込み
			var loadedAccount = savedAccount;
			this.client = MisskeyAccountConversion.FromAccountModel(loadedAccount, apps);
			var account = MisskeyAccountConversion.ToAccountModel(this.client);

			Console.WriteLine($"(ACC LOAD) Domain: {account.Domain}, ClientId: {account.ClientId}, AccessToken: {account.AccessToken}");
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			var foldersInRoot = await this.client.Drive.FoldersAsync();

			foreach (var folder in foldersInRoot)
			{
				Console.WriteLine($"(Folder) Id={folder.Id} Name={folder.Name} ParentId={folder.ParentId}");
			}
		}
	}
}
