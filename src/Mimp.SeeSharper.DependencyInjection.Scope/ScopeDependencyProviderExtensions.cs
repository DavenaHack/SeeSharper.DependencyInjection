using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public static class ScopeDependencyProviderExtensions
    {


        public static IScope GetScope(this IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            if (provider is IScopeDependencyProvider scopeProvider)
                return scopeProvider.Scope.Scope;

            return Scopes.No;
        }


    }
}
