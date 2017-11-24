using System.IO;
using CrawlerDemo.Application.Web.Integration.Asp;
using Microsoft.AspNetCore.Hosting;
using SimpleInjector.Lifestyles;

namespace CrawlerDemo.Application.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseSimpleInjector(o => o.DefaultScopedLifestyle = new AsyncScopedLifestyle())
				.UseStartup<Startup>()
				.UseApplicationInsights()
				.Build();

			host.Run();
		}
	}
}
