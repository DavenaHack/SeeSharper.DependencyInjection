using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Instantiation;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using Mimp.SeeSharper.DependencyInjection.Test.Mock;
using Mimp.SeeSharper.ObjectDescription;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Test
{
    [TestClass]
    public class InstantiationTest
    {


        [TestMethod]
        public void TestInstantiation()
        {
            var stringValue = "instantiate";

            using var provider = new DependencyProvider(
                new DependencySourceBuilder()
                    .AddSingleton<DisposeObject>()
                    .AddSingletonInstantiation<ObjectWithDependency>(ObjectDescriptions.EmptyDescription
                        .Append(nameof(ObjectWithDependency.StringProperty), stringValue))
                    .UseInstantiator()
                    .BuildSource(),
                new DependencyMatcher(),
                new LastDependencySelector(),
                new DependencyInvoker()
            );

            var disposeObject = provider.ProvideRequired<DisposeObject>().Dependency;
            var obj = (ObjectWithDependency)provider.ProvideRequired<ObjectWithDependency>().Dependency;

            Assert.AreSame(disposeObject, obj.DisposeObject);
            Assert.AreSame(stringValue, obj.StringProperty);
            Assert.AreEqual(ObjectWithDependency.DefaultInt, obj.IntProperty);
        }


    }
}
