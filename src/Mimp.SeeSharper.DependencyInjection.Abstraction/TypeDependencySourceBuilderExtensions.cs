using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public static class TypeDependencySourceBuilderExtensions
    {


        public static ITypeDependencySourceBuilder AsSelf(this ITypeDependencySourceBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(builder.Type);
        }

        public static ITypeDependencySourceBuilder As<TDependency>(this ITypeDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(typeof(TDependency));
        }


    }
}
