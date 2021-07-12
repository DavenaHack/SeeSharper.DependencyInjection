using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Transient;
using Mimp.SeeSharper.Instantiation;
using Mimp.SeeSharper.Instantiation.Abstraction;
using Mimp.SeeSharper.Instantiation.TypeResolver;
using Mimp.SeeSharper.Reflection;
using Mimp.SeeSharper.TypeProvider;
using Mimp.SeeSharper.TypeProvider.Abstraction;
using Mimp.SeeSharper.TypeResolver.Abstraction;
using Mimp.SeeSharper.TypeResolver.TypeProvider;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Instantiation
{
    public static partial class InstantiationDependencySourceBuilderExtensions
    {


        public static object InstantiatorTag { get; } = nameof(InstantiatorTag);

        public static object InstantiatorTypeResolverTag { get; } = nameof(InstantiatorTypeResolverTag);


        public static IDependencySourceBuilder UseInstantiator(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, IInstantiator> instantiatorFactory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (instantiatorFactory is null)
                throw new ArgumentNullException(nameof(instantiatorFactory));

            return builder.AddTransient(instantiatorFactory)
                .Tag(InstantiatorTag);
        }

        public static IDependencySourceBuilder UseInstantiatorTypeResolver(
            this IDependencySourceBuilder builder,
            Func<IDependencyProvider, ITypeResolver> typeResolverFactory
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (typeResolverFactory is null)
                throw new ArgumentNullException(nameof(typeResolverFactory));

            return builder.AddTransient(typeResolverFactory)
                .Tag(InstantiatorTypeResolverTag);
        }

        public static IDependencySourceBuilder UseInstantiator(
            this IDependencySourceBuilder builder
        )
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder
                .UseInstantiatorTypeResolver(GetDefaultTypeResolver)
                .UseInstantiator(GetDefaultInstantiator);
        }

        public static ITypeResolver GetDefaultTypeResolver(IDependencyProvider provider)
        {
            return new ProvidingTypeResolver(new MultipleAssemblyTypeProvider(new IAssemblyTypeProvider[] {
                new UsedAssemblyTypeProvider(),
                new EntryAssemblyTypeProvider(),
                new ExecutingAssemblyTypeProvider()
            }));
        }

        public static IInstantiator GetDefaultInstantiator(IDependencyProvider provider)
        {
            return provider.Use<ITypeResolver, IInstantiator>(InstantiatorTypeResolverTag, resolver => GetDefaultInstantiator(provider, resolver));
        }

        public static IInstantiator GetDefaultInstantiator(
            IDependencyProvider provider,
            ITypeResolver typeResolver,
            string typeKey = "$type",
            string tagKey = "$tag",
            Action<IInstantiatorBuilder>? beforeAddDefaults = null,
            Action<IInstantiatorBuilder>? afterAddDefaults = null
        )
        {
            if (typeResolver is null)
                throw new ArgumentNullException(nameof(typeResolver));

            var builder = new TryThrowInstantiatorBuilder();

            beforeAddDefaults?.Invoke(builder);

            builder.SetTypedRoot(typeResolver, typeKey);
            builder.AddTypeResolver(typeResolver);

            builder.Add(root =>
            {
                var internalBuilder = new TryThrowInstantiatorBuilder();
                internalBuilder.SetTypedRoot(typeResolver, typeKey);
                internalBuilder.Add(
                    new DependencyInjectionInstantiator(provider, tagKey),
                    root
                );

                var intern = internalBuilder.Build();
                var instance = new ConstructorMemberInstantiator(intern, true);

                var instantiators = new List<IInstantiator>();

                foreach (var i in SeeSharper.Instantiation.InstantiatorBuilderExtensions.AddUtilities(_ => instance, _ => intern)(root))
                    instantiators.Add(i);

                return instantiators;
            });

            afterAddDefaults?.Invoke(builder);

            return builder.Build();
        }


        public static Func<IDependencyContext, Type, Action<object>, object> InstantiateInitialize(Type type, object? instantiateValues, object? initializeValues)
        {
            return (context, t, save) => context.Provider.Use<IInstantiator, object>(InstantiatorTag, instantiator =>
            {
                t = GetInstantiationType(t, type);
                var n = t.Name;
                var instance = instantiator.Instantiate(t, instantiateValues, out _) ?? throw new InvalidOperationException($"{instantiator} return null for type {t} and {instantiateValues}");
                save(instance);
                instantiator.Initialize(instance, initializeValues, out _);
                return instance;
            });
        }

        public static Func<IDependencyContext, Type, object> Construct(Type type, object? instantiateValues, object? initializeValues)
        {
            var factory = InstantiateInitialize(type, instantiateValues, initializeValues);
            return (context, t) => factory(context, t, _ => { });
        }

        private static Type GetInstantiationType(Type dependencyType, Type implementationType)
        {
            return implementationType.HasGenericParameters()
                ? implementationType.ResolveInheritGenericType(dependencyType)
                : implementationType;
        }


    }
}
