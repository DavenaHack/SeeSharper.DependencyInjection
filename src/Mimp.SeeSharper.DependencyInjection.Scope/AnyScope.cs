using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public sealed class AnyScope : IScope
    {


        internal AnyScope() { }


        public bool In(IScope scope) => true;


        public IScope? InvolvedScope(IDependencyContext context, IEnumerable<IScope> scopes)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            return context.GetScope();
        }

        public override string ToString()
        {
            return "*";
        }


    }
}
