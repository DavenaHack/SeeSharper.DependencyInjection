using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class EmptyDependencyProvider : IDependencyProvider
    {


        public IDependency? Provide(IDependencyContext context) => null;


    }
}
