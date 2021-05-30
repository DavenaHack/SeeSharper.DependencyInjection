using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Tag
{
    public abstract class BaseTagDependencyFactory : BaseDependencyFactory, ITagDependencyFactory
    {


        public Func<ITagDependencyContext, Type, bool> IsTagged { get; }


        protected BaseTagDependencyFactory(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, Action<object>, object> factory,
            Func<ITagDependencyContext, Type, bool> isTagged
        ) : base(constructible, factory)
        {
            IsTagged = isTagged ?? throw new ArgumentNullException(nameof(isTagged));
        }


        public bool Tagged(ITagDependencyContext context, Type type)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            return IsTagged(context, type);
        }


    }
}
