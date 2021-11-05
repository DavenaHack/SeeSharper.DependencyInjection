using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public interface ITagTypeTagDependencySourceBuilder : ITagTypeTagDependencyBuilder, ITagDependencySourceBuilder, ITypeDependencySourceBuilder
    {


        public new ITagTypeTagDependencySourceBuilder Tag(Func<IDependencyProvider, object> tag);

        public new ITagTypeTagDependencySourceBuilder As(Type type);

        public new ITagTypeTagDependencySourceBuilder As(Func<IDependencyProvider, object> tag, Type type);


    }
}
