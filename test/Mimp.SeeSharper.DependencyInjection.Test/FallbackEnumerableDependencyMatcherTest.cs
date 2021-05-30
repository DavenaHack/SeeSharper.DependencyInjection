using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Enumerable;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class FallbackEnumerableDependencyMatcherTest
    {


        [TestMethod]
        public void TestMatch()
        {
            var matcher = new FallbackEnumerableDependencyMatcher(new DependencyMatcher());

            var enumerable = new MockTypeDependencyFactory(typeof(IEnumerable<object>), new BaseDependency(Array.Empty<object>()));
            var strings = new IDependencyFactory[]
            {
                new MockTypeDependencyFactory(typeof(string), new BaseDependency("string0")),
                new MockTypeDependencyFactory(typeof(string), new BaseDependency("string1")),
                new MockTypeDependencyFactory(typeof(string), new BaseDependency("string2")),
                new MockTypeDependencyFactory(typeof(string), new BaseDependency("string3")),
                new MockTypeDependencyFactory(typeof(string), new BaseDependency("string4")),
            };

            var factories = new IDependencyFactory[]
            {
                new MockDependencyFactory(),
                enumerable,
                new MockDependencyFactory(),
                strings[0],
                strings[1],
                new MockDependencyFactory(),
                new MockDependencyFactory(),
                strings[2],
                strings[3],
                strings[4],
            };
            var provider = new MockDependencyProvider();

            Assert.IsTrue(matcher.Match(provider, new DependencyContext(provider, typeof(IEnumerable<object>)), typeof(IEnumerable<object>), factories).All(f => f == enumerable));
            Assert.IsTrue(matcher.Match(provider, new DependencyContext(provider, typeof(IEnumerable<string>)), typeof(IEnumerable<string>), factories).All(f => f is EnumerableDependencyFactory e && e.Factories.All(f => strings.Contains(f))));
        }


    }
}
