using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class FirstDependencySelector : IDependencySelector
    {


        public IDependencyFactory? Select(IDependencyProvider provider, IDependencyContext context, IEnumerable<IDependencyFactory> factories)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (factories is null)
                throw new ArgumentNullException(nameof(factories));

            return factories.FirstOrDefault();
        }


    }
}
