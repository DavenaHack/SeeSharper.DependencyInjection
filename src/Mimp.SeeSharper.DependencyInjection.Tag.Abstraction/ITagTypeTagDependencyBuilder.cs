using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public interface ITagTypeTagDependencyBuilder : ITagDependencyBuilder, ITagTypeDependencyBuilder
    {


        public new ITagTypeTagDependencyBuilder Tag(Func<IDependencyProvider, object> tag);

        public new ITagTypeTagDependencyBuilder As(Type type);

        public new ITagTypeTagDependencyBuilder As(Func<IDependencyProvider, object> tag, Type type);


    }
}
