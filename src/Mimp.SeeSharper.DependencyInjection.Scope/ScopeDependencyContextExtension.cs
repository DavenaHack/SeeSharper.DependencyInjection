using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public static class ScopeDependencyContextExtension
    {


        public static IScope GetScope(this IDependencyContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            return context.Provider.GetScope();
        }


    }
}
