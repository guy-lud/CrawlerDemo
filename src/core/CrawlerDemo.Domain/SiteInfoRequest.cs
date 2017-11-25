using System.Collections.Generic;

namespace CrawlerDemo.Domain
{
	public class SiteInfoRequest
	{
		public string SiteUrl { get; }
		public IEnumerable<string> MatchingWords { get; }
		public int RecursionDepth { get; }

		public SiteInfoRequest(string siteUrl, IEnumerable<string> matchingWords, int recursionDepth)
		{
			SiteUrl = siteUrl;
			MatchingWords = matchingWords;
			RecursionDepth = recursionDepth;
		}
	}
}