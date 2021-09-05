using System;
using System.Collections.Generic;
#if !NETSTANDARD2_1 && NullableAttributes
using System.Diagnostics.CodeAnalysis;
#endif

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeEqualityComparer : IEqualityComparer<object?>
    {


        public IEqualityComparer<object?> Comparer { get; }

        public bool SubScopes { get; }


        public ScopeEqualityComparer(IEqualityComparer<object?> comparer, bool subScopes)
        {
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            SubScopes = subScopes;
        }

        public ScopeEqualityComparer(bool subScopes)
            : this(EqualityComparer<object?>.Default, subScopes) { }

        public ScopeEqualityComparer()
            : this(true) { }


        public new virtual bool Equals(object? providerScope, object? scope)
        {
            if (scope is null)
                return true;

            if (Comparer.Equals(providerScope, scope))
                return true;

            if (SubScopes)
            {
                bool IsSubScope(object? providerScope)
                {
                    while (providerScope is SubScope sub)
                    {
                        if (Comparer.Equals(sub.Scope, scope))
                            return true;
                        if (sub.Scope is SubScope)
                            return IsSubScope(sub.Scope);

                        providerScope = sub.Parent;
                        if (Comparer.Equals(providerScope, scope))
                            return true;
                    }
                    return false;
                }

                return IsSubScope(providerScope);
            }

            return false;
        }


        public int GetHashCode(
#if !NETSTANDARD2_1 && NullableAttributes
            [DisallowNull] 
#endif
            object? obj
            )
        {
            return Comparer.GetHashCode(obj!);
        }


    }
}
