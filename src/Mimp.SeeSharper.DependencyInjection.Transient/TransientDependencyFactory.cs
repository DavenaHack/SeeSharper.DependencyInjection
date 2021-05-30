using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Transient
{
    public class TransientDependencyFactory : BaseDependencyFactory
    {


        private readonly IDictionary<IDependencyProvider, ICollection<IDependency>> _transients;


        public bool DisposeAutomatically { get; }


        public TransientDependencyFactory(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, object> factory,
            bool disposeAutomatically
        ) : base(constructible, factory is null
            ? throw new ArgumentNullException(nameof(factory))
            : (Func<IDependencyContext, Type, Action<object>, object>)((context, type, _) => factory(context, type)))
        {
            DisposeAutomatically = disposeAutomatically;
            _transients = new Dictionary<IDependencyProvider, ICollection<IDependency>>();
        }


        public override IDependency Construct(IDependencyContext context, Type type)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            ThrowIfIsNotConstructible(context, type);

            var provider = context.Provider;
            if (!_transients.TryGetValue(provider, out var transients))
                lock (_transients)
                    if (!_transients.TryGetValue(provider, out transients))
                        _transients[provider] = transients = new List<IDependency>();

            var transient = Construct(ConstructInstance(context, type, _ => { }), t =>
            {
                if (_transients.ContainsKey(provider)) // false - disposing or already done
                    transients.Remove(t);
            });
            transients.Add(transient);

            return transient;
        }


        protected virtual IDependency Construct(object transient, Action<IDependency> dispose)
        {
            return new TransientDependency(transient, dispose);
        }


        public override void Dispose(IDependencyProvider provider)
        {
            lock (_transients)
            {
                if (DisposeAutomatically && _transients.TryGetValue(provider, out var transients))
                {
                    _transients.Remove(provider);
                    foreach (var transient in transients)
                        if (transient is IDisposable d)
                            d.Dispose();
                }
                else
                    _transients.Remove(provider);
            }
        }


    }
}
