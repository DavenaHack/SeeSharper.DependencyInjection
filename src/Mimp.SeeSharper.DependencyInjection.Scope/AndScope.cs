using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class AndScope : IScope
    {


        public IEnumerable<IScope> Scopes { get; }


        protected IEnumerable<IScope> FlatScopes
        {
            get
            {
                foreach (var scope in Scopes)
                    if (scope is AndScope and)
                        foreach (var scp in and.FlatScopes)
                            yield return scp;
                    else
                        yield return scope;
            }
        }


        public AndScope(IEnumerable<IScope> scopes)
        {
            Scopes = scopes?.Select(s => s ?? throw new ArgumentNullException(nameof(scopes), "At least one scope is null"))?.ToArray()
                ?? throw new ArgumentNullException(nameof(scopes));
        }

        public AndScope(params IScope[] scopes)
            : this((IEnumerable<IScope>)scopes) { }


        public bool In(IScope scope)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            if (ReferenceEquals(this, scope))
                return true;

            if (scope is AndScope and)
                return and.FlatScopes.All(scp => FlatScopes.Any(s => s.In(scp)))
                    && FlatScopes.All(scp => and.FlatScopes.Any(s => scp.In(s)));

            return FlatScopes.All(scp => scp.In(scope));
        }


        public virtual IScope? InvolvedScope(IDependencyContext context, IEnumerable<IScope> scopes)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            if (!In(context.GetScope()))
                return null;

            foreach (var scope in scopes)
                if (In(scope))
                    return scope;

            return this;
        }


        public override string ToString()
        {
            return "(" + string.Join("&", FlatScopes.Select(s => s.ToString())) + ")";
        }

    }
}
