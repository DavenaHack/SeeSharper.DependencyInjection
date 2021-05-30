using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public static class ScopeDependencySourceExtensions
    {


        public static IDependencySource Scoped(this IDependencySource source, Func<IDependencyProvider, IDependencyContext, bool> isScope)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (isScope is null)
                throw new ArgumentNullException(nameof(isScope));

            return new ScopeDependencySource(source, isScope);
        }

        public static IDependencySource Scoped(this IDependencySource source, IEnumerable<object> scopes)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            return source.Scoped(ScopeDependencySource.IsScopes(scopes));
        }

        public static IDependencySource Scoped(this IDependencySource source, object scope, params object[] scopes)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            return source.Scoped(new[] { scope }.Concat(scopes));
        }


    }
}
