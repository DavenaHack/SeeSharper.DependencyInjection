using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Test.Mock
{
    public class MockDependencyProvider : IDependencyProvider
    {
        public IDependency? Provide(IDependencyContext context) => null;
    }
}
