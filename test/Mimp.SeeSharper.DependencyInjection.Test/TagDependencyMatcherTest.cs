using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class TagDependencyMatcherTest
    {


        [TestMethod]
        public void TestMatch()
        {
            var matcher = new TagDependencyMatcher();

            var tag = new object();
            var factories = new IDependencyFactory[]
            {
                new MockTagDependencyFactory(new ()),
                new MockDependencyFactory(true),
                new MockDependencyFactory(),
                new MockTagDependencyFactory(tag, true),
                new MockDependencyFactory(true),
                new MockDependencyFactory(),
                new MockTagDependencyFactory(tag),
                new MockDependencyFactory(true),
                new MockTagDependencyFactory(new(), true),
                new MockDependencyFactory(true),
            };
            var provider = new MockDependencyProvider();

            Assert.IsTrue(matcher.Match(provider, new TagDependencyContext(tag, provider, typeof(object)), typeof(object), factories).All(f => ReferenceEquals(((MockTagDependencyFactory)f).Tag, tag)));
        }


    }
}
