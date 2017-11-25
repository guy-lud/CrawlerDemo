using System.Collections.Generic;

namespace CrawlerDemo.Domain.Site
{
	public class ParsingOutcome
	{
		public ParsingOutcome(IEnumerable<string> links, IEnumerable<string> sentences)
		{
			Links = links;
			Sentences = sentences;
		}

		public IEnumerable<string> Links { get; }
		public IEnumerable<string> Sentences { get; }
	}
}