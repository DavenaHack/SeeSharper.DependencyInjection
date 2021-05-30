using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class DependencySourceBuilder : IDependencySourceBuilder
    {


        private ICollection<Func<IDependencyFactory>> _factories;
        private readonly ICollection<Func<IDependencySource>> _sources;


        public DependencySourceBuilder()
        {
            _factories = new List<Func<IDependencyFactory>>();
            _sources = new List<Func<IDependencySource>>();
        }


        public IDependencySourceBuilder AddDependency(Func<IDependencyFactory> factory)
        {
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            _factories.Add(factory);

            return this;
        }

        public IDependencySourceBuilder AddSource(Func<IDependencySource> source)
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
                    _sources.Add(() => new DependencySource(factories.Select(f => f())));
                    _factories = new List<Func<IDependencyFactory>>();
                }
        }


        public IDependencySource BuildSource()
        {
            FlushFactories();
            return new UnionDependencySource(_sources.Select(s => s()));
        }


    }
}
