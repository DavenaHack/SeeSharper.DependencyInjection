using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.ObjectDescription;
using Mimp.SeeSharper.ObjectDescription.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Instantiation
{
    public static partial class InstantiationDependencySourceBuilderExtensions
    {


        public static ITagTypeTagDependencySourceBuilder AddSingletonInstantiation(this IDependencySourceBuilder builder, Type type, IObjectDescription instantiate, IObjectDescription initialize)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (instantiate is null)
                throw new ArgumentNullException(nameof(instantiate));
            if (initialize is null)
                throw new ArgumentNullException(nameof(initialize));

            return builder.AddSingleton(
                type,
                InstantiateInitialize(type, instantiate, initialize)
            );
        }

        public static ITagTypeTagDependencySourceBuilder AddSingletonInstantiation(this IDependencySourceBuilder builder, Type type, IObjectDescription description)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (description is null)
                throw new ArgumentNullException(nameof(description));

            return builder.AddSingleton(
                type,
                InstantiateInitialize(type, description)
            );
        }

        public static ITagTypeTagDependencySourceBuilder AddSingleton(this IDependencySourceBuilder builder, Type type)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return builder.AddSingletonInstantiation(type, ObjectDescriptions.EmptyDescription);
        }


        public static ITagTypeTagDependencySourceBuilder AddSingletonInstantiation<TDependency>(this IDependencySourceBuilder builder,
            IObjectDescription instantiate, IObjectDescription initialize)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (instantiate is null)
                throw new ArgumentNullException(nameof(instantiate));
            if (initialize is null)
                throw new ArgumentNullException(nameof(initialize));

            return builder.AddSingletonInstantiation(typeof(TDependency), instantiate, initialize);
        }

        public static ITagTypeTagDependencySourceBuilder AddSingletonInstantiation<TDependency>(this IDependencySourceBuilder builder,
            IObjectDescription description)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (description is null)
                throw new ArgumentNullException(nameof(description));

            return builder.AddSingletonInstantiation(typeof(TDependency), description);
        }

        public static ITagTypeTagDependencySourceBuilder AddSingleton<TDependency>(this IDependencySourceBuilder builder)
            where TDependency : notnull
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddSingleton(typeof(TDependency));
        }


    }
}
