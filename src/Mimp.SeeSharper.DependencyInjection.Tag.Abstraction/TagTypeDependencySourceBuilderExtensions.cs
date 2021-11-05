using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public static class TagTypeDependencySourceBuilderExtensions
    {


        public static ITagTypeTagDependencySourceBuilder As<TDependency>(this ITagTypeTagDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As<ITagTypeTagDependencySourceBuilder, TDependency>();
        }

        public static ITagTypeTagDependencySourceBuilder As<TDependency>(this ITagTypeTagDependencySourceBuilder builder, Func<IDependencyProvider, object> tag)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            return builder.As<ITagTypeTagDependencySourceBuilder, TDependency>(tag);
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
