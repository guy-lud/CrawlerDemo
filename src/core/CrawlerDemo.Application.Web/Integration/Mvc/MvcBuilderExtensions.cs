using ExistAll.AspNet.FeatureFolderBase;
using Microsoft.Extensions.DependencyInjection;

namespace CrawlerDemo.Application.Web.Integration.Mvc
{
	public static class MvcBuilderExtensions
	{
		public static IMvcBuilder AddFeatureFolderStructure(this IMvcBuilder target)
		{
			target.AddFeatureFolders(new FeatureFolderOptions
			{
				FeatureFolderName = "Application",
				ViewExtractionOption = ViewExtractionOption.Explicits
			});
			return target;
		}
	}
}
