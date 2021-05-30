using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Tag.Abstraction
{
    public interface ITagDependencyBuilder : IDependencyBuilder
    {


        public ITagDependencyBuilder Tag(object tag);


    }
}
