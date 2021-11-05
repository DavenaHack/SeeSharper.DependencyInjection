using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class DependencyScopeFactoryTest
    {


        [TestMethod]
        public void TestCreateScope()
        {
            var factory = new DependencyScopeFactory();

            var provider = new MockDependencyProvider();
            var scope = new ScopeFactory().CreateScope(new object());
            using var dependencyScope = factory.CreateDependencyScope(scope, provider);

            Assert.AreSame(scope, dependencyScope.Scope);
            Assert.AreSame(provider, dependencyScope.Provider.Parent);
        }


    }
}
