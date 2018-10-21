namespace MisskeyDriveSync.Models
{
	public class MisskeyApp
	{
		public MisskeyApp(string domain, string clientId, string secret)
		{
			this.Domain = domain;
			this.ClientId = clientId;
			this.Secret = secret;
		}

		public string Domain { get; set; }

		public string ClientId { get; set; }

		public string Secret { get; set; }
	}
}
