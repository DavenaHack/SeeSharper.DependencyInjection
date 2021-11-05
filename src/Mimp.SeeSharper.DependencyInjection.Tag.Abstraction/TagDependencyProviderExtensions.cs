using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public static class TagDependencyProviderExtensions
    {


        public static object TagVerifierTag { get; } = nameof(TagVerifierTag);


        public static ITagVerifier GetTagVerifier(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            return provider.GetDependencyRequired<ITagVerifier>(TagVerifierTag);
        }

        public static void UseTagVerifier(this IDependencyProvider provider, Action<ITagVerifier> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.Use(TagVerifierTag, use);
        }

        public static R UseTagVerifier<R>(this IDependencyProvider provider, Func<ITagVerifier, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return provider.Use(TagVerifierTag, use);
        }


        public static Func<ITagDependencyContext, Type, bool> Tagged(IEnumerable<KeyValuePair<Type?, object>> tags)
        {
            if (tags is null)
                throw new ArgumentNullException(nameof(tags));
            if (tags.Any(pair => pair.Value is null))
                throw new ArgumentNullException(nameof(tags), $"At least one tag is null.");

            tags = tags.ToArray();

            return (context, type) =>
            {
                // To provide a tag verfier it have to resolve TagVerifierTag
                if (context.DependencyType == typeof(ITagVerifier) && Equals(context.Tag, TagVerifierTag))
                    return tags.Any(pair => (pair.Key is null || pair.Key == typeof(ITagVerifier)) && Equals(pair.Value, TagVerifierTag));

                return context.Provider.UseTagVerifier(verifier => verifier.HasTag(context, type, tags));
            };
        }


        public static IDependency? Provide(this IDependencyProvider provider, object tag, Type type)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return provider.Provide(new TagDependencyContext(tag, provider, type));
        }

        public static IDependency? Provide<TDependency>(this IDependencyProvider provider, object tag)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return provider.Provide(tag, typeof(TDependency));
        }

        public static TDependency? GetDependency<TDependency>(this IDependencyProvider provider, object tag)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return (TDependency?)provider.Provide<TDependency>(tag)?.Dependency;
        }


        public static IDependency ProvideRequired(this IDependencyProvider provider, object tag, Type type)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return provider.ProvideRequired(new TagDependencyContext(tag, provider, type));
        }

        public static IDependency ProvideRequired<TDependency>(this IDependencyProvider provider, object tag)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return provider.ProvideRequired(tag, typeof(TDependency));
        }

        public static TDependency GetDependencyRequired<TDependency>(this IDependencyProvider provider, object tag)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return (TDependency)provider.ProvideRequired<TDependency>(tag).Dependency;
        }


        public static void Use(this IDependencyProvider provider, object tag, Type type, Action<object> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            DependencyProviderExtensions.Use(provider, new TagDependencyContext(tag, provider, type), use);
        }

        public static R Use<R>(this IDependencyProvider provider, object tag, Type type, Func<object, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return DependencyProviderExtensions.Use(provider, new TagDependencyContext(tag, provider, type), use);
        }


        public static void UseOr(this IDependencyProvider provider, object tag, Type type, Action<object> use, Func<object> @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            DependencyProviderExtensions.UseOr(provider, new TagDependencyContext(tag, provider, type), use, @default);
        }

        public static void UseOr(this IDependencyProvider provider, object tag, Type type, Action<object> use, object @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            DependencyProviderExtensions.UseOr(provider, new TagDependencyContext(tag, provider, type), use, @default);
        }


        public static R UseOr<R>(this IDependencyProvider provider, object tag, Type type, Func<object, R> use, Func<object> @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            return DependencyProviderExtensions.UseOr(provider, new TagDependencyContext(tag, provider, type), use, @default);
        }

        public static R UseOr<R>(this IDependencyProvider provider, object tag, Type type, Func<object, R> use, object @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            return DependencyProviderExtensions.UseOr(provider, new TagDependencyContext(tag, provider, type), use, @default);
        }


        public static void UseElse(this IDependencyProvider provider, object tag, Type type, Action<object> use, Action @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            DependencyProviderExtensions.UseElse(provider, new TagDependencyContext(tag, provider, type), use, @else);
        }

        public static void UseElse(this IDependencyProvider provider, object tag, Type type, Action<object> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            DependencyProviderExtensions.UseElse(provider, new TagDependencyContext(tag, provider, type), use);
        }


        public static R UseElse<R>(this IDependencyProvider provider, object tag, Type type, Func<object, R> use, Func<R> @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            return DependencyProviderExtensions.UseElse(provider, new TagDependencyContext(tag, provider, type), use, @else);
        }

        public static R UseElse<R>(this IDependencyProvider provider, object tag, Type type, Func<object, R> use, R @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            return DependencyProviderExtensions.UseElse(provider, new TagDependencyContext(tag, provider, type), use, @else);
        }



        public static void Use<TDependency>(this IDependencyProvider provider, object tag, Action<TDependency> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.Use(tag, typeof(TDependency), d => use((TDependency)d));
        }

        public static R Use<TDependency, R>(this IDependencyProvider provider, object tag, Func<TDependency, R> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            return provider.Use(tag, typeof(TDependency), d => use((TDependency)d));
        }


        public static void UseOr<TDependency>(this IDependencyProvider provider, object tag, Action<TDependency> use, Func<TDependency> @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            provider.UseOr(tag, typeof(TDependency), d => use((TDependency)d), @default);
        }

        public static void UseOr<TDependency>(this IDependencyProvider provider, object tag, Action<TDependency> use, TDependency @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            provider.UseOr(tag, typeof(TDependency), d => use((TDependency)d), @default);
        }


        public static R UseOr<TDependency, R>(this IDependencyProvider provider, object tag, Func<TDependency, R> use, Func<TDependency> @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            return provider.UseOr(tag, typeof(TDependency), d => use((TDependency)d), @default);
        }

        public static R UseOr<TDependency, R>(this IDependencyProvider provider, object tag, Func<TDependency, R> use, TDependency @default)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@default is null)
                throw new ArgumentNullException(nameof(@default));

            return provider.UseOr(tag, typeof(TDependency), d => use((TDependency)d), @default);
        }


        public static void UseElse<TDependency>(this IDependencyProvider provider, object tag, Action<TDependency> use, Action @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            provider.UseElse(tag, typeof(TDependency), d => use((TDependency)d), @else);
        }

        public static void UseElse<TDependency>(this IDependencyProvider provider, object tag, Action<TDependency> use)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (use is null)
                throw new ArgumentNullException(nameof(use));

            provider.UseElse(tag, typeof(TDependency), d => use((TDependency)d));
        }


        public static R UseElse<TDependency, R>(this IDependencyProvider provider, object tag, Func<TDependency, R> use, Func<R> @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            return provider.UseElse(tag, typeof(TDependency), d => use((TDependency)d), @else);
        }

        public static R UseElse<TDependency, R>(this IDependencyProvider provider, object tag, Func<TDependency, R> use, R @else)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));
            if (use is null)
                throw new ArgumentNullException(nameof(use));
            if (@else is null)
                throw new ArgumentNullException(nameof(@else));

            return provider.UseElse(tag, typeof(TDependency), d => use((TDependency)d), @else);
        }


    }
}
