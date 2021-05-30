using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Instantiation
{
    public static partial class InstantiationDependencySourceBuilderExtensions
    {


        public static ITagScopeTypeDependencySourceBuilder AddScopedInstantiation(this IDependencySourceBuilder builder, Type type, object? instantiateValues, object? initializeValues)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddScoped(
                type,
                InstantiateInitialize(type, instantiateValues ?? Array.Empty<KeyValuePair<string?, object?>>(), initializeValues)
            );
        }

        public static ITagScopeTypeDependencySourceBuilder AddScoped(this IDependencySourceBuilder builder, Type type)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddScopedInstantiation(type, null, null);
        }


        public static ITagScopeTypeDependencySourceBuilder AddScopedInstantiation<TDependency>(this IDependencySourceBuilder builder, object? instantiateValues, object? initializeValues)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddScopedInstantiation(typeof(TDependency), instantiateValues, initializeValues);
        }

        public static ITagScopeTypeDependencySourceBuilder AddScoped<TDependency>(this IDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddScoped(typeof(TDependency));
        }


    }
}
