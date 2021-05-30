using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class SingletonDependencyFactoryTest
    {


        [TestMethod]
        public void TestSingleton()
        {
            var singleton = new SingletonDependencyFactory((_, _) => true, (_, _, _) => new DisposeObject(), true);

            using var provider1 = new DependencyProvider(
                new DependencySource(new IDependencyFactory[] {
                    singleton
                }),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );
            using var provider2 = new DependencyProvider(
                new DependencySource(new IDependencyFactory[] {
                    singleton,
                }),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );

            var value = provider1.ProvideRequired<DisposeObject>().Dependency;
            Assert.AreSame(value, provider1.ProvideRequired<DisposeObject>().Dependency);
            Assert.AreSame(value, provider2.ProvideRequired<DisposeObject>().Dependency);
            Assert.AreSame(value, provider1.ProvideRequired<object>().Dependency);
        }


        [TestMethod]
        public void TestDispose()
        {
            DisposeObject? firstDispose = null, dispose = null;
            var manual = new DisposeObject();
            var disposeSingleton = new SingletonDependencyFactory(
                (_, t) => t == typeof(DisposeObject),
                (_, _, _) => dispose = new DisposeObject(),
                true);
            var manualSingleton = new SingletonDependencyFactory(
                (_, t) => t == typeof(object),
                (_, _, _) => manual,
                false);


            using var provider1 = new DependencyProvider(
                new DependencySource(new IDependencyFactory[] {
                    disposeSingleton,
                    manualSingleton
                }),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );
            using var provider2 = new DependencyProvider(
                new DependencySource(new IDependencyFactory[] {
                    disposeSingleton,
                    manualSingleton
                }),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );
            using var provider3 = new DependencyProvider(
                new DependencySource(new IDependencyFactory[] {
                    disposeSingleton,
                    manualSingleton
                }),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );

            firstDispose = (DisposeObject)provider1.ProvideRequired<DisposeObject>().Dependency;
            provider1.Provide<object>();
            provider2.Provide<DisposeObject>();
            provider2.Provide<object>();

            provider1.Dispose();
            Assert.IsFalse(dispose!.Disposed);
            Assert.IsFalse(manual.Disposed);

            provider2.Dispose();
            Assert.IsTrue(dispose.Disposed);
            Assert.IsFalse(manual.Disposed);

            manual.Dispose();
            Assert.IsTrue(manual.Disposed);

            provider3.Provide<DisposeObject>();
            provider3.Provide<object>();

            Assert.AreNotSame(firstDispose, dispose);
            Assert.IsTrue(firstDispose.Disposed);
            Assert.IsFalse(dispose.Disposed);
            Assert.IsTrue(manual.Disposed);

            provider3.Dispose();
            Assert.IsTrue(dispose.Disposed);
        }


    }
}
