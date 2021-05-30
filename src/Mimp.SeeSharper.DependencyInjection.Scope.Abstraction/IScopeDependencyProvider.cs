using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeDependencyProvider : IDependencyProvider
    {


        public IDependencyProvider Parent { get; }

        public IDependencyScope Scope { get; }


    }
}
