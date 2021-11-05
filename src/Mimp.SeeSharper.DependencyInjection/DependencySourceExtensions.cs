using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection
{
    public static class DependencySourceExtensions
    {


        public static IDependencySource Union(this IDependencySource source, IEnumerable<IDependencySource> sources)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (sources is null)
                throw new ArgumentNullException(nameof(sources));

            return new UnionDependencySource(new[] { source }.Concat(sources));
        }

        public static IDependencySource Union(this IDependencySource source, params IDependencySource[] sources)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return source.Union((IEnumerable<IDependencySource>)sources);
        }


    }
}
