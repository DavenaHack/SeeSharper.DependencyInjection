using Microsoft.Extensions.Configuration;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Extensions.Configuration.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.TypeProvider;
using Mimp.SeeSharper.TypeProvider.Abstraction;
using Mimp.SeeSharper.TypeResolver.Abstraction;
using Mimp.SeeSharper.TypeResolver.TypeProvider;
using System;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.Configuration
{
    public static class ConfigureDependencySourceBuilderExtensions
    {


        private static readonly IConfigurationDependencyFactoryBuilder _DefaultFactoryBuilder;

        private static readonly IConfigurationDependencySourceBuilder _DefaultSourceBuilder;

        static ConfigureDependencySourceBuilderExtensions()
        {
            var typeResolver = new ProvidingTypeResolver(new MultipleAssemblyTypeProvider(new IAssemblyTypeProvider[] {
                new UsedAssemblyTypeProvider(),
                new EntryAssemblyTypeProvider(),
                new ExecutingAssemblyTypeProvider()
            }));
            _DefaultFactoryBuilder = new ConfigurationDependencyFactoryBuilder(typeResolver.ResolveSingle, ResolveScope);
            _DefaultSourceBuilder = new ConfigurationDependencySourceBuilder(_DefaultFactoryBuilder, ResolveScope);
        }


        private static IScope? ResolveScope(IDependencyProvider provider, IConfigurationSection section)
        {
            if (section.GetChildren().Any())
                throw new ArgumentException($"{section.Path} have to be a value to parse scope");

            var value = section.Value;
            if (value is null)
                return null;

            try
            {
                return Scopes.CreateScopeLeftRightWithPriority(provider.GetScopeFactory(), value);

            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"{section.Path} is invalid scope: {value}", ex);
            }
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

            return builder.AddDependency(provider => dependencyBuilder.GetFactory(provider, configuration, configuration));
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

            return builder.AddSource(provider => dependencyBuilder.GetSource(provider, configuration, configuration));
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
