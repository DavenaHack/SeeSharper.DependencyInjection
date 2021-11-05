using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Tag
{
    public class TagDependencyMatcher : IDependencyMatcher
    {


        public virtual IEnumerable<IDependencyFactory> Match(IDependencyProvider provider, IDependencyContext context, Type dependencyType, IEnumerable<IDependencyFactory> factories)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (dependencyType is null)
                throw new ArgumentNullException(nameof(dependencyType));
            if (factories is null)
                throw new ArgumentNullException(nameof(factories));

            if (context is ITagDependencyContext tag)
            {
                foreach (var factory in factories)
                    if (factory is ITagDependencyFactory tagFactory && tagFactory.Tagged(tag, dependencyType))
                        yield return tagFactory;
            }
            else
            {
                foreach (var factory in factories)
                    yield return factory;
            }
        }


    }
}
