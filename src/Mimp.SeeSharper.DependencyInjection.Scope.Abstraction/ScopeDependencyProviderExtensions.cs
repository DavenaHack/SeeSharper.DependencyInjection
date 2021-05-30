using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class ScopeDependencyProviderExtensions
    {


        public static object ScopeVerifierTag { get; } = nameof(ScopeVerifierTag);

        public static object ScopeFactoryTag { get; } = nameof(ScopeFactoryTag);

        public static object ScopeProviderTag { get; } = nameof(ScopeProviderTag);


        public static void UseScopeVerifier(this IDependencyProvider provider, Action<IScopeVerifier> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.Use(ScopeVerifierTag, use);
        }

        public static R UseScopeVerifier<R>(this IDependencyProvider provider, Func<IScopeVerifier, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return provider.Use(ScopeVerifierTag, use);
        }


        public static void UseScopeFactory(this IDependencyProvider provider, Action<IDependencyScopeFactory> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.Use(ScopeFactoryTag, use);
        }

        public static R UseScopeFactory<R>(this IDependencyProvider provider, Func<IDependencyScopeFactory, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return provider.Use(ScopeFactoryTag, use);
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



        public static IDependencyScope CreateScope(this IDependencyProvider provider, Func<IDependencyScope, object> scope)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return provider.UseScopeFactory(factory => factory.CreateScope(scope, provider));
        }

        public static IDependencyScope CreateScope(this IDependencyProvider provider, object scope)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return provider.UseScopeFactory(factory => factory.CreateScope(scope, provider));
        }

        public static IDependencyScope CreateScope(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.CreateScope(scope => provider.UseScopeProvider(scopeProvider => scopeProvider.GetScope(scope)));
        }


    }
}
