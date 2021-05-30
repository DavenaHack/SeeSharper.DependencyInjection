using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface IDependencySource
    {


        public IEnumerable<IDependencyFactory> GetFactories(IDependencyProvider provider, IDependencyContext context);


        public void Dispose(IDependencyProvider provider);


    }
}
