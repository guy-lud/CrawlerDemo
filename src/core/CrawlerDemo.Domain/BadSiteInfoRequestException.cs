using System;

namespace CrawlerDemo.Domain
{
	[Serializable]
	public class BadSiteInfoRequestException : Exception
	{
		public BadSiteInfoRequestException(string message) : base(message)
		{
		}

		public BadSiteInfoRequestException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}