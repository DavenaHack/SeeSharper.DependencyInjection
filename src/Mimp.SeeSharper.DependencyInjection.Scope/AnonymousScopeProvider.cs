using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class AnonymousScopeProvider : BaseScopeProvider
    {


        protected override IScope CreateScope(IDependencyScope scope)
        {
            return scope.Provider.CreateScope(new object());
        }


    }
}
