using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class DependencyScopeFactoryExtensions
    {


        public static IDependencyScope CreateScope(this IDependencyScopeFactory factory, Func<IDependencyScope, object> scope, IDependencyProvider parent)
        {
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));

            return factory.CreateScope(new DependencyScopeContext(scope, parent));
        }

        public static IDependencyScope CreateScope(this IDependencyScopeFactory factory, object scope, IDependencyProvider parent)
        {
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));

            return factory.CreateScope(_ => scope, parent);
        }


    }
}
