using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class SubScopeTest
    {


        [TestMethod]
        public void TestScope()
        {
            var factory = new ScopeFactory();
            IScope scope(object scope) =>
                factory.CreateScope(scope);

            var root = scope("root");
            var parent0 = root.Sub(scope("parent0"));
            var parent1 = root.Sub(scope("parent1"));
            var scope00 = parent0.Sub(scope("scope0"));
            var scope10 = parent1.Sub(scope("scope0"));


            Assert.IsTrue(parent0.In(scope("root").Sub(scope("parent0"))));
            Assert.IsTrue(scope("root").In(parent0));
            Assert.IsTrue(scope("root").In(parent1));
            Assert.IsTrue(scope("root").In(scope00));
            Assert.IsTrue(scope("root").In(scope10));
            Assert.IsTrue(root.Sub(scope("parent0")).In(scope00));
            Assert.IsTrue(root.Sub(scope("parent1")).In(scope10));
            Assert.IsFalse(root.Sub(scope("parent0")).In(scope10));
            Assert.IsFalse(root.Sub(scope("parent1")).In(scope00));
            Assert.IsFalse(scope("scope0").In(scope00));
            Assert.IsFalse(scope("scope0").In(scope10));
            Assert.IsFalse(scope("parent0").In(scope00));
            Assert.IsFalse(scope("parent1").In(scope10));
            Assert.IsTrue(scope("root").In(scope00));
            Assert.IsTrue(scope("root").In(scope10));

        }


    }
}