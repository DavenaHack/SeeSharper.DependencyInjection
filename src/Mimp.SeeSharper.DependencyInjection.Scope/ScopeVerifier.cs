using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeVerifier : IScopeVerifier
    {


        public IEqualityComparer<object?> Comparer { get; }

        public bool SuperScopes { get; }


        public ScopeVerifier(IEqualityComparer<object?> comparer, bool superScopes)
        {
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            SuperScopes = superScopes;
        }

        public ScopeVerifier(bool superScopes)
            : this(new ScopeEqualityComparer(), superScopes) { }

        public ScopeVerifier()
            : this(true) { }


        public virtual bool HasScope(IDependencyProvider provider, object? scope)
        {
            if (provider is not IScopeDependencyProvider scopeProvider)
                return Comparer.Equals(null, scope);

            if (Comparer.Equals(scopeProvider.Scope.Scope, scope))
                return true;

            if (SuperScopes)
            {
                while (scopeProvider.Parent is IScopeDependencyProvider super)
                    if (Comparer.Equals(super.Scope.Scope, scope))
                        return true;
                    else
                        scopeProvider = super;
                return Comparer.Equals(null, scope);
            }

            return false;
        }


    }
}
