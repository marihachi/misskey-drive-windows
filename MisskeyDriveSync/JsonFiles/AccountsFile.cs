using MisskeyDriveSync.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MisskeyDriveSync.JsonFiles
{
	public class AccountsFile : JsonFile
	{

		#region Properties

		public List<MisskeyApp> Instances { get; set; } = new List<MisskeyApp>();

		public List<MisskeyAccount> Accounts { get; set; } = new List<MisskeyAccount>();

		#endregion Properties

		#region Methods

		/// <summary>
		/// 適宜、デフォルト値を設定することによって値の整合性を取ります
		/// </summary>
		private void _Normalize()
		{

		}

		/// <summary>
		/// JSONファイルから設定を読み込みます
		/// <para>JSONファイルが存在しないときは新規に生成します</para>
		/// </summary>
		public static async Task<AccountsFile> LoadAsync()
		{
			var setting = await LoadAsync<AccountsFile>("misskeyDriveSync.accounts.json");
			setting._Normalize();

			return setting;
		}

		/// <summary>
		/// JSONファイルに設定を保存します
		/// </summary>
		public Task SaveAsync()
		{
			_Normalize();
			return SaveAsync("misskeyDriveSync.accounts.json");
		}

		#endregion Methods

	}
}
