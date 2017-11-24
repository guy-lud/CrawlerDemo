namespace CrawlerDemo.Domain
{
	public interface ICrawlerSiteRequestValidator
	{
		void ValidateCrawlingRequest(CrawlerSiteRequest request);
	}
}
