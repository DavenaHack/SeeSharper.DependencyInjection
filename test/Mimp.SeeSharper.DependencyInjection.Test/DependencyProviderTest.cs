using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class DependencyProviderTest
    {


        [TestMethod]
        public void TestProvide()
        {
            var result = new BaseDependency(new());

            var provider = new DependencyProvider(
                new DependencySource(new IDependencyFactory[] {
                    new MockDependencyFactory(),
                    new MockDependencyFactory(true),
                    new MockDependencyFactory(),
                    new MockDependencyFactory(true),
                    new MockDependencyFactory(true),
                    new MockDependencyFactory(),
                    new MockDependencyFactory(),
                    new MockDependencyFactory(true),
                    new MockDependencyFactory(true),
                    new MockDependencyFactory(true, result),
                }),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );

            Assert.AreSame(result, provider.Provide<object>());
        }


    }
}
