using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Enumerable
{
    public class EnumerableDependency : DisposeDependency
    {


        public IEnumerable<IDependency> Dependencies { get; }


        public EnumerableDependency(object dependency, IEnumerable<IDependency> dependencies)
            : base(dependency)
        {
            Dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
            if (Dependencies.Any(d => d is null))
                throw new ArgumentNullException(nameof(dependencies), "At least one dependeny is null.");
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_disposed)
                foreach (var dependency in Dependencies)
                    if (dependency is IDisposable d)
                        d.Dispose();
        }

    }
}
