using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public static class ScopeExtensions
    {


        public static IScope And(this IScope scope, IEnumerable<IScope> scopes)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            return new AndScope(new[] { scope }.Concat(scopes));
        }

        public static IScope And(this IScope scope, params IScope[] scopes)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return scope.And((IEnumerable<IScope>)scopes);
        }


        public static IScope Or(this IScope scope, IEnumerable<IScope> scopes)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            return new OrScope(new[] { scope }.Concat(scopes));
        }

        public static IScope Or(this IScope scope, params IScope[] scopes)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return scope.Or((IEnumerable<IScope>)scopes);
        }


        public static IScope Xor(this IScope scope, IEnumerable<IScope> scopes)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            return new XorScope(new[] { scope }.Concat(scopes));
        }

        public static IScope Xor(this IScope scope, params IScope[] scopes)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return scope.Xor((IEnumerable<IScope>)scopes);
        }


        public static IScope Sub(this IScope scope, IScope sub)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));
            if (sub is null)
                throw new ArgumentNullException(nameof(sub));

            return new SubScope(scope, sub);
        }


    }
}
