using Disboard.Misskey;
using MisskeyDriveSync.Models;

namespace MisskeyDriveSync.Services
{
	public static class MisskeyAppConversion
	{
		public static MisskeyApp ToAppModel(MisskeyClient client)
		{
			return new MisskeyApp(client.Domain, client.ClientId, client.ClientSecret);
		}

		public static MisskeyClient FromAppModel(MisskeyApp app)
		{
			return new MisskeyClient(app.Domain, app.Secret) { ClientId = app.ClientId };
		}
	}
}
