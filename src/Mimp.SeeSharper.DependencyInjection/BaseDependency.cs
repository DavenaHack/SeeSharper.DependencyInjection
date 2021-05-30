using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection
{
    public class BaseDependency : IDependency
    {


        public virtual object Dependency { get; }


        public BaseDependency(object dependency)
        {
            Dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
        }


    }
}
