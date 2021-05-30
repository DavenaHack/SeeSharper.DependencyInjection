using System;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class DisposeDependency : BaseDependency, IDisposable
    {


        public override object Dependency
        {
            get
            {
                ThrowIfObjectDisposed();
                return base.Dependency;
            }
        }


        public DisposeDependency(object dependency)
            : base(dependency) { }


        #region IDisposable


        protected bool _disposed;


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (Dependency is IDisposable d)
                        d.Dispose();
                }

                _disposed = true;
            }
        }

        ~DisposeDependency()
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
