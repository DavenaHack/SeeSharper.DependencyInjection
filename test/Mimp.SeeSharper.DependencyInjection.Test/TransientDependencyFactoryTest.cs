using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;
using Mimp.SeeSharper.DependencyInjection.Transient;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class TransientDependencyFactoryTest
    {


        [TestMethod]
        public void TestTransient()
        {
            var transient = new TransientDependencyFactory((_, _) => true, (_, _) => new DisposeObject(), true);

            using var provider = new DependencyProvider(
                new DependencySource(new IDependencyFactory[] {
                    transient
                }),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );

            provider.Use<DisposeObject>(value =>
                provider.Use<DisposeObject>(other =>
                   Assert.AreNotSame(value, other)
                )
            );
        }


        [TestMethod]
        public void TestDispose()
        {
            DisposeObject? dispose = null, manual = null;
            var disposeTransient = new TransientDependencyFactory(
                (_, t) => t == typeof(DisposeObject),
                (_, _) => dispose = new DisposeObject(),
                true);
            var manualTransient = new TransientDependencyFactory(
                (_, t) => t == typeof(object),
                (_, _) => manual = new DisposeObject(),
                false);


            using var provider = new DependencyProvider(
                new DependencySource(new IDependencyFactory[] {
                    disposeTransient,
                    manualTransient
                }),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );

            provider.Use<DisposeObject>(_ => { });
            provider.Use<object>(_ => { });
            Assert.IsTrue(dispose!.Disposed);
            Assert.IsTrue(manual!.Disposed);

            provider.Provide<DisposeObject>();
            var transient = provider.ProvideRequired<object>();
            Assert.IsFalse(dispose.Disposed);
            Assert.IsFalse(manual.Disposed);

            provider.Dispose();
            Assert.IsTrue(dispose.Disposed);
            Assert.IsFalse(manual.Disposed);

            ((IDisposable)transient).Dispose();
            Assert.IsTrue(manual.Disposed);
        }


    }
}
