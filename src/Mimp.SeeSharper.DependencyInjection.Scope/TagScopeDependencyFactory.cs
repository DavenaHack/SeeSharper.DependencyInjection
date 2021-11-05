using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class TagScopeDependencyFactory : ScopeDependencyFactory, ITagDependencyFactory
    {


        public Func<ITagDependencyContext, Type, bool> IsTagged { get; }


        public TagScopeDependencyFactory(
            Func<IDependencyContext, Type, bool> constructible,
            Func<IDependencyContext, Type, Action<object>, object> factory,
            IScope scope,
            Func<ITagDependencyContext, Type, bool> isTagged,
            bool disposeAutomatically
        ) : base(constructible, factory, scope, disposeAutomatically)
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
