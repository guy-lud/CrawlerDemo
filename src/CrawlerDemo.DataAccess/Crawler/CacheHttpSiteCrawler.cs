using System.Threading.Tasks;
using CrawlerDemo.Domain.Crawler;
using CrawlerDemo.Domain.Site;

namespace CrawlerDemo.DataAccess.Crawler
{
	public class CacheHttpSiteCrawler : ISiteCrawler
	{
		private readonly ISiteCrawler _crawler;
		private readonly ISiteRepository _siteRepository;

		public CacheHttpSiteCrawler(ISiteCrawler crawler, ISiteRepository siteRepository)
		{
			_crawler = crawler;
			_siteRepository = siteRepository;
		}

		public async Task<string> DownloadSiteAsStringAsync(string url)
		{
			var siteContent = _siteRepository.GetSiteContent(url);

			if (siteContent != null)
				return await Task.FromResult(siteContent);

			siteContent = await _crawler.DownloadSiteAsStringAsync(url);

			_siteRepository.AddSiteContent(url, siteContent);

			return siteContent;
		}
	}
}