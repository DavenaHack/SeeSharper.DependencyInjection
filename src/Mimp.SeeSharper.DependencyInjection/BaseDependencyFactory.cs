using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using Mimp.SeeSharper.Reflection;
#if NullableAttributes
using System.Diagnostics.CodeAnalysis;
#endif

namespace Mimp.SeeSharper.DependencyInjection
{
    public abstract class BaseDependencyFactory : IDependencyFactory
    {


        public Func<IDependencyContext, Type, bool> IsConstructible { get; }

        public Func<IDependencyContext, Type, Action<object>, object> Factory { get; }


        protected BaseDependencyFactory(Func<IDependencyContext, Type, bool> constructible, Func<IDependencyContext, Type, Action<object>, object> factory)
        {
            IsConstructible = constructible ?? throw new ArgumentNullException(nameof(constructible));
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }


        public virtual bool Constructible(IDependencyContext context, Type type)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return IsConstructible(context, type);
        }

        protected void ThrowIfIsNotConstructible(IDependencyContext context, Type type)
        {
            if (!Constructible(context, type))
                throw new InvalidInvokeException($"{this} can't construct {type}.");
        }


        public abstract IDependency Construct(IDependencyContext context, Type type);

        protected object ConstructInstance(IDependencyContext context, Type type, Action<object> saveInstance)
        {
            var called = false;
            object? savedInstance = null;
            var instance = Factory(context, type, instance =>
            {
                called = true;
                savedInstance = instance;
                saveInstance(instance);
            });
            if (!called)
                saveInstance(instance);
            else if (!ReferenceEquals(savedInstance, instance))
                throw new InvalidOperationException($"Save action passed a diffrent instance as it returned");
            return instance;
        }


        public abstract void Dispose(IDependencyProvider provider);


        protected static bool TryGetValue(
            Type type,
            IEnumerable<object> instances,
#if NullableAttributes 
            [NotNullWhen(true)]
#endif
            out object? instance
        )
        {
            foreach (var i in instances)
                if (type.IsAssignableFrom(i.GetType()))
                {
                    instance = i;
                    return true;
                }
            instance = null;
            return false;
        }


        public static Func<IDependencyContext, Type, bool> TypeConstructible(IEnumerable<Type> types)
        {
            if (types is null)
                throw new ArgumentNullException(nameof(types));
            if (types.Any(t => t is null))
                throw new ArgumentNullException(nameof(types), "At least one type is null.");

            types = types.ToArray();

            return (_, type) => types.Any(t => ConstructibleType(t, type));
        }

        public static bool ConstructibleType(Type dependencyType, Type type)
        {
            if (dependencyType is null)
                throw new ArgumentNullException(nameof(dependencyType));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return dependencyType == type || dependencyType.IsGenericTypeDefinition && type.Inherit(dependencyType);
        }


        public static Func<IDependencyContext, Type, bool> ConstructibleContext(
            Func<IDependencyProvider, Type, bool> constructible
        )
        {
            if (constructible is null)
                throw new ArgumentNullException(nameof(constructible));

            return (context, type) => constructible(context.Provider, type);
        }


        public static Func<IDependencyContext, Type, Action<object>, object> Construct(
            Func<IDependencyProvider, Type, object> instantiate,
            Action<IDependencyProvider, Type, object>? initialize
        )
        {
            if (instantiate is null)
                throw new ArgumentNullException(nameof(instantiate));

            return (context, type, save) =>
            {
                var provider = context.Provider;
                var instance = instantiate(provider, type);
                save(instance);
                initialize?.Invoke(provider, type, instance);
                return instance;
            };
        }

        public static Func<IDependencyContext, Type, object> Construct(
            Func<IDependencyProvider, Type, object> factory
        )
        {
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            return (context, type) => factory(context.Provider, type);
        }


    }
}
