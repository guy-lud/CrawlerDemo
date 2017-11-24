using System.Collections.Generic;

namespace CrawlerDemo.Domain
{
	public class CrawlerSiteRequest
	{
		public string SiteUrl { get; }
		public IEnumerable<string> MatchingWords { get; }
		public int RecursionDepth { get; }

		public CrawlerSiteRequest(string siteUrl, IEnumerable<string> matchingWords, int recursionDepth)
		{
			SiteUrl = siteUrl;
			MatchingWords = matchingWords;
			RecursionDepth = recursionDepth;
		}
	}
}