using Disboard.Exceptions;
using Disboard.Misskey;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MisskeyDriveSync.Services
{
	public static class MisskeyService
	{
		public static async Task CreateApp(MisskeyClient client)
		{
			var app = await client.App.CreateAsync(
				"MisskeyDriveSync",
				"MisskeyDrive sync app for Windows Desktop.",
				new[] { "drive-read", "drive-write" },
				null);

			client.ClientId = app.Id;
		}

		public static async Task<Disboard.Misskey.Models.User> Authorize(MisskeyClient client)
		{
			if (client.ClientSecret == null)
				throw new ApplicationException("ClientSecret is empty");

			var session = await client.Auth.Session.GenerateAsync();
			System.Diagnostics.Process.Start(session.Url);

			// polling user-key
			while (true)
			{
				try
				{
					var credential = await client.Auth.Session.UserKeyAsync(session.Token);
					if (credential.AccessToken != null)
					{
						return credential.User;
					}
				}
				catch (DisboardException ex)
				{
					if (ex.StatusCode != HttpStatusCode.BadRequest || ex.Message != "this session is not allowed yet")
					{
						throw;
					}
				}
				await Task.Delay(1000);
			}
		}
	}
}
