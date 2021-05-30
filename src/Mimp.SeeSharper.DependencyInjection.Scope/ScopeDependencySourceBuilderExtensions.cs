using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public static class ScopeDependencySourceBuilderExtensions
    {


        public static IDependencySourceBuilder UseScopeVerifier(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, IScopeVerifier> verifierFactory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (verifierFactory is null)
                throw new ArgumentNullException(nameof(verifierFactory));

            return builder.AddSingleton(verifierFactory)
                .Tag(ScopeDependencyProviderExtensions.ScopeVerifierTag);
        }

        public static IDependencySourceBuilder UseScopeVerifier(
            this IDependencySourceBuilder builder
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.UseScopeVerifier(GetDefaultScopeVerifier);
        }

        public static IScopeVerifier GetDefaultScopeVerifier(IDependencyProvider provider)
        {
            return new ScopeVerifier();
        }


        public static IDependencySourceBuilder UseScopeFactory(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, IDependencyScopeFactory> scopeFactoryFactory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (scopeFactoryFactory is null)
                throw new ArgumentNullException(nameof(scopeFactoryFactory));

            return builder.AddSingleton(scopeFactoryFactory)
                .Tag(ScopeDependencyProviderExtensions.ScopeFactoryTag);
        }

        public static IDependencySourceBuilder UseScopeFactory(
            this IDependencySourceBuilder builder
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.UseScopeFactory(GetDefaultScopeFactory);
        }

        public static IDependencyScopeFactory GetDefaultScopeFactory(IDependencyProvider provider)
        {
            return new DependencyScopeFactory();
        }


        public static IDependencySourceBuilder UseScopeProvider(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, IScopeProvider> scopeProviderFactory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (scopeProviderFactory is null)
                throw new ArgumentNullException(nameof(scopeProviderFactory));

            return builder.AddSingleton(scopeProviderFactory)
                .Tag(ScopeDependencyProviderExtensions.ScopeProviderTag);
        }

        public static IDependencySourceBuilder UseScopeProvider(
            this IDependencySourceBuilder builder
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.UseScopeProvider(GetDefaultScopeProvider);
        }

        public static IScopeProvider GetDefaultScopeProvider(IDependencyProvider provider)
        {
            return new AnonymousScopeProvider();
        }


        public static IDependencySourceBuilder UseScope(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, IScopeVerifier> verifierFactory,
            Func<IDependencyProvider, IDependencyScopeFactory> scopeFactoryFactory,
            Func<IDependencyProvider, IScopeProvider> scopeProviderFactory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (verifierFactory is null)
                throw new ArgumentNullException(nameof(verifierFactory));
            if (scopeFactoryFactory is null)
                throw new ArgumentNullException(nameof(scopeFactoryFactory));
            if (scopeProviderFactory is null)
                throw new ArgumentNullException(nameof(scopeProviderFactory));

            return builder
                .UseScopeVerifier(verifierFactory)
                .UseScopeFactory(scopeFactoryFactory)
                .UseScopeProvider(scopeProviderFactory);
        }

        public static IDependencySourceBuilder UseScope(
            this IDependencySourceBuilder builder
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder
                .UseScopeVerifier()
                .UseScopeFactory()
                .UseScopeProvider();
        }



        public static ITagScopeDependencySourceBuilder AddScoped(
            this IDependencySourceBuilder builder,
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, Action<object>, object> factory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (constructible is null)
                throw new ArgumentNullException(nameof(constructible));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            var scope = new ScopeDependencySourceBuilder(builder,
                new ScopeDependencyBuilder(constructible, factory, ScopeDependencyProviderExtensions.UseScopeVerifier)
            );

            builder.AddDependency(() => scope.BuildDependency());

            return scope;
        }

        public static ITagScopeDependencySourceBuilder AddScoped(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, Type, bool> constructible,
            Func<IDependencyProvider, Type, object> instantiate,
            Action<IDependencyProvider, Type, object>? initialize
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (constructible is null)
                throw new ArgumentNullException(nameof(constructible));
            if (instantiate is null)
                throw new ArgumentNullException(nameof(instantiate));

            return builder.AddScoped(
                BaseDependencyFactory.ConstructibleContext(constructible),
                BaseDependencyFactory.Construct(instantiate, initialize)
            );
        }

        public static ITagScopeDependencySourceBuilder AddScoped(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, Type, bool> constructible,
            Func<IDependencyProvider, Type, object> factory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (constructible is null)
                throw new ArgumentNullException(nameof(constructible));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            return builder.AddScoped(constructible, factory, null);
        }


        public static ITagScopeTypeDependencySourceBuilder AddScoped(
            this IDependencySourceBuilder builder,
            Type type,
            Func<IDependencyContext, Type, Action<object>, object> factory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            var scope = new ScopeTypeDependencySourceBuilder(builder,
                new ScopeTypeDependencyBuilder(type, factory, ScopeDependencyProviderExtensions.UseScopeVerifier));

            builder.AddDependency(() => scope.BuildDependency());

            return scope;
        }

        public static ITagScopeTypeDependencySourceBuilder AddScoped(
            this IDependencySourceBuilder builder,
            Type type,
            Func<IDependencyProvider, Type, object> instantiate,
            Action<IDependencyProvider, Type, object>? initialize
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (instantiate is null)
                throw new ArgumentNullException(nameof(instantiate));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddScoped(type, BaseDependencyFactory.Construct(instantiate, initialize));
        }

        public static ITagScopeTypeDependencySourceBuilder AddScoped(
            this IDependencySourceBuilder builder,
            Type type,
            Func<IDependencyProvider, Type, object> factory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddScoped(type, factory, null);
        }


        public static ITagScopeTypeDependencySourceBuilder AddScoped<TDependency>(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, TDependency> factory,
            Action<IDependencyProvider, TDependency>? initialize
        ) where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            return builder.AddScoped(
                typeof(TDependency),
                (provider, _) => factory(provider),
                initialize is null ? null : (provider, _, instance) => initialize(provider, (TDependency)instance)
            );
        }

        public static ITagScopeTypeDependencySourceBuilder AddScoped<TDependency>(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, TDependency> factory
        ) where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            return builder.AddScoped(factory, null);
        }



        public static IDependencySourceBuilder AddScopedSource(this IDependencySourceBuilder builder, Action<IDependencySourceBuilder> buildSource, Func<IDependencyProvider, IDependencyContext, bool> isScope)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (buildSource is null)
                throw new ArgumentNullException(nameof(buildSource));

            return builder.AddSource(() =>
            {
                var builder = new DependencySourceBuilder();
                buildSource(builder);
                return builder.BuildSource().Scoped(isScope);
            });
        }

        public static IDependencySourceBuilder AddScopedSource(this IDependencySourceBuilder builder, Action<IDependencySourceBuilder> buildSource, IEnumerable<object> scopes)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (buildSource is null)
                throw new ArgumentNullException(nameof(buildSource));
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            var isScope = ScopeDependencySource.IsScopes(scopes);

            return builder.AddScopedSource(buildSource, isScope);
        }

        public static IDependencySourceBuilder AddScopedSource(this IDependencySourceBuilder builder, Action<IDependencySourceBuilder> buildSource, object scope, params object[] scopes)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (buildSource is null)
                throw new ArgumentNullException(nameof(buildSource));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return builder.AddScopedSource(buildSource, new[] { scope }.Concat(scopes));
        }


    }
}
