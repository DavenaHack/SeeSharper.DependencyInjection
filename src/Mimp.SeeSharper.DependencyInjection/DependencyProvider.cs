using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class DependencyProvider : IDependencyProvider, IDisposable
    {


        public IDependencySource Source { get; }

        public IDependencyMatcher Matcher { get; }

        public IDependencySelector Selector { get; }

        public IDependencyInvoker Invoker { get; }


        public DependencyProvider(IDependencySource source, IDependencyMatcher matcher, IDependencySelector selector, IDependencyInvoker invoker)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Matcher = matcher ?? throw new ArgumentNullException(nameof(matcher));
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
            Invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        }


        public virtual IDependency? Provide(IDependencyContext context)
        {
            ThrowIfObjectDisposed();
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            var factories = Matcher.Match(this, context, context.DependencyType, Source.GetFactories(this, context));
            if (factories is null)
                return null;

            var factory = Selector.Select(this, context, factories);
            if (factory is null)
                return null;

            return Invoker.Invoke(this, context, factory);
        }


        #region IDisposable


        protected bool _disposed;


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Source.Dispose(this);
                }

                _disposed = true;
            }
        }

        ~DependencyProvider()
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
