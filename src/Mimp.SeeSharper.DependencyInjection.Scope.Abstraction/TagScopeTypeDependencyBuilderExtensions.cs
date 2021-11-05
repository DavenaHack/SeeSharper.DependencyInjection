using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class TagScopeTypeDependencyBuilderExtensions
    {


        public static ITagScopeTypeDependencyBuilder As<TDependency>(this ITagScopeTypeDependencyBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As<ITagScopeTypeDependencyBuilder, TDependency>();
        }

        public static ITagScopeTypeDependencyBuilder As<TDependency>(this ITagScopeTypeDependencyBuilder builder, Func<IDependencyProvider, object> tag)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return builder.As<ITagScopeTypeDependencyBuilder, TDependency>(tag);
        }

        public static ITagTypeTagDependencySourceBuilder As<TDependency>(this ITagTypeTagDependencySourceBuilder builder, object tag)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return builder.As<ITagTypeTagDependencySourceBuilder, TDependency>(tag);
        }


    }
}
