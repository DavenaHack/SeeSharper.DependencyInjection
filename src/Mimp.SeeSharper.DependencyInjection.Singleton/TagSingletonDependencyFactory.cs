using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Singleton
{
    public class TagSingletonDependencyFactory : SingletonDependencyFactory, ITagDependencyFactory
    {


        public Func<ITagDependencyContext, Type, bool> IsTagged { get; }


        public TagSingletonDependencyFactory(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, Action<object>, object> factory,
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
