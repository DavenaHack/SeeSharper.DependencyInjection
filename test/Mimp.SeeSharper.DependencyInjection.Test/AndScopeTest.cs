using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class AndScopeTest
    {


        [TestMethod]
        public void TestScope()
        {
            var factory = new ScopeFactory();
            IScope scope(object scope) =>
                factory.CreateScope(scope);

            var scope1 = scope("1");

            var and012 = scope("0")
                .And(scope1)
                .And(scope("2"));

            var and10 = scope1
                .And(scope("0"));
            var and02 = scope("0")
                .And(scope("2"));

            Assert.IsFalse(and012.In(scope("3")));
            Assert.IsFalse(scope1.In(and10));
            Assert.IsFalse(scope1.In(and012));
            Assert.IsFalse(scope1.In(and02));
            Assert.IsFalse(and012.In(scope1));
            Assert.IsFalse(and012.In(and10));
            Assert.IsFalse(and012.In(and02));
            Assert.IsFalse(and10.In(and012));
            Assert.IsFalse(and02.In(and012));
            Assert.IsTrue(and012.In(and10.And(scope("2"))));
            Assert.IsTrue(and012.In(and02.And(scope("1"))));
        }

    }
}