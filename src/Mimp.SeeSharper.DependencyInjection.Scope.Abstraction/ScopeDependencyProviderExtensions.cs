using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class ScopeDependencyProviderExtensions
    {


        public static object ScopeFactoryTag { get; } = nameof(ScopeFactoryTag);

        public static object DependencyScopeFactoryTag { get; } = nameof(DependencyScopeFactoryTag);

        public static object ScopeProviderTag { get; } = nameof(ScopeProviderTag);


        public static IScopeFactory GetScopeFactory(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.GetDependencyRequired<IScopeFactory>(ScopeFactoryTag);
        }

        public static void UseScopeFactory(this IDependencyProvider provider, Action<IScopeFactory> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.Use(ScopeFactoryTag, use);
        }

        public static R UseScopeFactory<R>(this IDependencyProvider provider, Func<IScopeFactory, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return provider.Use(ScopeFactoryTag, use);
        }


        public static IDependencyScopeFactory GetDependencyScopeFactory(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.GetDependencyRequired<IDependencyScopeFactory>(DependencyScopeFactoryTag);
        }

        public static void UseDependencyScopeFactory(this IDependencyProvider provider, Action<IDependencyScopeFactory> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.Use(DependencyScopeFactoryTag, use);
        }

        public static R UseDependencyScopeFactory<R>(this IDependencyProvider provider, Func<IDependencyScopeFactory, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return provider.Use(DependencyScopeFactoryTag, use);
        }


        public static IScopeProvider GetScopeProvider(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.GetDependencyRequired<IScopeProvider>(ScopeProviderTag);
        }

        public static void UseScopeProvider(this IDependencyProvider provider, Action<IScopeProvider> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.Use(ScopeProviderTag, use);
        }

        public static R UseScopeProvider<R>(this IDependencyProvider provider, Func<IScopeProvider, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return provider.Use(ScopeProviderTag, use);
        }


        public static IScope CreateScope(this IDependencyProvider provider, object? scope)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.UseScopeFactory(factory => factory.CreateScope(scope));
        }


        public static IDependencyScope CreateDependencyScope(this IDependencyProvider provider, Func<IDependencyScope, IScope> scope)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return provider.UseDependencyScopeFactory(factory => factory.CreateDependencyScope(scope, provider));
        }

        public static IDependencyScope CreateDependencyScope(this IDependencyProvider provider, IScope scope)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return provider.UseDependencyScopeFactory(factory => factory.CreateDependencyScope(scope, provider));
        }

        public static IDependencyScope CreateDependencyScope(this IDependencyProvider provider, Func<IDependencyScope, object?> scope)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return provider.CreateDependencyScope(dependencyScope => provider.CreateScope(scope(dependencyScope)));
        }

        public static IDependencyScope CreateDependencyScope(this IDependencyProvider provider, object? scope)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return provider.UseDependencyScopeFactory(factory => factory.CreateDependencyScope(provider.CreateScope(scope), provider));
        }

        public static IDependencyScope CreateDependencyScope(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.CreateDependencyScope(scope => provider.UseScopeProvider(scopeProvider => scopeProvider.GetScope(scope)));
        }


    }
}
