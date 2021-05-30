using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
#if NullableAttributes
using System.Diagnostics.CodeAnalysis;
#endif

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeDependencyFactory : BaseDependencyFactory
    {


        private readonly IDictionary<object, ScopeContainer> _scopes;


        public Action<IDependencyProvider, Action<IScopeVerifier>> Verifier { get; }

        public Func<IDependencyContext, IScopeVerifier, Type, bool> Scope { get; }

        public bool DisposeAutomatically { get; }


        public ScopeDependencyFactory(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, Action<object>, object> factory,
            Action<IDependencyProvider, Action<IScopeVerifier>> verifier,
            Func<IDependencyContext, IScopeVerifier, Type, bool> scope,
            bool disposeAutomatically
        ) : base(constructible, factory)
        {
            Verifier = verifier ?? throw new ArgumentNullException(nameof(verifier));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            DisposeAutomatically = disposeAutomatically;
            _scopes = new Dictionary<object, ScopeContainer>();
        }


        public override bool Constructible(IDependencyContext context, Type type)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return IsConstructible(context, type) && IsScope(context, type);
        }

        protected bool IsScope(IDependencyContext context, Type type)
        {
            return Verify(context.Provider, verifier => Scope(context, verifier, type));
        }


        public override IDependency Construct(IDependencyContext context, Type type)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            ThrowIfIsNotScope(context, type);
            ThrowIfIsNotConstructible(context, type);

            var provider = context.Provider;
            var scopeProvider = provider is IScopeDependencyProvider sp ? sp : null;
            var scope = scopeProvider is null ? provider : scopeProvider.Scope.Scope;

            if (!_scopes.TryGetValue(scope, out var container))
                lock (_scopes)
                    if (!_scopes.TryGetValue(scope, out container))
                    {
                        if (TryGetScope(provider, _scopes.Keys, out var s))
                            _scopes[scope] = container = _scopes[s!];
                        else
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

        protected virtual IDependency Construct(object scope, object dependency)
        {
            return new ScopeDependency(scope, dependency);
        }


        private void OnScopeDisposed(object? scope, EventArgs _)
        {
            Dispose(((IDependencyScope)scope!).Provider);
        }

        public override void Dispose(IDependencyProvider provider)
        {
            var scopeProvider = provider is IScopeDependencyProvider sp ? sp : null;
            var scope = scopeProvider is null ? provider : scopeProvider.Scope.Scope;

            if (_scopes.TryGetValue(scope, out var container))
                lock (_scopes)
                    if (container.Providers.Remove(provider))
                    {
                        if (scopeProvider is not null)
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


        protected T Verify<T>(IDependencyProvider provider, Func<IScopeVerifier, T> verfiy)
        {
            var called = false;
            T? result = default;

            Verifier(provider, verifier =>
            {
                called = true;
                result = verfiy(verifier ?? throw new InvalidOperationException("Verifier is null."));
            });
            if (!called)
                throw new InvalidOperationException("Verifier never call delegate.");

            return result!;
        }

        protected void Verify(IDependencyProvider provider, Action<IScopeVerifier> verfiy) =>
            Verify<object?>(provider, verifier =>
            {
                verfiy(verifier);
                return null;
            });


        protected void ThrowIfIsNotScope(IDependencyContext context, Type type)
        {
            if (!IsScope(context, type))
                throw new InvalidInvokeException($"{context.Provider} is't in scope of {this}");
        }


        protected bool TryGetScope(
            IDependencyProvider provider,
            IEnumerable<object> scopes,
#if NullableAttributes 
            [NotNullWhen(true)]
#endif
            out object? scope
        )
        {
            object? result = null;
            if (Verify(provider, verifier =>
            {
                foreach (var s in scopes)
                    if (verifier.HasScope(provider, s))
                    {
                        result = s;
                        return true;
                    }
                return false;
            }))
            {
                scope = result!;
                return true;
            }
            else
            {
                scope = null;
                return false;
            }
        }


        public static Func<IDependencyContext, IScopeVerifier, Type, bool> IsScopes(IEnumerable<object?> scopes)
        {
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            scopes = scopes.ToArray();

            return (context, verifier, _) => scopes.Any(s => verifier.HasScope(context.Provider, s));
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
