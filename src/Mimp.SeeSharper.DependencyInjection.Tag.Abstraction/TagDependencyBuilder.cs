using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public static class TagDependencyBuilderExtensions
    {


        public static TBuilder Tag<TBuilder>(this TBuilder builder, object tag)
            where TBuilder : ITagDependencyBuilder
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (tag is null)
                throw new ArgumentNullException(nameof(tag));

            builder.Tag(_ => tag);

            return builder;
        }


    }
}
