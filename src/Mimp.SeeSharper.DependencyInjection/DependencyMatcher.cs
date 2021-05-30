using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class DependencyMatcher : IDependencyMatcher
    {


        public virtual IEnumerable<IDependencyFactory> Match(IDependencyProvider provider, IDependencyContext context, Type dependencyType, IEnumerable<IDependencyFactory> factories)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (factories is null)
                throw new ArgumentNullException(nameof(factories));

            foreach (var factory in factories)
                if (factory.Constructible(context, dependencyType))
                    yield return factory;
        }


    }
}
