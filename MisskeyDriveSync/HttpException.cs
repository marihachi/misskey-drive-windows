using System;
using System.Net;

namespace MisskeyDriveSync
{
	public class HttpException : Exception
	{
		public HttpException(HttpStatusCode statusCode) : base()
		{
			this.StatusCode = statusCode;
		}

		public HttpException(HttpStatusCode statusCode, Exception innerException) : base(null, innerException)
		{
			this.StatusCode = statusCode;
		}

		public HttpStatusCode StatusCode { get; }
	}
}
