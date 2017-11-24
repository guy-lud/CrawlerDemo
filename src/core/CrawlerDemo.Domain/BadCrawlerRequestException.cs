using System;

namespace CrawlerDemo.Domain
{
	[Serializable]
	public class BadCrawlerRequestException : Exception
	{
		public BadCrawlerRequestException(string message) : base(message)
		{
		}

		public BadCrawlerRequestException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}