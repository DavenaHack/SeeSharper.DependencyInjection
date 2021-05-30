using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Test.Mock
{
    public class MockTagDependencyFactory : MockDependencyFactory, ITagDependencyFactory
    {


        public object Tag { get; }


        public MockTagDependencyFactory(object tag, bool constructible = true, IDependency? dependency = null)
            : base(constructible, dependency)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }


        public bool Tagged(ITagDependencyContext context, Type type)
        {
            return context.Tag == Tag;
        }


    }
}
