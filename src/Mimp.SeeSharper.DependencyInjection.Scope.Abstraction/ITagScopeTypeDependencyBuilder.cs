using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface ITagScopeTypeDependencyBuilder : ITagScopeDependencyBuilder, ITagTypeTagDependencyBuilder, IScopeTypeDependencyBuilder
    {


        public new ITagScopeTypeDependencyBuilder AddScope(Func<IDependencyProvider, IScope> scope);

        public new ITagScopeTypeDependencyBuilder Tag(Func<IDependencyProvider, object> tag);

        public new ITagScopeTypeDependencyBuilder As(Type type);

        public new ITagScopeTypeDependencyBuilder As(Func<IDependencyProvider, object> tag, Type type);


    }
}
