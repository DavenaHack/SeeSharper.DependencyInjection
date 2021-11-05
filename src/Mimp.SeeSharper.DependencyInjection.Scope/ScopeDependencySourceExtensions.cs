using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public static class ScopeDependencySourceExtensions
    {


        public static IDependencySource Scoped(this IDependencySource source, IScope scope)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            return new ScopeDependencySource(source, scope);
        }


    }
}
