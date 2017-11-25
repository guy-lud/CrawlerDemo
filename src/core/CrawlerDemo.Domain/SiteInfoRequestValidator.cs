using System;
using System.Linq;
using CrawlerDemo.Frameworks;

namespace CrawlerDemo.Domain
{
	public class SiteInfoRequestValidator : ICrawlerSiteRequestValidator
	{
		public void ValidateCrawlingRequest(SiteInfoRequest infoRequest)
		{
			if (infoRequest == null)
				throw new BadSiteInfoRequestException("crawling infoRequest is null");

			if (infoRequest.SiteUrl.IsNullOrWhiteSpaces())
				throw new BadSiteInfoRequestException("site url is null or empty");

			try
			{
				Uri uri = new Uri(infoRequest.SiteUrl);
			}
			catch (Exception e)
			{
				throw new BadSiteInfoRequestException($"site url [{infoRequest.SiteUrl}] is not valid format", e);
			}

			if (infoRequest.MatchingWords == null || !infoRequest.MatchingWords.Any())
				throw new BadSiteInfoRequestException($"No matching words were provided");

			if (infoRequest.RecursionDepth < 0)
				throw new BadSiteInfoRequestException(
					$"RecursionDepth can not be lower than 0. RecursionDepth=[{infoRequest.RecursionDepth}]");
		}
	}
}