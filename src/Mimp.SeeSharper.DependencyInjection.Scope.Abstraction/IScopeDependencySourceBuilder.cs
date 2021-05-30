using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeDependencySourceBuilder : IScopeDependencyBuilder, IDependencySourceBuilder
    {


        public new IScopeDependencySourceBuilder AddScope(object? scope);


    }
}
