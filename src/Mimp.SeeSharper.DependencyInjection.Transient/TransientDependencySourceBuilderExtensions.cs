using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Transient
{
    public static class TransientDependencySourceBuilderExtensions
    {


        public static ITagDependencySourceBuilder AddTransient(
            this IDependencySourceBuilder builder,
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, object> factory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (constructible is null)
                throw new ArgumentNullException(nameof(constructible));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            var transient = new TransientDependencySourceBuilder(builder,
                new TransientDependencyBuilder(constructible, factory));

            builder.AddDependency(() => transient.BuildDependency());

            return transient;
        }

        public static ITagDependencySourceBuilder AddTransient(
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

            return builder.AddTransient(
                BaseDependencyFactory.ConstructibleContext(constructible),
                BaseDependencyFactory.Construct(factory)
            );
        }


        public static ITagTypeDependencySourceBuilder AddTransient(
            this IDependencySourceBuilder builder,
            Type type,
            Func<IDependencyContext, Type, object> factory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            var transient = new TransientTypeDependencySourceBuilder(builder,
                new TransientTypeDependencyBuilder(type, factory));

            builder.AddDependency(() => transient.BuildDependency());

            return transient;
        }

        public static ITagTypeDependencySourceBuilder AddTransient(
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

            return builder.AddTransient(
                type,
                BaseDependencyFactory.Construct(factory)
            );
        }


        public static ITagTypeDependencySourceBuilder AddTransient<TDependency>(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, TDependency> factory
        ) where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            return builder.AddTransient(
                typeof(TDependency),
                (provider, _) => factory(provider)
            );
        }


    }
}
