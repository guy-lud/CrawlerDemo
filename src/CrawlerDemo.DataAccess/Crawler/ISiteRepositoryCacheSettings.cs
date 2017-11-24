namespace CrawlerDemo.DataAccess.Crawler
{
	public interface ISiteRepositoryCacheSettings
	{
		int CacheValidityInSeconds { get; set; }
		string CacheKey { get; set; }
	}
}