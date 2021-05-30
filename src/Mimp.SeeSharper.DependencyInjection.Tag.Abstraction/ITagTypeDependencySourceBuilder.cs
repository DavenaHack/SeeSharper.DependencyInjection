using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public interface ITagTypeDependencySourceBuilder : ITagTypeTagDependencyBuilder, ITagDependencySourceBuilder, ITypeDependencySourceBuilder
    {


        public new ITagTypeDependencySourceBuilder Tag(object tag);

        public new ITagTypeDependencySourceBuilder As(Type type);

        public new ITagTypeDependencySourceBuilder As(object tag, Type type);


    }
}
