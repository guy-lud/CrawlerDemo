using CrawlerDemo.Application.Web.Integration.Mvc;
using CrawlerDemo.Domain;
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

			app.UseMvc(routes =>
			{
			
			});
		}
	}
}
