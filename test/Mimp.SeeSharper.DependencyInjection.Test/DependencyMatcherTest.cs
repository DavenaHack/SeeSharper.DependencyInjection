using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class DependencyMatcherTest
    {


        [TestMethod]
        public void TestMatch()
        {
            var matcher = new DependencyMatcher();

            var factories = new IDependencyFactory[]
            {
                new MockDependencyFactory(),
                new MockDependencyFactory(true),
                new MockDependencyFactory(),
                new MockDependencyFactory(true),
                new MockDependencyFactory(true),
                new MockDependencyFactory(),
                new MockDependencyFactory(),
                new MockDependencyFactory(true),
                new MockDependencyFactory(true),
                new MockDependencyFactory(true),
            };
            var provider = new MockDependencyProvider();

            Assert.IsTrue(matcher.Match(provider, new DependencyContext(provider, typeof(object)), typeof(object), factories).All(f => ((MockDependencyFactory)f).IsConstructible));
        }


    }
}
