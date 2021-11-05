using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeDependency : BaseDependency
    {


        public IScope Scope { get; }


        public ScopeDependency(IScope scope, object dependency)
            : base(dependency)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }


    }
}
