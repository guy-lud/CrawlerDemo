using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerDemo.Domain.Crawler
{
	public interface ICrawler
	{
		string CrawlSite(string url);
	}
}
