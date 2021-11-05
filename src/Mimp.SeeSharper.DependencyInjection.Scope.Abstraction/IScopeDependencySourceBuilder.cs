using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeDependencySourceBuilder : IScopeDependencyBuilder, IDependencySourceBuilder
    {


        public new IScopeDependencySourceBuilder AddScope(Func<IDependencyProvider, IScope> scope);


    }
}
