using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface IDependencyContext
    {


        public Type DependencyType { get; }

        public IDependencyProvider Provider { get; }


    }
}
