using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class DependencyScope : IDependencyScope
    {


        public event EventHandler? OnDisposing;

        public event EventHandler? OnDisposed;


        private readonly Func<IDependencyScope, object> _scope;
        public object Scope { get => _scope(this); }

        public IScopeDependencyProvider Provider { get; }


        public DependencyScope(Func<IDependencyScope, object> scope, Func<IDependencyScope, IScopeDependencyProvider> providerFactory)
        {
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Provider = (providerFactory ?? throw new ArgumentNullException(nameof(providerFactory)))(this) ?? throw new ArgumentException("Factory return null.", nameof(providerFactory));
        }



        #region IDisposable


        protected bool _disposed;


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    OnDisposing?.Invoke(this, EventArgs.Empty);
                    if (Scope is IDisposable sd)
                        sd.Dispose();
                    if (Provider is IDisposable pd)
                        pd.Dispose();
                    OnDisposed?.Invoke(this, EventArgs.Empty);
                }

                _disposed = true;
            }
        }

        ~DependencyScope()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        protected void ThrowIfObjectDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }


        #endregion


    }
}
