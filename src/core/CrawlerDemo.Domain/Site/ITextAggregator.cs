using System.Collections.Generic;

namespace CrawlerDemo.Domain.Site
{
	public interface ITextAggregator
	{
		IEnumerable<WordCounter> AggregateMatchWords(IEnumerable<string> matchWords, IEnumerable<string> sentences);
	}
}