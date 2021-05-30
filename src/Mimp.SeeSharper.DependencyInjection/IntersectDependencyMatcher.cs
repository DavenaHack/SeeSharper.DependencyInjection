using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class IntersectDependencyMatcher : IDependencyMatcher
    {


        public IEnumerable<IDependencyMatcher> Matchers { get; }


        public IntersectDependencyMatcher(IEnumerable<IDependencyMatcher> matchers)
        {
            Matchers = matchers?.ToArray() ?? throw new ArgumentNullException(nameof(matchers));
            if (Matchers.Any(m => m is null))
                throw new ArgumentNullException(nameof(matchers), "At least on matcher is null");
        }

        public IntersectDependencyMatcher(params IDependencyMatcher[] matchers)
            : this((IEnumerable<IDependencyMatcher>)matchers) { }


        public IEnumerable<IDependencyFactory> Match(IDependencyProvider provider, IDependencyContext context, Type dependencyType, IEnumerable<IDependencyFactory> factories)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (factories is null)
                throw new ArgumentNullException(nameof(factories));

            return Matchers.Select(m => m.Match(provider, context, dependencyType, factories))
                .Aggregate((a, b) => a.Intersect(b));
        }


    }
}
