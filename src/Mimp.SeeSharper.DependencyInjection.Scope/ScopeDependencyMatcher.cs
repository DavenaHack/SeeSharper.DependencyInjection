using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class ScopeDependencyMatcher : IDependencyMatcher
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

            IScope? scope = null;
            foreach (var factory in factories)
                if (factory is not IScopeDependencyFactory scopeFactory
                    || scopeFactory.Scope.In(scope ??= context.GetScope()))
                    yield return factory;
        }


    }
}
