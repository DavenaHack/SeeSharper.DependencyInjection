using System;

namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public class DependencyContext : IDependencyContext
    {


        public Type DependencyType { get; }

        public IDependencyProvider Provider { get; }


        public DependencyContext(IDependencyProvider provider, Type dependencyType)
        {
            DependencyType = dependencyType ?? throw new ArgumentNullException(nameof(dependencyType));
            Provider = provider ?? throw new ArgumentNullException(nameof(dependencyType));
        }


        public override string? ToString()
        {
            return $"{GetType().Name} {{ {nameof(DependencyType)} = {DependencyType} }}";
        }


    }
}
