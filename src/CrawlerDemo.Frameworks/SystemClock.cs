using System;

namespace CrawlerDemo.Frameworks
{
	public class SystemClock : ISystemClock
	{
		public DateTime Now()
		{
			return DateTime.UtcNow;
		}
	}
}