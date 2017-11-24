using System;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace CrawlerDemo.Application.Web.Integration.Asp
{
	internal class CompositeServiceProvider : IServiceProvider, ISupportRequiredService
	{
		private readonly IServiceProvider _microsoftServiceProvider;
		private readonly Container _container;

		public CompositeServiceProvider(IServiceProvider microsoftServiceMicrosoftServiceProvider, Container container)
		{
			_microsoftServiceProvider = microsoftServiceMicrosoftServiceProvider;
			_container = container;
		}

		public object GetService(Type serviceType)
		{
			// we want to verify that we'll get our IServiceScopeFactory and not Microsoft's IServiceScopeFactory
			// which does not support both containers.
			if (serviceType == typeof(IServiceScopeFactory))
			{
				return _container.GetInstance(serviceType);
			}

			// First we'll check if MS DI can provide the requested service if not, we'll check SimpleInjector.
			return _microsoftServiceProvider.GetService(serviceType) ?? _container.GetInstance(serviceType);
		}

		public object GetRequiredService(Type serviceType)
		{
			// we want to verify that we'll get our IServiceScopeFactory and not Microsoft's IServiceScopeFactory
			// which does not support both containers.
			if (serviceType == typeof(IServiceScopeFactory))
			{
				return _container.GetInstance(serviceType);
			}

			// First we'll check if MS DI can provide the requested service if not, we'll check SimpleInjector.
			// We don't want to user GetRequiredService because it will throw exception.
			return _microsoftServiceProvider.GetService(serviceType) ?? _container.GetRequiredService(serviceType);
		}
	}
}