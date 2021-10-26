using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.ObjectDescription;
using Mimp.SeeSharper.ObjectDescription.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Instantiation
{
    public static partial class InstantiationDependencySourceBuilderExtensions
    {


        public static ITagScopeTypeDependencySourceBuilder AddScopedInstantiation(this IDependencySourceBuilder builder, Type type,
            IObjectDescription instantiate, IObjectDescription initialize)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (instantiate is null)
                throw new ArgumentNullException(nameof(instantiate));
            if (initialize is null)
                throw new ArgumentNullException(nameof(initialize));

            return builder.AddScoped(
                type,
                InstantiateInitialize(type, instantiate, initialize)
            );
        }

        public static ITagScopeTypeDependencySourceBuilder AddScopedInstantiation(this IDependencySourceBuilder builder, Type type, IObjectDescription description)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (description is null)
                throw new ArgumentNullException(nameof(description));

            return builder.AddScoped(
                type,
                InstantiateInitialize(type, description)
            );
        }

        public static ITagScopeTypeDependencySourceBuilder AddScoped(this IDependencySourceBuilder builder, Type type)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddScopedInstantiation(type, ObjectDescriptions.EmptyDescription);
        }


        public static ITagScopeTypeDependencySourceBuilder AddScopedInstantiation<TDependency>(this IDependencySourceBuilder builder,
            IObjectDescription instantiate, IObjectDescription initialize)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (instantiate is null)
                throw new ArgumentNullException(nameof(instantiate));
            if (initialize is null)
                throw new ArgumentNullException(nameof(initialize));

            return builder.AddScopedInstantiation(typeof(TDependency), instantiate, initialize);
        }

        public static ITagScopeTypeDependencySourceBuilder AddScopedInstantiation<TDependency>(this IDependencySourceBuilder builder, IObjectDescription description)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (description is null)
                throw new ArgumentNullException(nameof(description));

            return builder.AddScopedInstantiation(typeof(TDependency), description);
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
