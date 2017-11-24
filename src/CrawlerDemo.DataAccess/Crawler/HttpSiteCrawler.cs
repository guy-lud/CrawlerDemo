using System.Net.Http;
using System.Threading.Tasks;
using CrawlerDemo.Domain.Crawler;

namespace CrawlerDemo.DataAccess.Crawler
{
	public class HttpSiteCrawler : ISiteCrawler
	{
		private readonly HttpClient _client = new HttpClient();

		public async Task<string> DownloadSiteAsStringAsync(string url)
		{
			return await _client.GetStringAsync(url);
		}
	}
}