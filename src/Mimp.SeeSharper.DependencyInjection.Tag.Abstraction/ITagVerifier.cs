using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public interface ITagVerifier
    {


        public bool HasTag(ITagDependencyContext context, Type type, IEnumerable<KeyValuePair<Type?, object>> tags);


    }
}