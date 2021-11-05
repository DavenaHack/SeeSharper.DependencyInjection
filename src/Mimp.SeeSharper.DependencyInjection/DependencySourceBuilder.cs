using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class DependencySourceBuilder : IDependencySourceBuilder
    {


        private ICollection<Func<IDependencyProvider, IDependencyFactory>> _factories;
        private readonly ICollection<Func<IDependencyProvider, IDependencySource>> _sources;


        public DependencySourceBuilder()
        {
            _factories = new List<Func<IDependencyProvider, IDependencyFactory>>();
            _sources = new List<Func<IDependencyProvider, IDependencySource>>();
        }


        public IDependencySourceBuilder AddDependency(Func<IDependencyProvider, IDependencyFactory> factory)
        {
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            _factories.Add(factory);

            return this;
        }

        public IDependencySourceBuilder AddSource(Func<IDependencyProvider, IDependencySource> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            FlushFactories();
            _sources.Add(source);

            return this;
        }


        protected void FlushFactories()
        {
            if (_factories.Count > 0)
                lock (_factories)
                {
                    var factories = _factories;
                    _sources.Add(provider => new DependencySource(factories.Select(f => f(provider))));
                    _factories = new List<Func<IDependencyProvider, IDependencyFactory>>();
                }
        }


        public IDependencySource BuildSource(IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            FlushFactories();
            return new UnionDependencySource(_sources.Select(s => s(provider)));
        }


    }
}
