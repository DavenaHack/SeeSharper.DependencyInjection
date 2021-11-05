using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class ScopeTest
    {


        [TestMethod]
        public void TestScope()
        {
            var factory = new ScopeFactory();
            IScope scope(object scope) =>
                factory.CreateScope(scope);

            var scope0 = scope("scope");
            var scope1 = scope("scope");
            var scope2 = scope("notScope");

            Assert.IsTrue(scope0.In(scope1));
            Assert.IsFalse(scope0.In(scope2));
        }


        [TestMethod]
        public void TestComplexScope()
        {
            var factory = new ScopeFactory();
            IScope scope(object scope) =>
                factory.CreateScope(scope);

            var xor__Or_And03_2__And_Or13_0 =
                new XorScope(new[] {
                    scope("0").And(scope("3")).Or(scope("2")),
                    scope("1").Or(scope("3")).And(scope("0"))
                });


            Assert.IsFalse(xor__Or_And03_2__And_Or13_0.In(scope("4")));
            Assert.IsTrue(xor__Or_And03_2__And_Or13_0.In(scope("2")));
            Assert.IsTrue(xor__Or_And03_2__And_Or13_0.In(scope("1").Or(scope("0"))));
            Assert.IsTrue(xor__Or_And03_2__And_Or13_0.In(scope("1").And(scope("0"))));
            Assert.IsTrue(xor__Or_And03_2__And_Or13_0.In(scope("1").Xor(scope("0"))));
            Assert.IsFalse(xor__Or_And03_2__And_Or13_0.In(scope("3").And(scope("0"))));
            Assert.IsFalse(xor__Or_And03_2__And_Or13_0.In(scope("3").Or(scope("0"))));
            Assert.IsFalse(xor__Or_And03_2__And_Or13_0.In(scope("3").Xor(scope("0"))));
            Assert.IsTrue(xor__Or_And03_2__And_Or13_0.In(scope("1").Or(scope("0").Or(scope("4")))));
            Assert.IsFalse(xor__Or_And03_2__And_Or13_0.In(scope("1").And(scope("0").And(scope("4")))));
            Assert.IsTrue(xor__Or_And03_2__And_Or13_0.In(scope("1").Xor(scope("0").Xor(scope("4")))));


        }

    }
}