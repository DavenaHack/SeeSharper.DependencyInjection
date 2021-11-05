using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class ScopeDependencySourceTest
    {


        [TestMethod]
        public void TestGetFactories()
        {
            var scope = new ScopeFactory().CreateScope(new object());
            var source = new DependencySource(new IDependencyFactory[] {
                    new MockDependencyFactory(true)
            }).Scoped(scope);

            var provider = new DependencyProvider(
                new DependencySourceBuilder().UseScope().BuildSource(new EmptyDependencyProvider()),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );

            Assert.IsFalse(source.GetFactories(provider, new DependencyContext(provider, typeof(object))).Any());

            using (var depScope = provider.CreateDependencyScope(scope))
            {
                Assert.IsTrue(source.GetFactories(provider, new DependencyContext(depScope.Provider, typeof(object))).Any());
                Assert.IsTrue(source.GetFactories(depScope.Provider, new DependencyContext(depScope.Provider, typeof(object))).Any());
            }
        }


    }
}
