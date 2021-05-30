#if NETCOREAPP2_1
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Threading.Tasks;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection
{
    public static partial class DependencyServiceCollectionExtensions
    {


        public static IServiceProvider BuildDependencyServiceProvider(
            this IServiceCollection services,
            Func<IDependencySourceBuilder, IDependencySourceBuilder>? configurateBuilder = null,
            Func<IDependencySourceBuilder, IDependencyProvider>? getProvider = null
        )
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.Insert(0, ServiceDescriptor.Transient<IStartupFilter>(provider => new SetFirstHttpContextStartUpFilter()));

            var factory = new DependencyServiceProviderFactory(configurateBuilder ?? (builder => builder), getProvider ?? DependencyServiceProviderFactory.GetDefaultProvider);
            return factory.CreateServiceProvider(factory.CreateBuilder(services));
        }


        internal class SetFirstHttpContextMiddleware
        {


            private readonly IHttpContextAccessor _contextAccessor;

            private readonly RequestDelegate _next;


            public SetFirstHttpContextMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor)
            {
                _next = next;
                _contextAccessor = contextAccessor;
            }


            public async Task Invoke(HttpContext context)
            {
                // If there isn't already an HttpContext set on the context
                // accessor for this async/thread operation, set it. 
                // This allows scope provider to use it.
                if (_contextAccessor.HttpContext is null)
                    _contextAccessor.HttpContext = context;

                await _next.Invoke(context);
            }


        }


        internal class SetFirstHttpContextStartUpFilter : IStartupFilter
        {


            public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            {
                return builder =>
                {
                    builder.UseMiddleware<SetFirstHttpContextMiddleware>();
                    next(builder);
                };
            }


        }


    }
}
#endif