using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface IDependencyMatcher
    {


        public IEnumerable<IDependencyFactory> Match(IDependencyProvider provider, IDependencyContext context, Type dependencyType, IEnumerable<IDependencyFactory> factories);


    }
}
