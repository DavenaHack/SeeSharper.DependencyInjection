using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class TagScopeTypeDependencyBuilderExtensions
    {


        public static ITagScopeTypeDependencyBuilder AsSelf(this ITagScopeTypeDependencyBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(builder.Type);
        }

        public static ITagScopeTypeDependencyBuilder As<TDependency>(this ITagScopeTypeDependencyBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(typeof(TDependency));
        }


        public static ITagScopeTypeDependencyBuilder AsSelf(this ITagScopeTypeDependencyBuilder builder, object tag)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(tag, builder.Type);
        }

        public static ITagScopeTypeDependencyBuilder As<TDependency>(this ITagScopeTypeDependencyBuilder builder, object tag)
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
