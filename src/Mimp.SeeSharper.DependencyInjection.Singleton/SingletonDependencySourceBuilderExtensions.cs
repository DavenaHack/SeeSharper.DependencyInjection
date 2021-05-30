using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Singleton
{
    public static class SingletonDependencySourceBuilderExtensions
    {


        public static ITagDependencySourceBuilder AddSingleton(
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

            var singleton = new SingletonDependencySourceBuilder(builder, 
                new SingletonDependencyBuilder(constructible, factory));

            builder.AddDependency(() => singleton.BuildDependency());

            return singleton;
        }

        public static ITagDependencySourceBuilder AddSingleton(
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

            return builder.AddSingleton(
                BaseDependencyFactory.ConstructibleContext(constructible),
                BaseDependencyFactory.Construct(instantiate, initialize)
            );
        }

        public static ITagDependencySourceBuilder AddSingleton(
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

            return builder.AddSingleton(constructible, factory, null);
        }


        public static ITagTypeDependencySourceBuilder AddSingleton(
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

            var singleton = new SingletonTypeDependencySourceBuilder(builder,
                new SingletonTypeDependencyBuilder(type, factory));

            builder.AddDependency(() => singleton.BuildDependency());

            return singleton;
        }

        public static ITagTypeDependencySourceBuilder AddSingleton(
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

            return builder.AddSingleton(type, BaseDependencyFactory.Construct(instantiate, initialize));
        }

        public static ITagTypeDependencySourceBuilder AddSingleton(
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

            return builder.AddSingleton(type, factory, null);
        }


        public static ITagTypeDependencySourceBuilder AddSingleton<TDependency>(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, TDependency> factory,
            Action<IDependencyProvider, TDependency>? initialize
        ) where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            return builder.AddSingleton(
                typeof(TDependency),
                (provider, _) => factory(provider),
                initialize is null ? null : (provider, _, instance) => initialize(provider, (TDependency)instance)
            );
        }

        public static ITagTypeDependencySourceBuilder AddSingleton<TDependency>(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, TDependency> factory
        ) where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            return builder.AddSingleton(factory, null);
        }


    }
}
