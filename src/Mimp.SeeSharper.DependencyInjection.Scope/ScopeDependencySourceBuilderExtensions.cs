using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public static class ScopeDependencySourceBuilderExtensions
    {


        public static IDependencySourceBuilder UseScopeFactory(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, IScopeFactory> scopeFactory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (scopeFactory is null)
                throw new ArgumentNullException(nameof(scopeFactory));

            return builder.AddSingleton(scopeFactory)
                .Tag(Abstraction.ScopeDependencyProviderExtensions.ScopeFactoryTag);
        }

        public static IDependencySourceBuilder UseScopeFactory(
            this IDependencySourceBuilder builder
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.UseScopeFactory(GetDefaultScopeFactory);
        }

        public static IScopeFactory GetDefaultScopeFactory(IDependencyProvider provider)
        {
            return new ScopeFactory();
        }


        public static IDependencySourceBuilder UseDependencyScopeFactory(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, IDependencyScopeFactory> dependencyScopeFactory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (dependencyScopeFactory is null)
                throw new ArgumentNullException(nameof(dependencyScopeFactory));

            return builder.AddSingleton(dependencyScopeFactory)
                .Tag(Abstraction.ScopeDependencyProviderExtensions.DependencyScopeFactoryTag);
        }

        public static IDependencySourceBuilder UseDependencyScopeFactory(
            this IDependencySourceBuilder builder
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.UseDependencyScopeFactory(GetDefaultDependencyScopeFactory);
        }

        public static IDependencyScopeFactory GetDefaultDependencyScopeFactory(IDependencyProvider provider)
        {
            return new DependencyScopeFactory();
        }


        public static IDependencySourceBuilder UseScopeProvider(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, IScopeProvider> scopeProvider
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (scopeProvider is null)
                throw new ArgumentNullException(nameof(scopeProvider));

            return builder.AddSingleton(scopeProvider)
                .Tag(Abstraction.ScopeDependencyProviderExtensions.ScopeProviderTag);
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
            Func<IDependencyProvider, IScopeFactory> scopeFactory,
            Func<IDependencyProvider, IDependencyScopeFactory> dependencyScopeFactory,
            Func<IDependencyProvider, IScopeProvider> scopeProviderFactory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (scopeFactory is null)
                throw new ArgumentNullException(nameof(scopeFactory));
            if (dependencyScopeFactory is null)
                throw new ArgumentNullException(nameof(dependencyScopeFactory));
            if (scopeProviderFactory is null)
                throw new ArgumentNullException(nameof(scopeProviderFactory));

            return builder
                .UseScopeFactory(scopeFactory)
                .UseDependencyScopeFactory(dependencyScopeFactory)
                .UseScopeProvider(scopeProviderFactory);
        }

        public static IDependencySourceBuilder UseScope(
            this IDependencySourceBuilder builder
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder
                .UseScopeFactory()
                .UseDependencyScopeFactory()
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

            var scope = new TagScopeDependencySourceBuilder(builder,
                new ScopeDependencyBuilder(constructible, factory)
            );

            builder.AddDependency(provider => scope.BuildDependency(provider));

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

            var scope = new TagScopeTypeDependencySourceBuilder(builder,
                new ScopeTypeDependencyBuilder(type, factory));

            builder.AddDependency(provider => scope.BuildDependency(provider));

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


    }
}
