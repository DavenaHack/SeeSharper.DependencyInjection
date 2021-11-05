using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public static class TagTypeDependencyBuilderExtensions
    {


        public static TBuilder As<TBuilder>(this TBuilder builder, object tag, Type type)
            where TBuilder : ITagTypeDependencyBuilder
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.As(_ => tag, type);

            return builder;
        }


        public static ITagTypeDependencyBuilder As<TDependency>(this ITagTypeDependencyBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As<ITagTypeDependencyBuilder, TDependency>();
        }


        public static TBuilder AsSelf<TBuilder>(this TBuilder builder, Func<IDependencyProvider, object> tag)
            where TBuilder : ITagTypeDependencyBuilder
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            builder.As(tag, builder.Type);

            return builder;
        }

        public static TBuilder AsSelf<TBuilder>(this TBuilder builder, object tag)
            where TBuilder : ITagTypeDependencyBuilder
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            builder.As(_ => tag, builder.Type);

            return builder;
        }

        public static TBuilder As<TBuilder, TDependency>(this TBuilder builder, Func<IDependencyProvider, object> tag)
            where TBuilder : ITagTypeDependencyBuilder
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            builder.As(tag, typeof(TDependency));

            return builder;
        }

        public static TBuilder As<TBuilder, TDependency>(this TBuilder builder, object tag)
            where TBuilder : ITagTypeDependencyBuilder
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            builder.As(_ => tag, typeof(TDependency));

            return builder;
        }


        public static ITagTypeDependencyBuilder As<TDependency>(this ITagTypeDependencyBuilder builder, Func<IDependencyProvider, object> tag)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return builder.As<ITagTypeDependencyBuilder, TDependency>(tag);
        }

        public static ITagTypeDependencyBuilder As<TDependency>(this ITagTypeDependencyBuilder builder, object tag)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return builder.As<ITagTypeDependencyBuilder, TDependency>(tag);
        }


    }
}
