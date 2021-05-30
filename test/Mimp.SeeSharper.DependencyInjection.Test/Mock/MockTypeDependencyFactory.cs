using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Test.Mock
{
    public class MockTypeDependencyFactory : IDependencyFactory
    {


        public IDependency? Dependency { get; }

        public Type DependencyType { get; }


        public MockTypeDependencyFactory(Type dependencyType, IDependency? dependency = null)
        {
            Dependency = dependency;
            DependencyType = dependencyType ?? throw new ArgumentNullException(nameof(dependencyType));
        }


        public bool Constructible(IDependencyContext context, Type type) =>
            type == DependencyType;


        public IDependency Construct(IDependencyContext context, Type type) =>
            Dependency ?? new BaseDependency(type);


        public void Dispose(IDependencyProvider provider) { }


    }
}
