namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IDependencyScopeFactory
    {


        public IDependencyScope CreateScope(IDependencyScopeContext context);


    }
}
