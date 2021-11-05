using Microsoft.Extensions.Configuration;
using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.Configuration.Abstraction
{
    public interface IConfigurationDependencyFactoryBuilder
    {


        public IDependencyFactory GetFactory(IDependencyProvider provider, IConfiguration rootConfiguration, IConfiguration dependencyConfiguration);


    }
}
