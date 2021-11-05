using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class XorScopeTest
    {


        [TestMethod]
        public void TestScope()
        {
            var factory = new ScopeFactory();
            IScope scope(object scope) =>
                factory.CreateScope(scope);

            var scope1 = scope("1");

            var xor012 = scope("0")
                .Xor(scope1)
                .Xor(scope("2"));

            var xor10 = scope1
                .Xor(scope("0"));
            var xor02 = scope("0")
                .Xor(scope("2"));

            Assert.IsFalse(xor012.In(scope("3")));
            Assert.IsTrue(scope1.In(xor10));
            Assert.IsTrue(scope1.In(xor012));
            Assert.IsFalse(scope1.In(xor02));
            Assert.IsTrue(xor012.In(scope1));
            Assert.IsFalse(xor012.In(xor10));
            Assert.IsFalse(xor012.In(xor02));
            Assert.IsFalse(xor10.In(xor012));
            Assert.IsFalse(xor02.In(xor012));
            Assert.IsFalse(xor012.In(xor10.Xor(scope("2"))));
            Assert.IsFalse(xor012.In(xor02.Xor(scope("1"))));
        }

    }
}