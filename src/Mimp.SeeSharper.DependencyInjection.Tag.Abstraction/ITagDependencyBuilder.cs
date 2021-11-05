using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public interface ITagDependencyBuilder : IDependencyBuilder
    {


        public ITagDependencyBuilder Tag(Func<IDependencyProvider, object> tag);


    }
}
