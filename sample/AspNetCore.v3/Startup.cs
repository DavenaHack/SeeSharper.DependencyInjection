using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Extensions.Configuration;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using ServiceLibrary;

namespace AspNetCore.v3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();
        }

        public void ConfigureContainer(IDependencySourceBuilder builder)
        {
            builder.UseScopeProvider(provider => new HttpHeaderScopeProvider(provider.GetDependencyRequired<IHttpContextAccessor>()));

            builder.AddScoped<IFooService>(_ => new FooService("foo"));
            builder.AddSingleton<IBarService>(_ => new BarService("bar"));

            builder.AddScoped<IFooService>(_ => new FooService("foo1"))
                .AddScope("scope1");
            builder.AddScopedSource(sourceBuilder =>
                sourceBuilder.AddSingleton<IBarService>(_ => new BarService("bar1")),
                "scope1"
            );

            builder.AddConfigureSource(new ConfigurationBuilder().AddJsonFile("scope2.json").Build());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
