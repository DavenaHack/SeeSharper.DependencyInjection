using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.Instantiation.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Instantiation
{
    public static partial class InstantiationDependencySourceBuilderExtensions
    {


        public static ITagTypeDependencySourceBuilder AddSingletonInstantiation(this IDependencySourceBuilder builder, Type type, object? instantiateValues, object? initializeValues)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddSingleton(
                type,
                InstantiateInitialize(type, instantiateValues ?? Array.Empty<KeyValuePair<string?, object?>>(), initializeValues)
            );
        }

        public static ITagTypeDependencySourceBuilder AddSingleton(this IDependencySourceBuilder builder, Type type)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddSingletonInstantiation(type, null, null);
        }


        public static ITagTypeDependencySourceBuilder AddSingletonInstantiation<TDependency>(this IDependencySourceBuilder builder, object? instantiateValues, object? initializeValues)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddSingletonInstantiation(typeof(TDependency), instantiateValues, initializeValues);
        }

        public static ITagTypeDependencySourceBuilder AddSingleton<TDependency>(this IDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddSingleton(typeof(TDependency));
        }


    }
}
