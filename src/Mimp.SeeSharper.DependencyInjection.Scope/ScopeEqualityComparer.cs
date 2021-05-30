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


        public ScopeEqualityComparer(IEqualityComparer<object?> comparer)
        {
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public ScopeEqualityComparer()
            : this(EqualityComparer<object?>.Default) { }


        public new virtual bool Equals(object? providerScope, object? scope)
        {
            return scope is null
                || Comparer.Equals(providerScope, scope);
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
