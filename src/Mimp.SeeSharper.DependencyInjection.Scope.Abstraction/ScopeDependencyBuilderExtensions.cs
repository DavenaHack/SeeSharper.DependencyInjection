using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class ScopeDependencyBuilderExtensions
    {


        public static TBuilder AddScope<TBuilder>(this TBuilder builder, IScope scope)
            where TBuilder : IScopeDependencyBuilder
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            builder.AddScope(_ => scope);

            return builder;
        }

        public static TBuilder AddScope<TBuilder>(this TBuilder builder, object? scope)
            where TBuilder : IScopeDependencyBuilder
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.AddScope(provider => provider.CreateScope(scope));

            return builder;
        }


    }
}
