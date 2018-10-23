using Disboard.Misskey;
using MisskeyDriveSync.JsonFiles;
using MisskeyDriveSync.Models;
using MisskeyDriveSync.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MisskeyDriveSync.Forms
{
    public partial class AddAccountForm : Form
    {
        public AddAccountForm(AccountsFile accountsFile)
        {
            InitializeComponent();
            this.AccountsFile = accountsFile;
        }

        private AccountsFile AccountsFile;
        public MisskeyAccount Data { get; private set; }

        private async void authButton_Click(object sender, EventArgs e)
        {
            this.authButton.Enabled = false;

            if (string.IsNullOrEmpty(this.domainBox.Text))
            {
                this.authButton.Enabled = true;
                MessageBox.Show("ドメイン名が無効です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Uri uri;
            try
            {
                uri = new Uri($"https://{this.domainBox.Text}");
            }
            catch
            {
                this.authButton.Enabled = true;
                MessageBox.Show("ドメイン名が無効です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var app = this.AccountsFile.Apps.FirstOrDefault(i => i.Domain == uri.Host);

            var isNewApp = (app == null);

            if (isNewApp)
            {
                try
                {
                    var clientForCreateApp = new MisskeyClient(uri.Host);
                    await MisskeyService.CreateApp(clientForCreateApp);
                    app = MisskeyAppConversion.ToAppModel(clientForCreateApp);
                }
                catch
                {
                    this.authButton.Enabled = true;
                    MessageBox.Show("連携アプリの登録に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                this.AccountsFile.Apps.Add(app);
                await this.AccountsFile.SaveAsync();
            }

            waitingLabel.Visible = true;

            var clientForAuth = MisskeyAppConversion.FromAppModel(app);
            await MisskeyService.Authorize(clientForAuth);
            var account = MisskeyAccountConversion.ToAccountModel(clientForAuth);

            waitingLabel.Visible = false;

            if (isNewApp || !this.AccountsFile.Accounts.Any(i => i.AccessToken == account.AccessToken && i.Domain == app.Domain))
            {
                this.AccountsFile.Accounts.Add(account);
                await this.AccountsFile.SaveAsync();

                this.Data = account;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
