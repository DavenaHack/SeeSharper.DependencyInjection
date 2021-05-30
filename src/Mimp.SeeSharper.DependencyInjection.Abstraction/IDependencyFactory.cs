using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface IDependencyFactory
    {


        public bool Constructible(IDependencyContext context, Type type);

        public IDependency Construct(IDependencyContext context, Type type);

        public void Dispose(IDependencyProvider provider);


    }
}
