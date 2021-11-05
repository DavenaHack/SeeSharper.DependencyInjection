using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public abstract class BaseScopeProvider : IScopeProvider
    {


        private readonly IDictionary<IDependencyScope, IScope> _scopes;


        protected BaseScopeProvider()
        {
            _scopes = new Dictionary<IDependencyScope, IScope>();
        }


        public IScope GetScope(IDependencyScope scope)
        {
            if (!_scopes.TryGetValue(scope, out var scp))
                lock (_scopes)
                    if (!_scopes.TryGetValue(scope, out scp))
                    {
                        _scopes[scope] = scp = CreateScope(scope);
                        scope.OnDisposed += ReleaseCache;
                    }
            return scp;
        }


        private void ReleaseCache(object? sender, EventArgs e)
        {
            if (sender is IDependencyScope scope)
            {
                scope.OnDisposed -= ReleaseCache;
                lock (_scopes)
                    _scopes.Remove(scope);
            }
        }


        protected abstract IScope CreateScope(IDependencyScope scope);


    }
}
