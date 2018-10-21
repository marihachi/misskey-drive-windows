namespace MisskeyDriveSync.Models
{
	public class MisskeyAccount
	{
		public MisskeyAccount(string domain, string clientId, string accessToken, string username, string id)
		{
			this.Domain = domain;
			this.ClientId = clientId;
			this.AccessToken = accessToken;
			this.Username = username;
			this.Id = id;
		}

		public string Domain { get; set; }

		public string ClientId { get; set; }

		public string AccessToken { get; set; }

		public string Username { get; set; }

		public string Id { get; set; }
	}
}
