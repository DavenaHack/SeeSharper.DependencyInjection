using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Singleton
{
    public class SingletonDependencyFactory : BaseDependencyFactory
    {


        private readonly IDictionary<Type, object> _singletons;

        private readonly ISet<IDependencyProvider> _providers;

        public bool DisposeAutomatically { get; }


        public SingletonDependencyFactory(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, Action<object>, object> factory,
            bool disposeAutomatically
        ) : base(constructible, factory)
        {
            DisposeAutomatically = disposeAutomatically;
            _singletons = new Dictionary<Type, object>();
            _providers = new HashSet<IDependencyProvider>();
        }


        public override IDependency Construct(IDependencyContext context, Type type)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            ThrowIfIsNotConstructible(context, type);

            var provider = context.Provider;
            _providers.Add(provider);

            if (!_singletons.TryGetValue(type, out var singleton))
                lock (_singletons)
                    if (!_singletons.TryGetValue(type, out singleton))
                        if (TryGetValue(type, _singletons.Values, out singleton!))
                            _singletons[type] = singleton;
                        else
                            singleton = ConstructInstance(context, type, singleton => _singletons[type] = singleton);

            return Construct(singleton);
        }


        protected virtual IDependency Construct(object singleton)
        {
            return new SingletonDependency(singleton);
        }


        public override void Dispose(IDependencyProvider provider)
        {
            lock (_providers)
            {
                if (_providers.Remove(provider) && _providers.Count == 0)
                    lock (_singletons)
                    {
                        if (DisposeAutomatically)
                            foreach (var s in _singletons)
                                if (s.Value is IDisposable d)
                                    d.Dispose();
                        _singletons.Clear();
                    }
            }
        }


    }
}
