namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScopeFactory
    {


        public IScope CreateScope(object? scope);


    }
}
