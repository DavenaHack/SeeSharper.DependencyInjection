using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection;

namespace AspNetCore.v3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new DependencyServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
