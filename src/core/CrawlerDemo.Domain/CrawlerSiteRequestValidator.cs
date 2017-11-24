using System;
using System.Linq;
using CrawlerDemo.Frameworks;

namespace CrawlerDemo.Domain
{
	public class CrawlerSiteRequestValidator : ICrawlerSiteRequestValidator
	{
		public void ValidateCrawlingRequest(CrawlerSiteRequest request)
		{
			if(request == null)
				throw new BadCrawlerRequestException("crawling request is null");

			if(request.SiteUrl.IsNullOrWhiteSpaces())
				throw new BadCrawlerRequestException("site url is null or empty");

			try
			{
				Uri uri = new Uri(request.SiteUrl);
			}
			catch (Exception e)
			{
				throw new BadCrawlerRequestException($"site url [{request.SiteUrl}] is not valid format", e);
			}

			if(request.MatchingWords == null || !request.MatchingWords.Any())
				throw new BadCrawlerRequestException($"No matching words were provided");

			if(request.RecursionDepth < 0)
				throw new BadCrawlerRequestException($"RecursionDepth can not be lower than 0. RecursionDepth=[{request.RecursionDepth}]");
		}
	}
}