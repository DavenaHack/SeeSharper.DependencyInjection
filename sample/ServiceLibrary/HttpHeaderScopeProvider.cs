using Microsoft.AspNetCore.Http;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Linq;

namespace ServiceLibrary
{
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
                return new object();

            return context.Request.Headers["Scope"].FirstOrDefault() ?? new object();
        }


    }
}
