namespace CrawlerDemo.Domain
{
	public interface ICrawlerSiteRequestValidator
	{
		void ValidateCrawlingRequest(SiteInfoRequest infoRequest);
	}
}
