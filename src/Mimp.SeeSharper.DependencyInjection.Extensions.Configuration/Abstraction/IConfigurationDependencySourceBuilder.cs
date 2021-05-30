using Microsoft.Extensions.Configuration;
using Mimp.SeeSharper.DependencyInjection.Abstraction;

namespace Mimp.SeeSharper.DependencyInjection.Extensions.Configuration.Abstraction
{
    public interface IConfigurationDependencySourceBuilder
    {


        public IDependencySource GetSource(IConfiguration rootConfiguration, IConfiguration sourceConfiguration);


    }
}
