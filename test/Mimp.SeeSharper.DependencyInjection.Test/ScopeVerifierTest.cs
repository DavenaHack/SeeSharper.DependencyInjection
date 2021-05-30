using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class ScopeVerifierTest
    {


        [TestMethod]
        public void TestHasScope()
        {
            var verifier = new ScopeVerifier();

            var scope = new object();
            var parent = new MockDependencyProvider();

            Assert.IsTrue(verifier.HasScope(parent, null));
            Assert.IsFalse(verifier.HasScope(parent, scope));

            using var dependencyScope = new DependencyScope(_ => scope, scope => new ScopeDependencyProvider(scope, parent));

            Assert.IsTrue(verifier.HasScope(dependencyScope.Provider, null));
            Assert.IsTrue(verifier.HasScope(dependencyScope.Provider, scope));
            Assert.IsFalse(verifier.HasScope(dependencyScope.Provider, new object()));
        }


        [TestMethod]
        public void TestHasSuperScope()
        {
            var verifier = new ScopeVerifier(false);
            var superVerifier = new ScopeVerifier();

            var superScope = new object();
            using var dependencySubscope = new DependencyScope(_ => superScope, scope => new ScopeDependencyProvider(scope, new MockDependencyProvider()));
            using var dependencyScope = new DependencyScope(_ => new object(), scope => new ScopeDependencyProvider(scope, dependencySubscope.Provider));

            Assert.IsFalse(verifier.HasScope(dependencyScope.Provider, superScope));
            Assert.IsTrue(superVerifier.HasScope(dependencyScope.Provider, superScope));
        }


    }
}
