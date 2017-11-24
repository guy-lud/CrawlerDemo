using System;
using System.Runtime.Caching;
using CrawlerDemo.Domain.Site;
using CrawlerDemo.Frameworks;

namespace CrawlerDemo.DataAccess.Crawler
{
	public class SiteRepository : ISiteRepository
	{
		private readonly ISiteRepositoryCacheSettings _siteRepositoryCacheSettings;
		private readonly ISystemClock _systemClock;
		private readonly MemoryCache _cache = MemoryCache.Default;

		public SiteRepository(ISiteRepositoryCacheSettings siteRepositoryCacheSettings, ISystemClock systemClock)
		{
			_siteRepositoryCacheSettings = siteRepositoryCacheSettings;
			_systemClock = systemClock;
		}

		public string GetSiteContent(string url)
		{
			return (string)_cache.Get(GetCacheKey(url));
		}

		public void AddSiteContent(string url, string content)
		{
			var cacheItemPolicy = new CacheItemPolicy();
			var dateTimeOffset = new DateTimeOffset(_systemClock.Now());
			dateTimeOffset.Offset.Add(TimeSpan.FromMinutes(_siteRepositoryCacheSettings.CacheValidityInSeconds));
			cacheItemPolicy.AbsoluteExpiration = dateTimeOffset;

			_cache.Add(GetCacheKey(url), content, cacheItemPolicy);
		}

		private string GetCacheKey(string url)
		{
			return $"{_siteRepositoryCacheSettings.CacheKey}-{url}";
		}
	}
}