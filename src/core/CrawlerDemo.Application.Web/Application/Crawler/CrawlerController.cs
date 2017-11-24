using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace CrawlerDemo.Application.Web.Application.Crawler
{
	[Route("api")]
	public class CrawlerController : Controller
	{
		[HttpGet("/crawl")]
		public Object ExtractInfoFromWebSite([NotNull] string url, IEnumerable<string> matchWords, int recursionDepth)
		{



			return null;
		}

	}
}
