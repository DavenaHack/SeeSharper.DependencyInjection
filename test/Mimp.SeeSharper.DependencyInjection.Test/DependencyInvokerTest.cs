using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class DependencyInvokerTest
    {


        [TestMethod]
        public void TestInvoke()
        {
            var invoker = new DependencyInvoker();

            var result = new BaseDependency(new());
            var factory = new MockDependencyFactory(true, result);
            var provider = new MockDependencyProvider();

            Assert.AreSame(result, invoker.Invoke(provider, new DependencyContext(provider, typeof(object)), factory));
        }


    }
}
