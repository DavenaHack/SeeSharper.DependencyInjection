using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class TagScopeTypeDependencySourceBuilderExtensions
    {


        public static ITagScopeTypeDependencySourceBuilder AsSelf(this ITagScopeTypeDependencySourceBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(builder.Type);
        }

        public static ITagScopeTypeDependencySourceBuilder As<TDependency>(this ITagScopeTypeDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(typeof(TDependency));
        }


        public static ITagScopeTypeDependencySourceBuilder AsSelf(this ITagScopeTypeDependencySourceBuilder builder, object tag)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(tag, builder.Type);
        }

        public static ITagScopeTypeDependencySourceBuilder As<TDependency>(this ITagScopeTypeDependencySourceBuilder builder, object tag)
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
