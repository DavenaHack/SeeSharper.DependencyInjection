using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class SeparateOrScope : OrScope
    {


        public SeparateOrScope(IEnumerable<IScope> scopes)
            : base(scopes) { }

        public SeparateOrScope(params IScope[] scopes)
            : this((IEnumerable<IScope>)scopes) { }


        public override IScope? InvolvedScope(IDependencyContext context, IEnumerable<IScope> scopes)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            var contextScope = context.GetScope();
            if (!In(contextScope))
                return null;

            IScope? inScope = null;
            foreach (var scope in Scopes)
                if (scope.In(contextScope))
                {
                    foreach (var scp in scopes)
                        if (scope.In(scp))
                            return scp;

                    if (inScope is null)
                        inScope = scope;
                }

            return inScope;
        }


    }
}
