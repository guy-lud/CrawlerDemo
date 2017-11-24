using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
