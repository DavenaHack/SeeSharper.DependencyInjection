using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class ScopeDependencyFactoryTest
    {


        [TestMethod]
        public void TestScope()
        {
            var factory = new ScopeFactory();
            var scope1 = factory.CreateScope(new object());
            var scope2 = factory.CreateScope(new object());
            var factory0 = new ScopeDependencyFactory((_, t) => t == typeof(object), (_, _, _) => new DisposeObject(), Scopes.Any, true);
            var factory1 = new ScopeDependencyFactory((_, t) => t == typeof(DisposeObject), (_, _, _) => new DisposeObject(), scope1, true);
            var factory2 = new ScopeDependencyFactory((_, t) => t == typeof(DisposeObject), (_, _, _) => new DisposeObject(), scope2, true);
            var factory12 = new ScopeDependencyFactory((_, t) => t == typeof(IDisposable), (_, _, _) => new DisposeObject(), new SeparateOrScope(scope1, scope2), true);

            using var root = new DependencyProvider(
                new DependencySource(new IDependencyFactory[] {
                    factory0,
                    factory1,
                    factory2,
                    factory12
                }),
                new DependencyMatcher()
                    .Intersect(new ScopeDependencyMatcher()),
                new LastDependencySelector(),
                new DependencyInvoker()
            );
            var scopeFactory = new DependencyScopeFactory();

            using var scope10 = scopeFactory.CreateDependencyScope(scope1, root);
            using var scope11 = scopeFactory.CreateDependencyScope(scope1, root);
            using var scope20 = scopeFactory.CreateDependencyScope(scope2, root);

            // null scope
            var value = root.ProvideRequired<object>().Dependency;
            Assert.AreSame(value, root.ProvideRequired<object>().Dependency);
            Assert.AreNotSame(value, scope10.Provider.ProvideRequired<object>().Dependency);
            value = scope10.Provider.ProvideRequired<object>().Dependency;
            Assert.AreSame(value, scope10.Provider.ProvideRequired<object>().Dependency);
            Assert.AreSame(value, scope11.Provider.ProvideRequired<object>().Dependency);
            Assert.AreNotSame(value, scope20.Provider.ProvideRequired<object>().Dependency);

            // diffrent scopes
            Assert.IsNull(root.Provide<DisposeObject>());
            value = scope10.Provider.ProvideRequired<DisposeObject>().Dependency;
            Assert.AreSame(value, scope11.Provider.ProvideRequired<DisposeObject>().Dependency);
            Assert.AreNotSame(value, scope20.Provider.ProvideRequired<DisposeObject>().Dependency);

            // multiple scopes
            value = scope10.Provider.ProvideRequired<IDisposable>().Dependency;
            Assert.AreSame(value, scope11.Provider.ProvideRequired<IDisposable>().Dependency);
            Assert.AreNotSame(value, scope20.Provider.ProvideRequired<IDisposable>().Dependency);
        }


        [TestMethod]
        public void TestDispose()
        {
            var scopFactory = new ScopeFactory();
            var scope = scopFactory.CreateScope(new object());
            var factory = new ScopeDependencyFactory((_, t) => t == typeof(DisposeObject), (_, _, _) => new DisposeObject(), scope, true);

            using var root = new DependencyProvider(
                new DependencySource(new IDependencyFactory[] {
                    factory,
                }),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );
            var depScopeFactory = new DependencyScopeFactory();

            DisposeObject dispose;

            using (var dependencyScope = depScopeFactory.CreateDependencyScope(scope, root))
            {
                dispose = (DisposeObject)dependencyScope.Provider.ProvideRequired<DisposeObject>().Dependency;

                Assert.IsFalse(dispose.Disposed);

                using (var s = depScopeFactory.CreateDependencyScope(scope, root))
                    s.Provider.ProvideRequired<DisposeObject>();
                Assert.IsFalse(dispose.Disposed);
            }
            Assert.IsTrue(dispose.Disposed);

            using (var dependencyScope = depScopeFactory.CreateDependencyScope(scope, root))
                Assert.AreNotSame(dispose, dependencyScope.Provider.ProvideRequired<DisposeObject>().Dependency);
        }


    }
}
