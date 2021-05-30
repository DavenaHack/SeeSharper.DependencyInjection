using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class LastDependencySelectorTest
    {


        [TestMethod]
        public void TestSelect()
        {
            var selector = new LastDependencySelector();

            var last = new MockDependencyFactory();
            var factories = new IDependencyFactory[]
            {
                new MockDependencyFactory(),
                new MockDependencyFactory(),
                new MockDependencyFactory(),
                new MockDependencyFactory(),
                new MockDependencyFactory(),
                last,
            };
            var provider = new MockDependencyProvider();

            Assert.AreSame(last, selector.Select(provider, new DependencyContext(provider, typeof(object)), factories));
        }


    }
}
