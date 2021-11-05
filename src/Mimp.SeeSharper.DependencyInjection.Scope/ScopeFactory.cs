using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeFactory : IScopeFactory
    {


        public IEqualityComparer<object> Comparer { get; }


        public ScopeFactory(IEqualityComparer<object> comparer)
        {
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public ScopeFactory()
            : this(EqualityComparer<object?>.Default) { }


        public IScope CreateScope(object? scope)
        {
            if (scope is null)
                return Scopes.Any;

            return new Scope(scope, Comparer);
        }


    }
}
