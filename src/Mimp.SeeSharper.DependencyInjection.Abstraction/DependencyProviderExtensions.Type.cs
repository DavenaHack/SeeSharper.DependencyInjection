using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public static partial class DependencyProviderExtensions
    {


        public static IDependency? Provide(this IDependencyProvider provider, Type type)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return provider.Provide(new DependencyContext(provider, type));
        }

        public static IDependency? Provide<TDependency>(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.Provide(typeof(TDependency));
        }

        public static TDependency? GetDependency<TDependency>(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return (TDependency?)provider.Provide<TDependency>()?.Dependency;
        }


        public static IDependency ProvideRequired(this IDependencyProvider provider, Type type)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return provider.ProvideRequired(new DependencyContext(provider, type));
        }

        public static IDependency ProvideRequired<TDependency>(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.ProvideRequired(typeof(TDependency));
        }

        public static TDependency GetDependencyRequired<TDependency>(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return (TDependency)provider.ProvideRequired<TDependency>().Dependency;
        }



        public static void Use(this IDependencyProvider provider, Type type, Action<object> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.Use(new DependencyContext(provider, type), use);
        }

        public static R Use<R>(this IDependencyProvider provider, Type type, Func<object, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return provider.Use(new DependencyContext(provider, type), use);
        }


        public static void UseOr(this IDependencyProvider provider, Type type, Action<object> use, Func<object> @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            provider.UseOr(new DependencyContext(provider, type), use, @default);
        }

        public static void UseOr(this IDependencyProvider provider, Type type, Action<object> use, object @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            provider.UseOr(new DependencyContext(provider, type), use, @default);
        }


        public static R UseOr<R>(this IDependencyProvider provider, Type type, Func<object, R> use, Func<object> @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            return provider.UseOr(new DependencyContext(provider, type), use, @default);
        }

        public static R UseOr<R>(this IDependencyProvider provider, Type type, Func<object, R> use, object @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            return provider.UseOr(new DependencyContext(provider, type), use, @default);
        }


        public static void UseElse(this IDependencyProvider provider, Type type, Action<object> use, Action @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            provider.UseElse(new DependencyContext(provider, type), use, @else);
        }

        public static void UseElse(this IDependencyProvider provider, Type type, Action<object> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.UseElse(new DependencyContext(provider, type), use);
        }


        public static R UseElse<R>(this IDependencyProvider provider, Type type, Func<object, R> use, Func<R> @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            return provider.UseElse(new DependencyContext(provider, type), use, @else);
        }

        public static R UseElse<R>(this IDependencyProvider provider, Type type, Func<object, R> use, R @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            return provider.UseElse(new DependencyContext(provider, type), use, @else);
        }



        public static void Use<TDependency>(this IDependencyProvider provider, Action<TDependency> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.Use(typeof(TDependency), d => use((TDependency)d));
        }

        public static R Use<TDependency, R>(this IDependencyProvider provider, Func<TDependency, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return provider.Use(typeof(TDependency), d => use((TDependency)d));
        }


        public static void UseOr<TDependency>(this IDependencyProvider provider, Action<TDependency> use, Func<TDependency> @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            provider.UseOr(typeof(TDependency), d => use((TDependency)d), @default);
        }

        public static void UseOr<TDependency>(this IDependencyProvider provider, Action<TDependency> use, TDependency @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            provider.UseOr(typeof(TDependency), d => use((TDependency)d), @default);
        }


        public static R UseOr<TDependency, R>(this IDependencyProvider provider, Func<TDependency, R> use, Func<TDependency> @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            return provider.UseOr(typeof(TDependency), d => use((TDependency)d), @default);
        }

        public static R UseOr<TDependency, R>(this IDependencyProvider provider, Func<TDependency, R> use, TDependency @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            return provider.UseOr(typeof(TDependency), d => use((TDependency)d), @default);
        }


        public static void UseElse<TDependency>(this IDependencyProvider provider, Action<TDependency> use, Action @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            provider.UseElse(typeof(TDependency), d => use((TDependency)d), @else);
        }

        public static void UseElse<TDependency>(this IDependencyProvider provider, Action<TDependency> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.UseElse(typeof(TDependency), d => use((TDependency)d));
        }


        public static R UseElse<TDependency, R>(this IDependencyProvider provider, Func<TDependency, R> use, Func<R> @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            return provider.UseElse(typeof(TDependency), d => use((TDependency)d), @else);
        }

        public static R UseElse<TDependency, R>(this IDependencyProvider provider, Func<TDependency, R> use, R @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            return provider.UseElse(typeof(TDependency), d => use((TDependency)d), @else);
        }


    }
}
