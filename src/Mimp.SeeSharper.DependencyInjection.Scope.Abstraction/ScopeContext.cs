using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public class ScopeContext : IScopeContext
    {


        public Func<IDependencyScope, IScope> Scope { get; }

        public IDependencyProvider Parent { get; }


        public ScopeContext(Func<IDependencyScope, IScope> scope, IDependencyProvider parent)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }


    }
}
