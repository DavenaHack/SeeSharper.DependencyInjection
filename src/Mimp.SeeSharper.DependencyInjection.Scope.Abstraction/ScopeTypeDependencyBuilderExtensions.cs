using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class ScopeTypeDependencyBuilderExtensions
    {


        public static IScopeTypeDependencyBuilder AsSelf(this IScopeTypeDependencyBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(builder.Type);
        }

        public static IScopeTypeDependencyBuilder As<TDependency>(this IScopeTypeDependencyBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As(typeof(TDependency));
        }


    }
}
