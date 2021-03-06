using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public interface ITagTypeDependencyBuilder : ITypeDependencyBuilder
    {


        public new ITagTypeDependencyBuilder As(Type type);

        public ITagTypeDependencyBuilder As(Func<IDependencyProvider, object> tag, Type type);


    }
}
