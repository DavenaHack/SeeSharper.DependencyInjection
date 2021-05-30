using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeVerifier
    {


        public bool HasScope(IDependencyProvider provider, object? scope);


    }
}
