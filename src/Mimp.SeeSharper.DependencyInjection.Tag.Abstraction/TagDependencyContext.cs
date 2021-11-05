using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public class TagDependencyContext : DependencyContext, ITagDependencyContext
    {


        public object Tag { get; }


        public TagDependencyContext(object tag, IDependencyProvider provider, Type dependencyType)
            : base(provider, dependencyType)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }


        public override bool Equals(object? obj)
        {
            return obj is TagDependencyContext context &&
                   base.Equals(obj) &&
                   EqualityComparer<Type>.Default.Equals(DependencyType, context.DependencyType) &&
                   EqualityComparer<IDependencyProvider>.Default.Equals(Provider, context.Provider) &&
                   EqualityComparer<object>.Default.Equals(Tag, context.Tag);
        }


        public override int GetHashCode()
        {
            int hashCode = -863085855;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(DependencyType);
            hashCode = hashCode * -1521134295 + EqualityComparer<IDependencyProvider>.Default.GetHashCode(Provider);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Tag);
            return hashCode;
        }


        public override string? ToString()
        {
            return $"{GetType().Name} {{ {nameof(DependencyType)} = {DependencyType}, {nameof(Tag)} = {Tag} }}";
        }


    }
}
