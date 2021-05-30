namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface IDependencyProvider
    {


        /// <summary>
        /// A matching <see cref="IDependency"/> will return with use of <paramref name="context"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="DependencyInjectionException"></exception>
        public IDependency? Provide(IDependencyContext context);


    }
}
