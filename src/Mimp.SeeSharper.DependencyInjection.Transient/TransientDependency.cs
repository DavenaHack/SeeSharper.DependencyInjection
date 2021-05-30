using System;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class TransientDependency : DisposeDependency
    {


        private readonly Action<TransientDependency> _dispose;


        public TransientDependency(object dependency, Action<TransientDependency> dispose)
            : base(dependency)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
        }


        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
                _dispose(this);
            base.Dispose(disposing);
        }


    }
}
