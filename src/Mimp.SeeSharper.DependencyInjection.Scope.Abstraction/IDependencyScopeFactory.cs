namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IDependencyScopeFactory
    {


        public IDependencyScope CreateDependencyScope(IScopeContext context);


    }
}
