using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface IDependencySelector
    {


        public IDependencyFactory? Select(IDependencyProvider provider, IDependencyContext context, IEnumerable<IDependencyFactory> factories);


    }
}
