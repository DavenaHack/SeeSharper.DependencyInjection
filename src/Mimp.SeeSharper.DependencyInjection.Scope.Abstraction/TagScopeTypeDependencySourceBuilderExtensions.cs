using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class TagScopeTypeDependencySourceBuilderExtensions
    {


        public static ITagScopeTypeDependencySourceBuilder As<TDependency>(this ITagScopeTypeDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As<ITagScopeTypeDependencySourceBuilder, TDependency>();
        }

        public static ITagScopeTypeDependencySourceBuilder As<TDependency>(this ITagScopeTypeDependencySourceBuilder builder, Func<IDependencyProvider, object> tag)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return builder.As<ITagScopeTypeDependencySourceBuilder, TDependency>(tag);
        }

        public static ITagScopeTypeDependencySourceBuilder As<TDependency>(this ITagScopeTypeDependencySourceBuilder builder, object tag)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return builder.As<ITagScopeTypeDependencySourceBuilder, TDependency>(tag);
        }


    }
}
