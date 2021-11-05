using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class ScopeTypeDependencySourceBuilderExtensions
    {


        public static IScopeTypeDependencySourceBuilder As<TDependency>(this IScopeTypeDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.As<IScopeTypeDependencySourceBuilder, TDependency>();
        }


    }
}
