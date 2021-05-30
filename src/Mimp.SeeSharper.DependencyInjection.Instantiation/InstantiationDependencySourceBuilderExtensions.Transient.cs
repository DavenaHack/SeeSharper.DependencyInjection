using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Transient;
using Mimp.SeeSharper.Instantiation.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Instantiation
{
    public static partial class InstantiationDependencySourceBuilderExtensions
    {


        public static ITagTypeDependencySourceBuilder AddTransientInstantiation(this IDependencySourceBuilder builder, Type type, object? instantiateValues, object? initializeValues)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddTransient(
                type,
                Construct(type, instantiateValues ?? Array.Empty<KeyValuePair<string?, object?>>(), initializeValues)
            );
        }

        public static ITagTypeDependencySourceBuilder AddTransient(this IDependencySourceBuilder builder, Type type)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddTransientInstantiation(type, null, null);
        }


        public static ITagTypeDependencySourceBuilder AddTransientInstantiation<TDependency>(this IDependencySourceBuilder builder, object? instantiateValues, object? initializeValues)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddTransientInstantiation(typeof(TDependency), instantiateValues, initializeValues);
        }

        public static ITagTypeDependencySourceBuilder AddTransient<TDependency>(this IDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddTransient(typeof(TDependency));
        }


    }
}
