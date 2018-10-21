using Disboard.Misskey;
using MisskeyDriveSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MisskeyDriveSync.Services
{
	public static class MisskeyAccountConversion
	{
		public static MisskeyAccount ToAccountModel(MisskeyClient client, string username = null, string userId = null)
		{
			return new MisskeyAccount(client.Domain, client.ClientId, client.AccessToken, username, userId);
		}

		public static MisskeyClient FromAccountModel(MisskeyAccount account, List<MisskeyApp> apps)
		{
			var app = apps.FirstOrDefault(i => i.Domain == account.Domain);
			if (app == null)
				throw new ApplicationException("app not found");

			var client = MisskeyAppConversion.FromAppModel(app);
			client.AccessToken = account.AccessToken;

			return client;
		}
	}
}
