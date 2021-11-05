using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeContext
    {


        public Func<IDependencyScope, IScope> Scope { get; }

        public IDependencyProvider Parent { get; }


    }
}
