using System.Threading.Tasks;

namespace MisskeyDriveSync
{
	public class AccountsFile : JsonFile
	{

		#region Properties

		#endregion Properties

		#region Methods

		/// <summary>
		/// 適宜、デフォルト値を設定することによって値の整合性を取ります
		/// </summary>
		private void _Normalize()
		{

		}

		/// <summary>
		/// settings.json から設定を読み込みます
		/// <para>settings.json が存在しないときは新規に生成します</para>
		/// </summary>
		public static async Task<AccountsFile> LoadAsync()
		{
			var setting = await LoadAsync<AccountsFile>("misskeyDriveSync.accounts.json");
			setting._Normalize();

			return setting;
		}

		/// <summary>
		/// settings.json に設定を保存します
		/// </summary>
		public Task SaveAsync()
		{
			_Normalize();
			return SaveAsync("misskeyDriveSync.accounts.json");
		}

		#endregion Methods

	}
}
