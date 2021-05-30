using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeDependencyFactory : IDependencyFactory
    {


        public object? Scope { get; }


    }
}
