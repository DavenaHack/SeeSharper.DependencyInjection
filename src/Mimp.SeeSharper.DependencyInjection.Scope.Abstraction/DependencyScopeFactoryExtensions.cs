using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public static class DependencyScopeFactoryExtensions
    {


        public static IDependencyScope CreateDependencyScope(this IDependencyScopeFactory factory, Func<IDependencyScope, IScope> scope, IDependencyProvider parent)
        {
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));

            return factory.CreateDependencyScope(new ScopeContext(scope, parent));
        }

        public static IDependencyScope CreateDependencyScope(this IDependencyScopeFactory factory, IScope scope, IDependencyProvider parent)
        {
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));

            return factory.CreateDependencyScope(_ => scope, parent);
        }


    }
}
