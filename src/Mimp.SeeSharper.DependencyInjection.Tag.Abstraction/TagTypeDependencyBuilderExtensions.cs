using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public static class TagTypeDependencyBuilderExtensions
    {


        public static ITagTypeDependencyBuilder AsSelf(this ITagTypeDependencyBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(builder.Type);
        }

        public static ITagTypeDependencyBuilder As<TDependency>(this ITagTypeDependencyBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(typeof(TDependency));
        }


        public static ITagTypeDependencyBuilder AsSelf(this ITagTypeDependencyBuilder builder, object tag)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return builder.As(tag, builder.Type);
        }

        public static ITagTypeDependencyBuilder As<TDependency>(this ITagTypeDependencyBuilder builder, object tag)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return builder.As(tag, typeof(TDependency));
        }


    }
}
