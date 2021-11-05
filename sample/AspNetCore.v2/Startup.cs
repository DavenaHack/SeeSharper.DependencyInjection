using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mimp.SeeSharper.DependencyInjection;
using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Extensions.Configuration;
using Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection;
using Mimp.SeeSharper.DependencyInjection.Instantiation;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Singleton;
using ServiceLibrary;
using System;

namespace AspNetCore.v2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHttpContextAccessor();

            return services.BuildDependencyServiceProvider(builder =>
            {
                builder.UseScopeProvider(provider => new HttpHeaderScopeProvider(provider.GetDependencyRequired<IHttpContextAccessor>()));

                builder.AddScoped<IFooService>(_ => new FooService("foo"));
                builder.AddSingleton<IBarService>(_ => new BarService("bar"));

                builder.AddScoped<IFooService>(_ => new FooService("foo1"))
                    .AddScope(provider => provider.CreateScope("scope1"));

                builder.AddSource(provider =>
                {
                    var sourceBuilder = new DependencySourceBuilder();
                    sourceBuilder.AddSingleton<IBarService>(_ => new BarService("bar1"));
                    return sourceBuilder.BuildSource(provider).Scoped(provider.CreateScope("scope1"));
                });

                builder.AddScoped<IFooService>(_ => new FooService("foo11"))
                    .AddScope(provider => provider.CreateScope("scope1").Sub(provider.CreateScope("subscope1")));

                builder.AddConfigureSource(new ConfigurationBuilder().AddJsonFile("scope2.json").Build());
                builder.AddConfigureSource(new ConfigurationBuilder().AddJsonFile("scope22.json").Build());

                return builder;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
