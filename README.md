# SeeShraper DependencyInjection [![build and test](https://img.shields.io/github/workflow/status/DavenaHack/SeeSharper.DependencyInjection/build%20and%20test?label=build%20and%20test&logo=github)](https://github.com/DavenaHack/SeeSharper.DependencyInjection/actions/workflows/BuildAndTest.yml) [![NuGet Mimp.SeeSharper.DependencyInjection.Abstraction](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Abstraction?label=Mimp.SeeSharper.DependencyInjection.Abstraction&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Abstraction/) [![NuGet Mimp.SeeSharper.DependencyInjection.Singleton](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Singleton?label=Mimp.SeeSharper.DependencyInjection.Singleton&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Singleton/) [![NuGet Mimp.SeeSharper.DependencyInjection.Transient](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Transient?label=Mimp.SeeSharper.DependencyInjection.Transient&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Transient/) [![NuGet Mimp.SeeSharper.DependencyInjection.Scope.Abstraction](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Scope.Abstraction?label=Mimp.SeeSharper.DependencyInjection.Scope.Abstraction&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Scope.Abstraction/) [![NuGet Mimp.SeeSharper.DependencyInjection.Scope](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Scope?label=Mimp.SeeSharper.DependencyInjection.Scope&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Scope/) [![NuGet Mimp.SeeSharper.DependencyInjection.Tag.Abstraction](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Tag.Abstraction?label=Mimp.SeeSharper.DependencyInjection.Tag.Abstraction&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Tag.Abstraction/) [![NuGet Mimp.SeeSharper.DependencyInjection.Tag](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Tag?label=Mimp.SeeSharper.DependencyInjection.Tag&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Tag/) [![NuGet Mimp.SeeSharper.DependencyInjection](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection?label=Mimp.SeeSharper.DependencyInjection&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection/) [![NuGet Mimp.SeeSharper.DependencyInjection.Enumerable](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Enumerable?label=Mimp.SeeSharper.DependencyInjection.Enumerable&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Enumerable/) [![NuGet Mimp.SeeSharper.DependencyInjection.Instantiation](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Instantiation?label=Mimp.SeeSharper.DependencyInjection.Instantiation&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Instantiation/) [![NuGet Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection?label=Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection/) [![NuGet Mimp.SeeSharper.DependencyInjection.Extensions.Configuration](https://img.shields.io/nuget/v/Mimp.SeeSharper.DependencyInjection.Extensions.Configuration?label=Mimp.SeeSharper.DependencyInjection.Extensions.Configuration&logo=nuget)](https://www.nuget.org/packages/Mimp.SeeSharper.DependencyInjection.Extensions.Configuration/)

*SeeSharper DependencyInjection* is a component from the framework *[SeeSharper](https://github.com/DavenaHack/SeeSharper)*. *SeeSharper DependencyInjection* is a IoC container which work with IoC pattern. It support to plug in to other frameworks like Asp.Net.


## Get started

The construct is kept very abstract and may seem confusing for the beginning. So that everyone can get an insight into the framework, here are the basic explained briefly and concisely.

All necessary packages are public available on [NuGet](https://www.nuget.org/) and [GitHub](https://github.com/DavenaHack?tab=packages&repo_name=SeeSharper.DependencyInjection).


### Creating a *IDependencyProvider* and provide a *IDependency*

```ps
Install-Package Mimp.SeeSharper.DependencyInjection
```

<script src="https://gist.github.com/DavenaHack/7aadfd12ff294967aacdff071c742a59.js?file=Program.cs"></script>

### Singleton

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Singleton
```

<script src="https://gist.github.com/DavenaHack/7aadfd12ff294967aacdff071c742a59.js?file=Program.Singleton.cs"></script>

### Scope

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Scope
```

<script src="https://gist.github.com/DavenaHack/7aadfd12ff294967aacdff071c742a59.js?file=Program.Scope.cs"></script>

### Transient

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Transient
```

<script src="https://gist.github.com/DavenaHack/7aadfd12ff294967aacdff071c742a59.js?file=Program.Transient.cs"></script>

### Tag - Name/Key

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Tag
```

<script src="https://gist.github.com/DavenaHack/7aadfd12ff294967aacdff071c742a59.js?file=Program.Tag.cs"></script>

### Instantiation - Auto instantiate and resolving

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Instantiation
```

<script src="https://gist.github.com/DavenaHack/7aadfd12ff294967aacdff071c742a59.js?file=Program.Instantiation.cs"></script>


### Asp.Net (Core v3 - .Net 5)

```ps
Install-Package Mimp.SeeSharper.DependencyInjection.Extensions.DependencyInjection
```

#### Add to Asp.Net

<script src="https://gist.github.com/DavenaHack/a33c91cc6392e7ff5f942c1d0f9ac867.js?file=Program.cs"></script>

#### Configure your services

<script src="https://gist.github.com/DavenaHack/a33c91cc6392e7ff5f942c1d0f9ac867.js?file=StartUp.cs"></script>

#### Implement your own scope providing

<script src="https://gist.github.com/DavenaHack/a33c91cc6392e7ff5f942c1d0f9ac867.js?file=StartUp.cs"></script>



## License
Like *SeeSharper*, *SeeSharper DependencyInjection* is under the [MIT license](https://github.com/DavenaHack/SeeSharper.DependencyInjection/blob/master/LICENSE), too.
