using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public static class TypeDependencyBuilderExtensions
    {


        public static TBuilder AsSelf<TBuilder>(this TBuilder builder)
            where TBuilder : ITypeDependencyBuilder
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.As(builder.Type);

            return builder;
        }

        public static TBuilder As<TBuilder, TDependency>(this TBuilder builder)
            where TBuilder : ITypeDependencyBuilder
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.As(typeof(TDependency));

            return builder;
        }

        public static ITypeDependencyBuilder As<TDependency>(this ITypeDependencyBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As<ITypeDependencyBuilder, TDependency>();
        }


    }
}
