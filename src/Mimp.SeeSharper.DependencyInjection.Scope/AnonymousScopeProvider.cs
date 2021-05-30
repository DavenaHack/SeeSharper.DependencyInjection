using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class AnonymousScopeProvider : IScopeProvider
    {


        private readonly IDictionary<IDependencyScope, object> _scopes;


        public AnonymousScopeProvider()
        {
            _scopes = new Dictionary<IDependencyScope, object>();
        }


        public object GetScope(IDependencyScope provider)
        {
            if (!_scopes.TryGetValue(provider, out var scope))
                lock (_scopes)
                    if (!_scopes.TryGetValue(provider, out scope))
                        _scopes[provider] = scope = new object();
            return scope;
        }


    }
}
