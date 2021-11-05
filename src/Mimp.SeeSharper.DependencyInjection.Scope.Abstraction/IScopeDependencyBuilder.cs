using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeDependencyBuilder : IDependencyBuilder
    {


        public IScopeDependencyBuilder AddScope(Func<IDependencyProvider, IScope> scope);


    }
}
