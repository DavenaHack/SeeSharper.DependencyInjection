using Microsoft.Extensions.Configuration;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Extensions.Configuration.Abstraction;
using Mimp.SeeSharper.TypeProvider;
using Mimp.SeeSharper.TypeProvider.Abstraction;
using Mimp.SeeSharper.TypeResolver;
using Mimp.SeeSharper.TypeResolver.Abstraction;
using Mimp.SeeSharper.TypeResolver.Provide;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.Configuration
{
    public static class ConfigureDependencySourceBuilderExtensions
    {


        private static readonly IConfigurationDependencyFactoryBuilder _DefaultFactoryBuilder;

        private static readonly IConfigurationDependencySourceBuilder _DefaultSourceBuilder;

        static ConfigureDependencySourceBuilderExtensions()
        {
            var typeResolver = new ProvidedTypeResolver(new MultipleAssemblyTypeProvider(new IAssemblyTypeProvider[] {
                new UsedAssemblyTypeProvider(),
                new EntryAssemblyTypeProvider(),
                new ExecutingAssemblyTypeProvider()
            }));
            _DefaultFactoryBuilder = new ConfigurationDependencyFactoryBuilder(typeResolver.ResolveRequired);
            _DefaultSourceBuilder = new ConfigurationDependencySourceBuilder(_DefaultFactoryBuilder);
        }


        public static IDependencySourceBuilder AddConfigureDependency(
            this IDependencySourceBuilder builder,
            IConfigurationDependencyFactoryBuilder dependencyBuilder,
            IConfiguration configuration
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (dependencyBuilder is null)
                throw new ArgumentNullException(nameof(dependencyBuilder));
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            return builder.AddDependency(() => dependencyBuilder.GetFactory(configuration, configuration));
        }

        public static IDependencySourceBuilder AddConfigureDependency(
            this IDependencySourceBuilder builder,
            IConfiguration configuration
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            return builder.AddConfigureDependency(_DefaultFactoryBuilder, configuration);
        }


        public static IDependencySourceBuilder AddConfigureSource(
            this IDependencySourceBuilder builder,
            IConfigurationDependencySourceBuilder dependencyBuilder,
            IConfiguration configuration
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (dependencyBuilder is null)
                throw new ArgumentNullException(nameof(dependencyBuilder));
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            return builder.AddSource(() => dependencyBuilder.GetSource(configuration, configuration));
        }

        public static IDependencySourceBuilder AddConfigureSource(
            this IDependencySourceBuilder builder,
            IConfiguration configuration
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            return builder.AddConfigureSource(_DefaultSourceBuilder, configuration);
        }


    }
}
