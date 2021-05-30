using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection
{
    public static class DependencyMatcherExtensions
    {


        public static IDependencyMatcher Union(this IDependencyMatcher matcher, IEnumerable<IDependencyMatcher> matchers)
        {
            if (matcher is null)
                throw new ArgumentNullException(nameof(matcher));
            if (matchers is null)
                throw new ArgumentNullException(nameof(matchers));

            return matcher.Union(matchers.ToArray());
        }

        public static IDependencyMatcher Union(this IDependencyMatcher matcher, params IDependencyMatcher[] matchers)
        {
            if (matcher is null)
                throw new ArgumentNullException(nameof(matcher));

            return new UnionDependencyMatcher(new[] { matcher }.Concat(matchers));
        }


        public static IDependencyMatcher Intersect(this IDependencyMatcher matcher, IEnumerable<IDependencyMatcher> matchers)
        {
            if (matcher is null)
                throw new ArgumentNullException(nameof(matcher));
            if (matchers is null)
                throw new ArgumentNullException(nameof(matchers));

            return matcher.Intersect(matchers.ToArray());
        }

        public static IDependencyMatcher Intersect(this IDependencyMatcher matcher, params IDependencyMatcher[] matchers)
        {
            if (matcher is null)
                throw new ArgumentNullException(nameof(matcher));

            return new IntersectDependencyMatcher(new[] { matcher }.Concat(matchers));
        }


    }
}
