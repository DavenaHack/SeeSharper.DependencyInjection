using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public sealed class NoScope : IScope
    {


        internal NoScope() { }


        public bool In(IScope scope) => false;


        public IScope? InvolvedScope(IDependencyContext context, IEnumerable<IScope> scopes) => null;


    }
}
