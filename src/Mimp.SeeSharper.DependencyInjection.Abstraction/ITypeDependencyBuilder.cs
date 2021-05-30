using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface ITypeDependencyBuilder : IDependencyBuilder
    {


        public Type Type { get; }


        public ITypeDependencyBuilder As(Type type);


    }
}
