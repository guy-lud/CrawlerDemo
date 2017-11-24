namespace CrawlerDemo.Frameworks
{
	public static class StringExtensions
	{
		public static bool IsNullOrWhiteSpaces(this string target)
		{
			return string.IsNullOrWhiteSpace(target);
		}
	}
}
