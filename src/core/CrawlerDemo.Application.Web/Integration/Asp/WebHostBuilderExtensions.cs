using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace CrawlerDemo.Application.Web.Integration.Asp
{
    public static class WebHostBuilderExtensions
    {
	    public static IWebHostBuilder UseSimpleInjector(this IWebHostBuilder target, Action<ContainerOptions> action)
	    {
		    target.ConfigureServices(x => x.AddSingleton<IServiceProviderFactory<Container>>(new SimpleInjectorServiceProviderFactory(action)));
		    return target;
	    }

	    public static IWebHostBuilder UseSimpleInjector(this IWebHostBuilder target, Container container)
	    {
		    target.ConfigureServices(x => x.AddSingleton<IServiceProviderFactory<Container>>(new SimpleInjectorServiceProviderFactory(container)));
		    return target;
	    }
	}
}
