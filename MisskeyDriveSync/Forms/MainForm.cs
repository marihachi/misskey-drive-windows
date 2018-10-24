using Disboard.Misskey;
using MisskeyDriveSync.JsonFiles;
using MisskeyDriveSync.Models;
using MisskeyDriveSync.Properties;
using MisskeyDriveSync.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

		private async Task Scan1(MisskeyClient client, MisskeyDriveTreeNode currentNode, Disboard.Misskey.Models.Folder currentFolder = null)
		{
			// - folder

			currentNode.Folder = currentFolder;

			// - children

			Console.WriteLine("drive/folders");
			var folders = await client.Drive.FoldersAsync(folderId: currentFolder?.Id, limit: 100);

			// serial
			foreach (var folder in folders)
			{
				var childNode = new MisskeyDriveTreeNode { Parent = currentNode };
				currentNode.Children.Add(childNode);
				await Scan1(client, childNode, folder);
			}

			// - files

			Console.WriteLine("drive/files");
			currentNode.Files = (await client.Drive.FilesAsync(folderId: currentFolder?.Id, limit: 100)).ToList();
		}

		private async Task Scan2(MisskeyClient client, MisskeyDriveTreeNode currentNode, Disboard.Misskey.Models.Folder currentFolder = null)
		{
			// - folder

			currentNode.Folder = currentFolder;

			// - children

			Console.WriteLine("drive/folders");
			var folders = await client.Drive.FoldersAsync(folderId: currentFolder?.Id, limit: 100);

			// concurrent
			await Task.WhenAll(
				folders.Select(async folder =>
				{
					var childNode = new MisskeyDriveTreeNode { Parent = currentNode };
					currentNode.Children.Add(childNode);
					await Scan2(client, childNode, folder);
				}));

			// - files

			Console.WriteLine("drive/files");
			currentNode.Files = (await client.Drive.FilesAsync(folderId: currentFolder?.Id, limit: 100)).ToList();
		}

		private async Task Scan3(MisskeyClient client, MisskeyDriveTreeNode currentNode, Disboard.Misskey.Models.Folder currentFolder = null)
		{
			// - folder

			currentNode.Folder = currentFolder;

			// - children

			Console.WriteLine("drive/folders");
			var folders = await client.Drive.FoldersAsync(folderId: currentFolder?.Id, limit: 100);

			// concurrent(Task.Run)
			await Task.WhenAll(
				folders.Select(folder => Task.Run(async () =>
				{
					var childNode = new MisskeyDriveTreeNode { Parent = currentNode };
					currentNode.Children.Add(childNode);
					await Scan3(client, childNode, folder);
				})));

			// - files

			Console.WriteLine("drive/files");
			currentNode.Files = (await client.Drive.FilesAsync(folderId: currentFolder?.Id, limit: 100)).ToList();
		}

		private async Task Scan4(MisskeyClient client, MisskeyDriveTreeNode currentNode, Disboard.Misskey.Models.Folder currentFolder = null)
		{
			// - folder

			currentNode.Folder = currentFolder;

			// - children

			var t1 = Task.Run(async () =>
			{
				//await Task.Delay(10);
				Console.WriteLine("drive/folders");
				var folders = await client.Drive.FoldersAsync(folderId: currentFolder?.Id, limit: 100);

				// concurrent(Task.Run)
				await Task.WhenAll(
					folders.Select(folder => Task.Run(async () =>
					{
						var childNode = new MisskeyDriveTreeNode { Parent = currentNode };
						currentNode.Children.Add(childNode);
						await Scan4(client, childNode, folder);
					})));
			});

			// - files

			var t2 = Task.Run(async () =>
			{
				Console.WriteLine("drive/files");
				currentNode.Files = (await client.Drive.FilesAsync(folderId: currentFolder?.Id, limit: 100)).ToList();
			});

			await Task.WhenAll(new[] { t1, t2 });
		}

		private async Task processScan(int scanMethod)
		{
			// アカウント読み込み
			var loadedAccount = this.AccountsFile.Accounts[0];
			var client = MisskeyAccountConversion.FromAccountModel(loadedAccount, this.AccountsFile.Apps);

			var driveTree = new MisskeyDriveTreeNode();

			var sw = new Stopwatch();
			sw.Start();
			if (scanMethod == 1)
				await Scan1(client, driveTree);
			if (scanMethod == 2)
				await Scan2(client, driveTree);
			if (scanMethod == 3)
				await Scan3(client, driveTree);
			if (scanMethod == 4)
				await Scan4(client, driveTree);
			sw.Stop();

			Console.WriteLine($"{scanMethod}) {sw.Elapsed}ms");
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			await processScan(1);
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			await processScan(2);
		}

		private async void button3_Click(object sender, EventArgs e)
		{
			await processScan(3);
		}

		private async void button4_Click(object sender, EventArgs e)
		{
			await processScan(4);
		}
	}
}
