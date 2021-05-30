using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface ITypeDependencySourceBuilder : ITypeDependencyBuilder, IDependencySourceBuilder
    {


        public new ITypeDependencySourceBuilder As(Type type);


    }
}
