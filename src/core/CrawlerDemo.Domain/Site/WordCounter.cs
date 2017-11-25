namespace CrawlerDemo.Domain.Site
{
	public struct WordCounter
	{
		public WordCounter(string word, int amount)
		{
			Word = word;
			Amount = amount;
		}

		public string Word { get; }
		public int Amount { get; }
	}
}