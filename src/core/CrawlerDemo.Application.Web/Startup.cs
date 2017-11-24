using System.Reflection;
using CrawlerDemo.Application.Web.Integration.Mvc;
using CrawlerDemo.DataAccess;
using CrawlerDemo.DataAccess.Crawler;
using CrawlerDemo.Domain;
using CrawlerDemo.Domain.Crawler;
using CrawlerDemo.Domain.Site;
using CrawlerDemo.Frameworks;
using ExistAll.SimpleConfig;
using ExistAll.SimpleConfig.Binders;
using ExistsForAll.Shepherd.SimpleInjector;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace CrawlerDemo.Application.Web
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
				.AddFeatureFolderStructure();
		}

		public void ConfigureContainer(Container container)
		{
			var config = new ConfigBuilder();
			var collection = new AssemblyCollection()
				.AddPublicTypesAssemblies(GetType().GetTypeInfo().Assembly,
				typeof(Ref).Assembly,
				typeof(DomainRef).Assembly);

			config.Add(new ExistAll.SimpleConfig.Binders.ConfigurationBinder(Configuration));

			var settings = config.Build(collection.Assemblies, new ConfigOptions()
			{
				ConfigSuffix = "Settings"
			});

			foreach (var setting in settings)
			{
				container.RegisterSingleton(setting.Key, setting.Value);
			}

			container.RegisterSingleton<ISystemClock,SystemClock>();
			container.RegisterSingleton<ISiteCrawler,HttpSiteCrawler>();
			container.RegisterDecorator<ISiteCrawler, CacheHttpSiteCrawler>();
			container.RegisterSingleton<ISiteRepository, SiteRepository>();
			container.RegisterSingleton<ICrawlerSiteRequestValidator, CrawlerSiteRequestValidator>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler(new ExceptionHandlerOptions()
				{
					ExceptionHandler = async context => await context.Response.WriteAsync("Not good")
				});
			}

			app.UseStaticFiles();

			app.UseMvc();
		}
	}
}
