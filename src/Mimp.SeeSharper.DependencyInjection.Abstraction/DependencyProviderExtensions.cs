using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public static partial class DependencyProviderExtensions
    {


        public static IDependency ProvideRequired(this IDependencyProvider provider, IDependencyContext context)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            return provider.Provide(context) ?? throw new InvalidOperationException($"There is no dependency of type {context.DependencyType} and match {context}");
        }


        public static void Use(this IDependencyProvider provider, IDependencyContext context, Action<object> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.ProvideRequired(context).Use(use);
        }

        public static R Use<R>(this IDependencyProvider provider, IDependencyContext context, Func<object, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return provider.ProvideRequired(context).Use(use);
        }


        public static void UseOr(this IDependencyProvider provider, IDependencyContext context, Action<object> use, Func<object> @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            var dependency = provider.Provide(context);
            if (dependency is null)
                use(@default() ?? throw new ArgumentException($"{nameof(@default)} returns null.", nameof(@default)));
            else
                dependency.Use(use);
        }

        public static void UseOr(this IDependencyProvider provider, IDependencyContext context, Action<object> use, object @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            provider.UseOr(context, use, () => @default);
        }


        public static R UseOr<R>(this IDependencyProvider provider, IDependencyContext context, Func<object, R> use, Func<object> @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            var dependency = provider.Provide(context);
            if (dependency is null)
                return use(@default() ?? throw new ArgumentException($"{nameof(@default)} returns null.", nameof(@default)));
            else
                return dependency.Use(use);
        }

        public static R UseOr<R>(this IDependencyProvider provider, IDependencyContext context, Func<object, R> use, object @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            return provider.UseOr(context, use, () => @default);
        }


        public static void UseElse(this IDependencyProvider provider, IDependencyContext context, Action<object> use, Action @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            var dependency = provider.Provide(context);
            if (dependency is null)
                @else();
            else
                dependency.Use(use);
        }

        public static void UseElse(this IDependencyProvider provider, IDependencyContext context, Action<object> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.Provide(context)?.Use(use);
        }


        public static R UseElse<R>(this IDependencyProvider provider, IDependencyContext context, Func<object, R> use, Func<R> @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            var dependency = provider.Provide(context);
            if (dependency is null)
                return @else();
            else
                return dependency.Use(use);
        }

        public static R UseElse<R>(this IDependencyProvider provider, IDependencyContext context, Func<object, R> use, R @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            return provider.UseElse(context, use, () => @else);
        }


    }
}
