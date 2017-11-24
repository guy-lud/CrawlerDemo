using System;
using System.Collections.Generic;
using CrawlerDemo.Domain;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace CrawlerDemo.Application.Web.Application.Crawler
{
	[Route("api")]
	public class CrawlerController : Controller
	{
		private readonly ICrawlerSiteRequestValidator _crawlerSiteRequestValidator;

		public CrawlerController(ICrawlerSiteRequestValidator crawlerSiteRequestValidator)
		{
			_crawlerSiteRequestValidator = crawlerSiteRequestValidator;
		}

		[HttpGet("crawl")]
		public Object ExtractInfoFromWebSite([NotNull] string url, IEnumerable<string> matchWords, int recursionDepth)
		{
			var request = new CrawlerSiteRequest(url, matchWords, recursionDepth);
			_crawlerSiteRequestValidator.ValidateCrawlingRequest(request);

			return request;
		}

		[HttpGet("test")]
		public string Test()
		{
			
			return "ok";
		}

	}
}
