using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag
{
    public interface ITagDependencyFactory : IDependencyFactory
    {


        public bool Tagged(ITagDependencyContext context, Type type);


    }
}
