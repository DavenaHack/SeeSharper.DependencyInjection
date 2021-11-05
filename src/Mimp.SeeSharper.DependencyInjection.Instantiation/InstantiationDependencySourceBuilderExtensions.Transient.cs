using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Transient;
using Mimp.SeeSharper.ObjectDescription;
using Mimp.SeeSharper.ObjectDescription.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Instantiation
{
    public static partial class InstantiationDependencySourceBuilderExtensions
    {


        public static ITagTypeTagDependencySourceBuilder AddTransientInstantiation(this IDependencySourceBuilder builder, Type type,
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

            return builder.AddTransient(
                type,
                Construct(type, instantiate, initialize)
            );
        }

        public static ITagTypeTagDependencySourceBuilder AddTransientInstantiation(this IDependencySourceBuilder builder, Type type, IObjectDescription description)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (description is null)
                throw new ArgumentNullException(nameof(description));

            return builder.AddTransient(
                type,
                Construct(type, description)
            );
        }

        public static ITagTypeTagDependencySourceBuilder AddTransient(this IDependencySourceBuilder builder, Type type)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddTransientInstantiation(type, ObjectDescriptions.EmptyDescription);
        }


        public static ITagTypeTagDependencySourceBuilder AddTransientInstantiation<TDependency>(this IDependencySourceBuilder builder,
            IObjectDescription instantiate, IObjectDescription initialize)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (instantiate is null)
                throw new ArgumentNullException(nameof(instantiate));
            if (initialize is null)
                throw new ArgumentNullException(nameof(initialize));

            return builder.AddTransientInstantiation(typeof(TDependency), instantiate, initialize);
        }

        public static ITagTypeTagDependencySourceBuilder AddTransientInstantiation<TDependency>(this IDependencySourceBuilder builder, IObjectDescription description)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (description is null)
                throw new ArgumentNullException(nameof(description));

            return builder.AddTransientInstantiation(typeof(TDependency), description);
        }

        public static ITagTypeTagDependencySourceBuilder AddTransient<TDependency>(this IDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddTransient(typeof(TDependency));
        }


    }
}
