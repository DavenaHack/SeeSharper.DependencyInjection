using Microsoft.Extensions.DependencyInjection;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Enumerable;
using Mimp.SeeSharper.DependencyInjection.Instantiation;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using Mimp.SeeSharper.DependencyInjection.Tag;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Transient;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection
{
    public class DependencyServiceProviderFactory : IServiceProviderFactory<IDependencySourceBuilder>
    {


        private readonly Func<IDependencySourceBuilder, IDependencySourceBuilder> _configurateBuilder;

        private readonly Func<IDependencySourceBuilder, IDependencyProvider> _getProvider;


        public DependencyServiceProviderFactory(
            Func<IDependencySourceBuilder, IDependencySourceBuilder> configurateBuilder,
            Func<IDependencySourceBuilder, IDependencyProvider> getProvider
        )
        {
            if (configurateBuilder is null)
                throw new ArgumentNullException(nameof(configurateBuilder));
            if (getProvider is null)
                throw new ArgumentNullException(nameof(getProvider));
            _configurateBuilder = configurateBuilder;
            _getProvider = getProvider;
        }

        public DependencyServiceProviderFactory(
            Func<IDependencySourceBuilder, IDependencySourceBuilder> configurateBuilder
        ) : this(configurateBuilder, GetDefaultProvider) { }

        public DependencyServiceProviderFactory(
            Func<IDependencySourceBuilder, IDependencyProvider> getProvider
        ) : this(builder => builder, getProvider) { }

        public DependencyServiceProviderFactory()
            : this(GetDefaultProvider) { }



        public IDependencySourceBuilder CreateBuilder(IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            IDependencySourceBuilder builder = new DependencySourceBuilder();

            static Func<IDependencyProvider, Type, object> WrapFactory(Func<IServiceProvider, object> factory) =>
                (dependencyProvider, _) => dependencyProvider.Use<IServiceProvider, object>(provider => factory(provider));

            foreach (var service in services)
            {
                var type = service.ImplementationType ?? service.ServiceType;

                switch (service.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        if (service.ImplementationFactory is not null)
                            builder.AddSingleton(type, WrapFactory(service.ImplementationFactory))
                                .As(service.ServiceType);
                        else if (service.ImplementationInstance is not null)
                            builder.AddSingleton(type, (_, _) => service.ImplementationInstance)
                                .As(service.ServiceType);
                        else
                            builder.AddSingleton(type)
                                .As(service.ServiceType);
                        break;
                    case ServiceLifetime.Scoped:
                        if (service.ImplementationFactory is not null)
                            builder.AddScoped(type, WrapFactory(service.ImplementationFactory))
                                .As(service.ServiceType);
                        else if (service.ImplementationInstance is not null)
                            builder.AddScoped(type, (_, _) => service.ImplementationInstance)
                                .As(service.ServiceType);
                        else
                            builder.AddScoped(type)
                                .As(service.ServiceType);
                        break;
                    case ServiceLifetime.Transient:
                        if (service.ImplementationFactory is not null)
                            builder.AddTransient(type, WrapFactory(service.ImplementationFactory))
                                .As(service.ServiceType);
                        else if (service.ImplementationInstance is not null)
                            builder.AddTransient(type, (IDependencyContext _, Type _) => service.ImplementationInstance)
                                .As(service.ServiceType);
                        else
                            builder.AddTransient(type)
                                .As(service.ServiceType);
                        break;
                    default:
                        throw new NotSupportedException($"Lifetime {service.Lifetime} isn't supported");
                }
            }

            builder = _configurateBuilder(builder);

            return builder;
        }

        public IServiceProvider CreateServiceProvider(IDependencySourceBuilder containerBuilder)
        {
            if (containerBuilder is null)
                throw new ArgumentNullException(nameof(containerBuilder));

            return new DependencyServiceProvider(_getProvider(containerBuilder));
        }


        public static IDependencyProvider GetDefaultProvider(IDependencySourceBuilder builder)
        {
            var sourceBuilder = new DependencySourceBuilder();
            sourceBuilder.UseTagVerifier();
            sourceBuilder.UseScope();
            sourceBuilder.UseInstantiator();

            sourceBuilder.AddScoped(provider => provider);
            sourceBuilder.AddScoped<DependencyServiceProvider>()
                .As<IServiceProvider>();

            sourceBuilder.AddTransient<DependencyServiceScopeFactory>()
                .As<IServiceScopeFactory>();

            var source = sourceBuilder.BuildSource(new EmptyDependencyProvider());

            return new DependencyProvider(
                builder,
                source,
                new FallbackEnumerableDependencyMatcher(
                    new DependencyMatcher()
                        .Intersect(new TagDependencyMatcher())
                        .Intersect(new ScopeDependencyMatcher())),
                new LastDependencySelector(),
                new DependencyInvoker()
            );
        }


    }
}
