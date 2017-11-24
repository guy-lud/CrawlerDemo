namespace CrawlerDemo.Domain.Site
{
	public interface ISiteRepository
	{
		string GetSiteContent(string url);
		void AddSiteContent(string url, string content);
	}
}