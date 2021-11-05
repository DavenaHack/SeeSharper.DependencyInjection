namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeProvider
    {


        public IScope GetScope(IDependencyScope scope);


    }
}
