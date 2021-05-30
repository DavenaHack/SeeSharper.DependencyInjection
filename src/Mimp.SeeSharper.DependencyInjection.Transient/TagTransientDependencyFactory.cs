using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Transient
{
    public class TagTransientDependencyFactory : TransientDependencyFactory, ITagDependencyFactory
    {


        public Func<ITagDependencyContext, Type, bool> IsTagged { get; }


        public TagTransientDependencyFactory(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, object> factory,
            Func<ITagDependencyContext, Type, bool> isTagged,
            bool disposeAutomatically
        ) : base(constructible, factory, disposeAutomatically)
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
