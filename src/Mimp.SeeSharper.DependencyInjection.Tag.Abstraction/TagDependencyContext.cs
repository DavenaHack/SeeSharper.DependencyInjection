using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

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


        public override string? ToString()
        {
            return $"{GetType().Name} {{ {nameof(DependencyType)} = {DependencyType}, {nameof(Tag)} = {Tag} }}";
        }


    }
}
