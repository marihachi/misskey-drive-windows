using MisskeyDriveSync.JsonFiles;
using MisskeyDriveSync.Models;
using MisskeyDriveSync.Properties;
using MisskeyDriveSync.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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

		private AccountsFile AccountsFile { get; set; }

		private SettingFile SettingFile { get; set; }

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

		private async void MainForm_Load(object sender, EventArgs e)
		{
#if !DEBUG
			this.mainTabControl.TabPages.Remove(this.debugTabPage);
#endif

			this.refreshVersionInfo();

			this.NotificationAreaIcon = new NotifyIcon()
			{
				Icon = Resources.mds,
				Visible = true,
				Text = "Misskey ドライブ",
				ContextMenuStrip = this.NotificationAreaMenu
			};

			this.AccountsFile = await AccountsFile.LoadAsync();

			foreach (var account in this.AccountsFile.Accounts)
			{
				this.accountListView.Items.Add(new ListViewItem(new[] { $"{account.Username}@{account.Domain}" })
				{
					Tag = account
				});
			}
		}

		private void addAccountButton_Click(object sender, EventArgs e)
		{
			var f = new AddAccountForm(this.AccountsFile);
			if (f.ShowDialog() == DialogResult.OK)
			{
				var account = f.Data;

				// 新しくAccountsに追加された場合
				if (account != null)
				{
					// リストに項目を追加
					this.accountListView.Items.Add(new ListViewItem(new[] { $"{account.Username}@{account.Domain}" })
					{
						Tag = account
					});
				}
			}
		}

		private async void removeAccountButton_Click(object sender, EventArgs e)
		{
			var item = this.accountListView.SelectedItems[0];
			var account = (MisskeyAccount)item.Tag;
			this.AccountsFile.Accounts.Remove(account);
			await this.AccountsFile.SaveAsync();
			this.accountListView.Items.Remove(item);
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selected = this.accountListView.SelectedIndices.Count != 0;
			this.removeAccountButton.Enabled = selected;
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
			if (this.WindowState == FormWindowState.Minimized)
			{
				this.WindowState = FormWindowState.Normal;
			}
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

		private async void button1_Click(object sender, EventArgs e)
		{
			// アカウント読み込み
			var loadedAccount = this.AccountsFile.Accounts[0];
			var client = MisskeyAccountConversion.FromAccountModel(loadedAccount, this.AccountsFile.Apps);

			var tree = await MisskeyService.Instance.Scan(client);

			MisskeyService.Instance.ShowNodeTree(tree);
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			// アカウント読み込み
			var loadedAccount = this.AccountsFile.Accounts[0];
			var client = MisskeyAccountConversion.FromAccountModel(loadedAccount, this.AccountsFile.Apps);

			var tree = await MisskeyService.Instance.Scan(client);
			var file = tree.Files[0];
			var fileName = $"{file.Id}.{file.Name}";

			var bytes = await Task.Run(() => System.IO.File.ReadAllBytes($"misskey-drive/{fileName}"));

			var hash = MisskeyService.Instance.CalcMd5(bytes);

			Console.WriteLine($"source:   {file.Md5}");
			Console.WriteLine($"generate: {hash}");
		}

		private async void button3_Click(object sender, EventArgs e)
		{
			// アカウント読み込み
			var loadedAccount = this.AccountsFile.Accounts[0];
			var client = MisskeyAccountConversion.FromAccountModel(loadedAccount, this.AccountsFile.Apps);

			var tree = await MisskeyService.Instance.Scan(client);

			Console.WriteLine("downloading...");

			var queue = new ConcurrentTaskQueuing();

			foreach (var file in tree.Files)
			{
				var t = new Task(() =>
				{
					try
					{
						MisskeyService.Instance.DownloadFile(file).Wait();
					}
					catch (AggregateException ex) when (ex?.InnerException is HttpException ex2)
					{
						Console.WriteLine($"{file.Id} ({file.Name}): {ex2.StatusCode}");
					}
				});

				queue.TaskQueue.Add(t);
			}

			await queue.Start(2);

			Console.WriteLine("done");
		}

		private async void button4_Click(object sender, EventArgs e)
		{
			// アカウント読み込み
			var loadedAccount = this.AccountsFile.Accounts[0];
			var client = MisskeyAccountConversion.FromAccountModel(loadedAccount, this.AccountsFile.Apps);

			var tree = await MisskeyService.Instance.Scan(client);

			var pathes = MisskeyService.Instance.BuildPassList(tree, new List<string>());

			foreach (var path in pathes)
			{
				var pathStr = string.Join("/", path);
				Console.WriteLine(pathStr);
			}
		}

		private async void button5_Click(object sender, EventArgs e)
		{
			
		}
	}
}
