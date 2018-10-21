using System.Threading.Tasks;

namespace MisskeyDriveSync.JsonFiles
{
	public class SettingFile : JsonFile
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
		/// JSONファイルから設定を読み込みます
		/// <para>JSONファイルが存在しないときは新規に生成します</para>
		/// </summary>
		public static async Task<SettingFile> LoadAsync()
		{
			var setting = await LoadAsync<SettingFile>("misskeyDriveSync.setting.json");
			setting._Normalize();

			return setting;
		}

		/// <summary>
		/// JSONファイルに設定を保存します
		/// </summary>
		public Task SaveAsync()
		{
			_Normalize();
			return SaveAsync("misskeyDriveSync.setting.json");
		}

		#endregion Methods

	}
}
