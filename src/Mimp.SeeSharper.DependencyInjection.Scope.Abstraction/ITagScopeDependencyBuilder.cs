using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface ITagScopeDependencyBuilder : IScopeDependencyBuilder, ITagDependencyBuilder
    {


        public new ITagScopeDependencyBuilder AddScope(Func<IDependencyProvider, IScope> scope);

        public new ITagScopeDependencyBuilder Tag(Func<IDependencyProvider, object> tag);


    }
}
