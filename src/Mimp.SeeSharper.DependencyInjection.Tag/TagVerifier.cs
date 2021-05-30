using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Tag
{
    public class TagVerifier : ITagVerifier
    {


        public Func<ITagDependencyContext, Type?, object, bool> IsTag { get; }


        public TagVerifier(Func<ITagDependencyContext, Type?, object, bool> isTag)
        {
            IsTag = isTag ?? throw new ArgumentNullException(nameof(isTag));
        }

        public TagVerifier()
            : this(GetDefaultIsTag(BaseDependencyFactory.ConstructibleType, (t1, t2) => Equals(t1, t2))) { }


        public bool HasTag(ITagDependencyContext context, Type type, IEnumerable<KeyValuePair<Type?, object>> tags)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (tags is null)
                throw new ArgumentNullException(nameof(tags));

            foreach (var pair in tags)
                if (IsTag(context, type, pair.Value))
                    return true;

            return false;
        }


        public static Func<ITagDependencyContext, Type?, object, bool> GetDefaultIsTag(Func<Type, Type, bool> typeEquals, Func<object, object, bool> tagEquals)
        {
            if (typeEquals is null)
                throw new ArgumentNullException(nameof(typeEquals));
            if (tagEquals is null)
                throw new ArgumentNullException(nameof(tagEquals));

            return (context, type, tag) =>
            {
                if (context is null)
                    throw new ArgumentNullException(nameof(context));
                if (tag is null)
                    throw new ArgumentNullException(nameof(tag));

                return (type is null || typeEquals(context.DependencyType, type))
                    && tagEquals(context.Tag, tag);
            };
        }


    }
}
