using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeTypeDependencySourceBuilder : IScopeTypeDependencyBuilder, IScopeDependencySourceBuilder, ITypeDependencySourceBuilder
    {


        public new IScopeTypeDependencySourceBuilder As(Type type);

        public new IScopeTypeDependencySourceBuilder AddScope(Func<IDependencyProvider, IScope> scope);


    }
}
