using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public static class TypeDependencyBuilderExtensions
    {


        public static ITypeDependencyBuilder AsSelf(this ITypeDependencyBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(builder.Type);
        }

        public static ITypeDependencyBuilder As<TDependency>(this ITypeDependencyBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(typeof(TDependency));
        }


    }
}
