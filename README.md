# SeeShraper DependencyInjection [![build and test](https://img.shields.io/github/workflow/status/DavenaHack/SeeSharper.DependencyInjection/build%20and%20test?label=build%20and%20test&logo=github)](https://github.com/DavenaHack/SeeSharper.DependencyInjection/actions/workflows/BuildAndTest.yml) [![NuGet Mimp.SeeSharper.DependencyInjection.Abstraction](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Abstraction?label=Mimp.SeeSharper.DependencyInjection.Abstraction&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Abstraction/) [![NuGet Mimp.SeeSharper.DependencyInjection.Singleton](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Singleton?label=Mimp.SeeSharper.DependencyInjection.Singleton&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Singleton/) [![NuGet Mimp.SeeSharper.DependencyInjection.Transient](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Transient?label=Mimp.SeeSharper.DependencyInjection.Transient&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Transient/) [![NuGet Mimp.SeeSharper.DependencyInjection.Scope.Abstraction](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Scope.Abstraction?label=Mimp.SeeSharper.DependencyInjection.Scope.Abstraction&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Scope.Abstraction/) [![NuGet Mimp.SeeSharper.DependencyInjection.Scope](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Scope?label=Mimp.SeeSharper.DependencyInjection.Scope&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Scope/) [![NuGet Mimp.SeeSharper.DependencyInjection.Tag.Abstraction](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Tag.Abstraction?label=Mimp.SeeSharper.DependencyInjection.Tag.Abstraction&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Tag.Abstraction/) [![NuGet Mimp.SeeSharper.DependencyInjection.Tag](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Tag?label=Mimp.SeeSharper.DependencyInjection.Tag&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Tag/) [![NuGet Mimp.SeeSharper.DependencyInjection](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection?label=Mimp.SeeSharper.DependencyInjection&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection/) [![NuGet Mimp.SeeSharper.DependencyInjection.Enumerable](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Enumerable?label=Mimp.SeeSharper.DependencyInjection.Enumerable&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Enumerable/) [![NuGet Mimp.SeeSharper.DependencyInjection.Instantiation](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Instantiation?label=Mimp.SeeSharper.DependencyInjection.Instantiation&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Instantiation/) [![NuGet Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection?label=Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection/) [![NuGet Mimp.SeeSharper.DependencyInjection.Extensions.Configuration](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Extensions.Configuration?label=Mimp.SeeSharper.DependencyInjection.Extensions.Configuration&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Extensions.Configuration/)

*SeeSharper DependencyInjection* is a component from the framework *[SeeSharper](https://github.com/DavenaHack/SeeSharper)*. *SeeSharper DependencyInjection* is a IoC container which work with IoC pattern. It support to plug in to other frameworks like Asp.Net.


## Get started

The construct is kept very abstract and may seem confusing for the beginning. So that everyone can get an insight into the framework, here are the basic explained briefly and concisely. It exist some [samples](https://github.com/DavenaHack/SeeSharper.DependencyInjection/tree/master/sample) too.

All necessary packages are public available on [NuGet](https://www.nuget.org/) and [GitHub](https://github.com/DavenaHack?tab=packages&repo_name=SeeSharper.DependencyInjection).


### Creating a *IDependencyProvider* and provide a *IDependency*

```ps
Install-Package Mimp.SeeSharper.DependencyInjection
```
```cs
public class Program
{

  public static void Main(string[] args)
  {
    // Create a dependency builder
    IDependencySourceBuilder builder = new DependencySourceBuilder();

    // Add your dependency to the builder
    // ...
    // ...

    // Create the provider
    IDependencyProvider provider = new DependencyProvider(
      builder.BuildSource(),
      new FallbackEnumerableDependencyMatcher(
        new DependencyMatcher()
          .Intersect(new TagDependencyMatcher())),
      new LastDependencySelector(),
      new DependencyInvoker()
    );

    // Provide your dependency and use it
    // With "Use" the dependency will automatically dispose
    provider.Use<IMyDependency>(myDependency => {
      // ...
    });

    // Don't forget to dispose the provider
    proivder.Dispose();

  }

}
```

### Singleton

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Singleton
```
```cs
public static void AddSingletons(IDependencySourceBuilder builder)
{
  builder.AddSingleton<MyDependency>(provider => {
    // Create your dependency
    return new MyDependency();  
  }, (provider, myDependency) => {
    // Initialize your dependency
    // this step can skip and do all in factory method
    // it could be helpful when you need a bidirectional references has
    myDependency.IsInit = true;
  }).As<IMyDependency>();
}
```

### Scope

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Scope
```
```cs
public static void AddScopes(IDependencySourceBuilder builder)
{
  // Add all dependencies that are nessesary to resolve scopes
  builder.UseScope();

  // Add dependency
  builder.AddScoped<MyDependency>(provider => {
    // Create your dependency
    return new MyDependency();  
  }, (provider, myDependency) => {
    // Initialize your dependency
    // this step can skip and do all in factory method
    // it could be helpful when you need a bidirectional references has
    myDependency.IsInit = true;
  }).AddScope(provider => provider.CreateScope("scope1"))
    .AddScope(provider => provider.CreateScope("scope2"))
    .As<IMyDependency>();
  
  // Add a source that applies only to the scope
  builder.AddSource(provider =>
  {
    var sourceBuilder = new DependencySourceBuilder();
    // build your source ...
    return sourceBuilder.BuildSource(provider)
      .Scoped(provider.CreateScope("scope1"));
  });
}
```

### Transient

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Transient
```
```cs
public static void AddTransients(IDependencySourceBuilder builder)
{
  builder.AddTransient<MyDependency>(provider => {
    // Create your dependency
    return new MyDependency();  
  }).As<IMyDependency>();
}
```

### Tag - Name/Key

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Tag
```
```cs
public static void AddInstantiations(IDependencySourceBuilder builder)
{
  // Add a ITagVerifier to compare tags.
  builder.UseTagVerifier();
  
  builder.AddSingleton(/* ... */)
    .Tag("dep1");
  builder.AddScoped(/* ... */)
    .Tag("dep2");
  builder.AddTransient(/* ... */)
    .Tag("dep3");
}
```

### Instantiation - Auto instantiate and resolving

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Instantiation
```
```cs
public static void AddInstantiations(IDependencySourceBuilder builder)
{
  // Add all dependencies that are nessesary
  // to instantiate class with injecting it dependencies.
  // You can pass parameter to instantiate, too.
  builder.UseInstantiator();
  
  builder.AddSingleton<IMyDependency, MyDependency>();
  builder.AddScoped<IMyDependency, MyDependency>();
  builder.AddTransient<IMyDependency, MyDependency>();
}
```


### Asp.Net (Core v3 - .Net 5)

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection
Install-Package Mimp.SeeSharper.DependencyInjection.Extensions.Configuration
```

#### Add to Asp.Net

```cs
public class Program
{

	public static void Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			// Add SerivceProviderFactory to Asp.Net
			.UseServiceProviderFactory(new DependencyServiceProviderFactory())
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			});

}
```

#### Configure your services

```cs
public class Startup
{

	// This method gets called by the runtime. Use this method to add services to the container.
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();
		services.AddHttpContextAccessor();
	}

	// This method gets called by the runtime. Use this method to add dependencies to the builder.
	public void ConfigureContainer(IDependencySourceBuilder builder)
	{
		// Add your own IScopeProvider to manage your scopes and tenants.
		builder.UseScopeProvider(provider => 
		  new HttpHeaderScopeProvider(provider.GetDependencyRequired<IHttpContextAccessor>()));

		// Configure your services in a json or xml file with help of a IConfiguration
		builder.AddConfigureSource(new ConfigurationBuilder().AddJsonFile("dependencies.json").Build());
	}

}
```

#### Implement your own scope providing

```cs
public class HttpHeaderScopeProvider : IScopeProvider
{

	public IHttpContextAccessor HttpContextAccessor { get; }

	public HttpHeaderScopeProvider(IHttpContextAccessor httpContextAccessor)
	{
		HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
	}

	public object GetScope(IDependencyScope scope)
	{
		var context = HttpContextAccessor.HttpContext;
		if (context is null)
			return new object(); // return anonymous

		return context.Request.Headers["Scope"].FirstOrDefault() ?? new object();
	}

}
```



## License
Like *SeeSharper*, *SeeSharper DependencyInjection* is under the [MIT license](https://github.com/DavenaHack/SeeSharper.DependencyInjection/blob/master/LICENSE), too.
