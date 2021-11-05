using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;
#if NullableAttributes
using System.Diagnostics.CodeAnalysis;
#endif

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeDependencyFactory : BaseDependencyFactory, IScopeDependencyFactory
    {


        private readonly IDictionary<IScope, ScopeContainer> _scopes;


        public IScope Scope { get; }

        public bool DisposeAutomatically { get; }


        public ScopeDependencyFactory(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, Action<object>, object> factory,
            IScope scope,
            bool disposeAutomatically
        ) : base(constructible, factory)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            DisposeAutomatically = disposeAutomatically;
            _scopes = new Dictionary<IScope, ScopeContainer>();
        }


        public override IDependency Construct(IDependencyContext context, Type type)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            ThrowIfIsNotConstructible(context, type);

            var provider = context.Provider;
            var scopeProvider = provider is IScopeDependencyProvider sp ? sp : null;

            var scope = Scope.InvolvedScope(context, _scopes.Keys);
            if (scope is null)
                throw new InvalidInvokeException($"{context} isn't in scope of {this}");
            if (!_scopes.TryGetValue(scope, out var container))
                lock (_scopes)
                    if (!_scopes.TryGetValue(scope, out container))
                    {
                        _scopes[scope] = container = new ScopeContainer();
                        container.Dispose += () => _scopes.Remove(scope);
                    }
            container.Providers.Add(provider);

            if (!container.Dependencies.TryGetValue(type, out var dependency))
                lock (container)
                    if (!container.Dependencies.TryGetValue(type, out dependency))
                        if (TryGetValue(type, container.Dependencies.Values, out dependency!))
                            container.Dependencies[type] = dependency;
                        else
                            dependency = ConstructInstance(context, type, dependency => container.Dependencies[type] = dependency);

            if (scopeProvider is not null)
                scopeProvider.Scope.OnDisposed += OnScopeDisposed;

            return Construct(scope, dependency);
        }

        protected virtual IDependency Construct(IScope scope, object dependency)
        {
            return new ScopeDependency(scope, dependency);
        }


        private void OnScopeDisposed(object? scope, EventArgs _)
        {
            Dispose(((IDependencyScope)scope!).Provider);
        }

        public override void Dispose(IDependencyProvider provider)
        {
            var scope = provider.GetScope();

            if (_scopes.TryGetValue(scope, out var container))
                lock (_scopes)
                {
                    if (container.Providers.Remove(provider))
                    {
                        if (provider is IScopeDependencyProvider scopeProvider)
                            scopeProvider.Scope.OnDisposed -= OnScopeDisposed;

                        if (container.Providers.Count == 0)
                            lock (container)
                            {
                                container.Dispose?.Invoke(); // remove scopes

                                if (DisposeAutomatically)
                                    foreach (var s in container.Dependencies)
                                        if (s.Value is IDisposable d)
                                            d.Dispose();
                            }
                    }
                }
        }


        protected class ScopeContainer
        {


            public Action? Dispose { get; set; }

            public IDictionary<Type, object> Dependencies { get; }

            public ISet<IDependencyProvider> Providers { get; }


            public ScopeContainer()
            {
                Dependencies = new Dictionary<Type, object>();
                Providers = new HashSet<IDependencyProvider>();
            }


        }


    }
}
