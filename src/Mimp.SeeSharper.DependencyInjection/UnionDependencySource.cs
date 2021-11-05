using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class UnionDependencySource : IDependencySource
    {


        public IEnumerable<IDependencySource> Sources { get; }


        public UnionDependencySource(IEnumerable<IDependencySource> sources)
        {
            Sources = sources?.Select(s => s ?? throw new ArgumentNullException(nameof(sources), "At least on source is null."))?.ToArray()
                ?? throw new ArgumentNullException(nameof(sources));
        }

        public UnionDependencySource(params IDependencySource[] sources)
            : this((IEnumerable<IDependencySource>)sources) { }


        public IEnumerable<IDependencyFactory> GetFactories(IDependencyProvider provider, IDependencyContext context) =>
            Sources.Select(source => source.GetFactories(provider, context)).Aggregate((a, b) => a.Union(b));


        public void Dispose(IDependencyProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            foreach (var source in Sources)
                source.Dispose(provider);
        }

    }
}
