using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeDependencyBuilder : IDependencyBuilder
    {


        public IScopeDependencyBuilder AddScope(object? scope);


    }
}
