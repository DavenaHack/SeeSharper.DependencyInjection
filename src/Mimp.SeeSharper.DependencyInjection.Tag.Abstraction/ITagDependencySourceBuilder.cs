using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public interface ITagDependencySourceBuilder : ITagDependencyBuilder, IDependencySourceBuilder, IDependencyBuilder
    {


        public new ITagDependencySourceBuilder Tag(Func<IDependencyProvider, object> tag);


    }
}
