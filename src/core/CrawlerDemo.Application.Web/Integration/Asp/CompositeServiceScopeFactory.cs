using System;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace CrawlerDemo.Application.Web.Integration.Asp
{
	internal sealed class CompositeServiceScopeFactory : IServiceScopeFactory
	{
		private readonly Container _container;
		private readonly IServiceScopeFactory _defaultServiceScopeFactory;

		public CompositeServiceScopeFactory(Container container, IServiceScopeFactory defaultServiceScopeFactory)
		{
			_container = container;
			_defaultServiceScopeFactory = defaultServiceScopeFactory;
		}

		public IServiceScope CreateScope()
		{
			return new CompositeServiceScope(_container, _defaultServiceScopeFactory.CreateScope());
		}

		private class CompositeServiceScope : IServiceScope
		{
			private readonly IServiceScope _defaultServiceScope;

			public CompositeServiceScope(Container container, IServiceScope defaultServiceScope)
			{
				// for scoping we want to provide MS child container with simple injector.
				ServiceProvider = new CompositeServiceProvider(defaultServiceScope.ServiceProvider, container);
				_defaultServiceScope = defaultServiceScope;
			}

			public IServiceProvider ServiceProvider { get; }

			public void Dispose()
			{
				_defaultServiceScope.Dispose();
			}
		}
	}
}