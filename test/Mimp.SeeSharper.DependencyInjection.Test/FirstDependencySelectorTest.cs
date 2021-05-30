using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class FirstDependencySelectorTest
    {


        [TestMethod]
        public void TestSelect()
        {
            var selector = new FirstDependencySelector();

            var first = new MockDependencyFactory();
            var factories = new IDependencyFactory[]
            {
                first,
                new MockDependencyFactory(),
                new MockDependencyFactory(),
                new MockDependencyFactory(),
                new MockDependencyFactory(),
                new MockDependencyFactory(),
            };
            var provider = new MockDependencyProvider();

            Assert.AreSame(first, selector.Select(provider, new DependencyContext(provider, typeof(object)), factories));
        }


    }
}
