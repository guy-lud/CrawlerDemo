using System.Threading.Tasks;

namespace CrawlerDemo.Domain.Crawler
{
	public interface ISiteCrawler
	{
		Task<string> DownloadSiteAsStringAsync(string url);
	}
}