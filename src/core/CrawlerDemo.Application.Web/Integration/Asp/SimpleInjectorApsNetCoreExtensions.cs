using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Diagnostics;

namespace CrawlerDemo.Application.Web.Integration.Asp
{
	internal static class SimpleInjectorApsNetCoreExtensions
	{
		public static void ConfigureAutoCrossWiring(this Container container,
			IServiceProvider microsoftServiceProvider,
			IServiceCollection services)
		{
			container.ResolveUnregisteredType += (s, e) =>
			{
				if (e.Handled) return;

				var serviceType = e.UnregisteredServiceType;

				var descriptor = services.LastOrDefault(d => d.ServiceType == serviceType);

				// not supporting IEnumerable<> and such (for now)
				if (descriptor == null && serviceType.GetTypeInfo().IsGenericType)
				{
					var serviceTypeDefinition = serviceType.GetTypeInfo().GetGenericTypeDefinition();
					descriptor = services.LastOrDefault(d => d.ServiceType == serviceTypeDefinition);
				}

				if (descriptor == null)
					return;

				var lifestyle =
					descriptor.Lifetime == ServiceLifetime.Singleton ? Lifestyle.Singleton :
						descriptor.Lifetime == ServiceLifetime.Scoped ? Lifestyle.Scoped :
							Lifestyle.Transient;

				var registration = lifestyle.CreateRegistration(serviceType, () =>
					{
						// we might get an exception here since the IServiceProvider is the composite one
						// we use this serviceProvider so if it's scoped we'll get the one from the asp.net child container.
						var service = microsoftServiceProvider.GetService<IHttpContextAccessor>()
						.HttpContext
						.RequestServices
						.GetService(serviceType);

						return service;
					},
					container);

				if (lifestyle == Lifestyle.Transient && typeof(IDisposable).IsAssignableFrom(serviceType))
				{
					registration.SuppressDiagnosticWarning(
						DiagnosticType.DisposableTransientComponent,
						justification:
						"This is a cross-wired service. ASP.NET will ensure it gets disposed.");
				}

				e.Register(registration);
			};
		}
	}
}