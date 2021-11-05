using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeTypeDependencyBuilder : IScopeDependencyBuilder, ITypeDependencyBuilder
    {


        public new IScopeTypeDependencyBuilder As(Type type);

        public new IScopeTypeDependencyBuilder AddScope(Func<IDependencyProvider, IScope> scope);


    }
}
