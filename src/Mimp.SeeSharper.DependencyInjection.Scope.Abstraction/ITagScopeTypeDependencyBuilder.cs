using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface ITagScopeTypeDependencyBuilder : ITagScopeDependencyBuilder, ITagTypeDependencyBuilder, IScopeTypeDependencyBuilder
    {


        public new ITagScopeTypeDependencyBuilder AddScope(object? scope);

        public new ITagScopeTypeDependencyBuilder Tag(object tag);

        public new ITagScopeTypeDependencyBuilder As(Type type);

        public new ITagScopeTypeDependencyBuilder As(object tag, Type type);


    }
}
