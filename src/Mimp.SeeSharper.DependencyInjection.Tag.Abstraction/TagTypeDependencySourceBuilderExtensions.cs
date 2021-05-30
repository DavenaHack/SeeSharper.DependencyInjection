using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public static class TagTypeDependencySourceBuilderExtensions
    {


        public static ITagTypeDependencySourceBuilder AsSelf(this ITagTypeDependencySourceBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(builder.Type);
        }

        public static ITagTypeDependencySourceBuilder As<TDependency>(this ITagTypeDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(typeof(TDependency));
        }


        public static ITagTypeDependencySourceBuilder AsSelf(this ITagTypeDependencySourceBuilder builder, object tag)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(tag, builder.Type);
        }

        public static ITagTypeDependencySourceBuilder As<TDependency>(this ITagTypeDependencySourceBuilder builder, object tag)
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
