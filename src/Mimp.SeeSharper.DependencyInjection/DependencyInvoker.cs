using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class DependencyInvoker : IDependencyInvoker
    {


        public virtual IDependency Invoke(IDependencyProvider provider, IDependencyContext context, IDependencyFactory factory)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));

            try
            {
                return factory.Construct(context, context.DependencyType);
            }
            catch (Exception ex) when (ex is not InvalidInvokeException)
            {
                throw new InvalidInvokeException($"{factory} failed to construct with {context}.", ex);
            }
        }


    }
}
