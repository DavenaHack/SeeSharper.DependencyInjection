namespace Mimp.SeeSharper.DependencyInjection.Abstraction
{
    public interface IDependencyInvoker
    {


        /// <summary>
        /// Invoke the <paramref name="factory"/> and return the <see cref="IDependency"/>.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="context"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        /// <exception cref="InvalidInvokeException">Will throw if the <see cref="IDependencyInvoker"/> can't invoke the <paramref name="factory"/>.</exception>
        public IDependency Invoke(IDependencyProvider provider, IDependencyContext context, IDependencyFactory factory);


    }
}
