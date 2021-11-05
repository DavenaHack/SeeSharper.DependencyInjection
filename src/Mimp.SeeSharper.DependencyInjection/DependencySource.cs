using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class DependencySource : IDependencySource
    {


        private readonly IEnumerable<IDependencyFactory> _factories;


        public DependencySource(IEnumerable<IDependencyFactory> factories)
        {
            _factories = factories?.Select(f => f ?? throw new ArgumentNullException(nameof(factories), "At least one factory is null"))?.ToArray() 
                ?? throw new ArgumentNullException(nameof(factories));
        }

        public DependencySource(params IDependencyFactory[] factories)
            : this((IEnumerable<IDependencyFactory>)factories) { }


        public IEnumerable<IDependencyFactory> GetFactories(IDependencyProvider provider, IDependencyContext context) =>
            _factories;


        public void Dispose(IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            foreach (var factory in _factories)
                factory.Dispose(provider);
        }


    }
}
