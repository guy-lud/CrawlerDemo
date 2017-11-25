using System.Linq;
using HtmlAgilityPack;

namespace CrawlerDemo.Domain.Site
{
	public interface ISiteParser
	{
		ParsingOutcome ParseSiteContent(string siteString);
	}

	public class SiteParser : ISiteParser
	{
		public ParsingOutcome ParseSiteContent(string siteString)
		{
			var document = new HtmlDocument();
			document.LoadHtml(siteString);
			return new ParsingOutcome(Enumerable.Empty<string>(), Enumerable.Empty<string>());
		}
	}
}
