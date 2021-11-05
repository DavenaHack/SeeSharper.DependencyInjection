using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class OrScopeTest
    {


        [TestMethod]
        public void TestScope()
        {
            var factory = new ScopeFactory();
            IScope scope(object scope) =>
                factory.CreateScope(scope);

            var scope1 = scope("1");

            var or012 = scope("0")
                .Or(scope1)
                .Or(scope("2"));

            var or10 = scope1
                .Or(scope("0"));
            var or02 = scope("0")
                .Or(scope("2"));

            Assert.IsFalse(or012.In(scope("3")));
            Assert.IsTrue(scope1.In(or10));
            Assert.IsTrue(scope1.In(or012));
            Assert.IsFalse(scope1.In(or02));
            Assert.IsTrue(or012.In(scope1));
            Assert.IsTrue(or012.In(or10));
            Assert.IsTrue(or012.In(or02));
            Assert.IsTrue(or10.In(or012));
            Assert.IsTrue(or02.In(or012));
            Assert.IsTrue(or012.In(or10.Or(scope("2"))));
            Assert.IsTrue(or012.In(or02.Or(scope("1"))));
        }

    }
}