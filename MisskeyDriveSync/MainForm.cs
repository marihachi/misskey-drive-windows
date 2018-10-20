using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MisskeyDriveSync
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

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
			refreshVersionInfo();
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

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				processCannel();
				Hide();
			}
		}
	}
}
