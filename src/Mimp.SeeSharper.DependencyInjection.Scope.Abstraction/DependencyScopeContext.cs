using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public class DependencyScopeContext : IDependencyScopeContext
    {


        public Func<IDependencyScope, object> Scope { get; }

        public IDependencyProvider Parent { get; }


        public DependencyScopeContext(Func<IDependencyScope, object> scope, IDependencyProvider parent)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }


    }
}
