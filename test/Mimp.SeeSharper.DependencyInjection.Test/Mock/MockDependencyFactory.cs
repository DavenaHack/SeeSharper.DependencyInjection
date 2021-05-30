using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Test.Mock
{
    public class MockDependencyFactory : IDependencyFactory
    {


        public bool IsConstructible { get; }

        public IDependency? Dependency { get; }


        public MockDependencyFactory(bool constructible = false, IDependency? dependency = null)
        {
            IsConstructible = constructible;
            Dependency = dependency;
        }


        public bool Constructible(IDependencyContext context, Type type) => IsConstructible;

        public IDependency Construct(IDependencyContext context, Type type) => Dependency ?? new BaseDependency(type);

        public void Dispose(IDependencyProvider provider) { }


    }
}
