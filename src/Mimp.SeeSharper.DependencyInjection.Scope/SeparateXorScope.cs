using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class SeparateXorScope : XorScope
    {


        public SeparateXorScope(IEnumerable<IScope> scopes)
            : base(scopes) { }

        public SeparateXorScope(params IScope[] scopes)
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

            foreach (var scope in Scopes)
                if (scope.In(contextScope))
                {
                    foreach (var scp in scopes)
                        if (scope.In(scp))
                            return scp;

                    return scope;
                }

            return null;
        }


    }
}
