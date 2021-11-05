using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class Scope : IScope
    {


        public object Value { get; }

        public IEqualityComparer<object> Comparer { get; }


        public Scope(object value, IEqualityComparer<object> comparer)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }


        public bool In(IScope scope)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            if (ReferenceEquals(this, scope))
                return true;

            if (scope is not Scope scp)
                return scope.In(this);

            return scp.Comparer.Equals(Value, scp.Value);
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
            return Value.ToString() ?? string.Empty;
        }


    }
}
