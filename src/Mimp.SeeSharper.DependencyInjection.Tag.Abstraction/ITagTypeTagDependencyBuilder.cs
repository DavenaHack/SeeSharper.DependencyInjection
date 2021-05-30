using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public interface ITagTypeTagDependencyBuilder : ITagDependencyBuilder, ITagTypeDependencyBuilder
    {


        public new ITagTypeTagDependencyBuilder Tag(object tag);

        public new ITagTypeTagDependencyBuilder As(Type type);

        public new ITagTypeTagDependencyBuilder As(object tag, Type type);


    }
}
