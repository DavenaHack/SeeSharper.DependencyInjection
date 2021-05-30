using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Enumerable
{
    public class EnumerableDependencyFactory : IDependencyFactory
    {


        public IEnumerable<IDependencyFactory> Factories { get; }


        public EnumerableDependencyFactory(IEnumerable<IDependencyFactory> factories)
        {
            Factories = factories?.ToArray() ?? throw new ArgumentNullException(nameof(factories));
            if (Factories.Any(f => f is null))
                throw new ArgumentNullException(nameof(factories), "At least one factory is null.");
        }


        public bool Constructible(IDependencyContext context, Type type)
        {
            if (!type.IsIEnumerable())
                return false;
            var valueType = type.GetIEnumerableValueType()!;
            return Factories.All(f => f.Constructible(context, valueType));
        }

        public IDependency Construct(IDependencyContext context, Type type)
        {
            if (!type.IsIEnumerable())
                throw new InvalidInvokeException($"{type} isn't a enumerable.");
            if (!Constructible(context, type))
                throw new InvalidInvokeException($"{this} can't construct {type}.");

            var valueType = type.GetIEnumerableValueType()!;
            var dependencies = Factories.Select(factory => factory.Construct(context, valueType)).ToArray();
            var values = (IList)typeof(List<>).MakeGenericType(valueType).New();
            foreach (var dependency in dependencies)
                values.Add(dependency.Dependency);

            return new EnumerableDependency(values, dependencies);
        }

        public void Dispose(IDependencyProvider provider)
        {
            foreach (var factory in Factories)
                factory.Dispose(provider);
        }


    }
}
