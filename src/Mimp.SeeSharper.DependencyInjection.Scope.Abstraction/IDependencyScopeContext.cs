using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IDependencyScopeContext
    {


        public Func<IDependencyScope, object> Scope { get; }

        public IDependencyProvider Parent { get; }


    }
}
