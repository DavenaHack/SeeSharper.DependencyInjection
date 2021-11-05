using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface ITagScopeDependencySourceBuilder : ITagScopeDependencyBuilder, IScopeDependencySourceBuilder, ITagDependencySourceBuilder
    {


        public new ITagScopeDependencySourceBuilder AddScope(Func<IDependencyProvider, IScope> scope);

        public new ITagScopeDependencySourceBuilder Tag(Func<IDependencyProvider, object> tag);


    }
}
