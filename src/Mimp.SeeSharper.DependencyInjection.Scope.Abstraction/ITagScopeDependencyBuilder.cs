using Mimp.SeeSharper.DependencyInjection.Tag.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface ITagScopeDependencyBuilder : IScopeDependencyBuilder, ITagDependencyBuilder
    {


        public new ITagScopeDependencyBuilder AddScope(object? scope);

        public new ITagScopeDependencyBuilder Tag(object tag);


    }
}
