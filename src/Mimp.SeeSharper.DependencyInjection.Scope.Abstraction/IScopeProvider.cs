namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeProvider
    {


        public object GetScope(IDependencyScope scope);


    }
}
