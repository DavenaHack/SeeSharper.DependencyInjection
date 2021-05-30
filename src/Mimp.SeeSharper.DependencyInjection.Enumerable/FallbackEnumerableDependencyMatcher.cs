using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.Reflection;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Enumerable
{
    public class FallbackEnumerableDependencyMatcher : IDependencyMatcher
    {


        public IDependencyMatcher Matcher { get; }

        public IDependencyMatcher ValueMatcher { get; }


        public FallbackEnumerableDependencyMatcher(IDependencyMatcher matcher, IDependencyMatcher valueMatcher)
        {
            Matcher = matcher ?? throw new ArgumentNullException(nameof(matcher));
            ValueMatcher = valueMatcher ?? throw new ArgumentNullException(nameof(valueMatcher));
        }

        public FallbackEnumerableDependencyMatcher(IDependencyMatcher matcher)
            : this(matcher, matcher) { }


        public IEnumerable<IDependencyFactory> Match(IDependencyProvider provider, IDependencyContext context, Type dependencyType, IEnumerable<IDependencyFactory> factories)
        {
            var hasEnumerable = false;

            foreach (var factory in Matcher.Match(provider, context, dependencyType, factories))
            {
                hasEnumerable = true;
                yield return factory;
            }

            if (!hasEnumerable && dependencyType.IsIEnumerable())
            {
                var valueType = dependencyType.GetIEnumerableValueType()!;
                var valueFactories = new List<IDependencyFactory>();
                foreach (var factory in ValueMatcher.Match(provider, context, valueType, factories))
                    valueFactories.Add(factory);
                yield return new EnumerableDependencyFactory(valueFactories);
            }
        }


    }
}
