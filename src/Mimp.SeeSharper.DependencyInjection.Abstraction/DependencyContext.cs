using System;
using System.Collections.Generic;

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


        public override bool Equals(object? obj)
        {
            return obj is DependencyContext context &&
                   EqualityComparer<Type>.Default.Equals(DependencyType, context.DependencyType) &&
                   EqualityComparer<IDependencyProvider>.Default.Equals(Provider, context.Provider);
        }


        public override int GetHashCode()
        {
            int hashCode = 1559281912;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(DependencyType);
            hashCode = hashCode * -1521134295 + EqualityComparer<IDependencyProvider>.Default.GetHashCode(Provider);
            return hashCode;
        }


        public override string? ToString()
        {
            return $"{GetType().Name} {{ {nameof(DependencyType)} = {DependencyType} }}";
        }


    }
}
