using Microsoft.AspNetCore.Http;
using Mimp.SeeSharper.DependencyInjection.Scope;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Linq;

namespace ServiceLibrary
{
    public class HttpHeaderScopeProvider : BaseScopeProvider
    {


        public IHttpContextAccessor HttpContextAccessor { get; }


        public HttpHeaderScopeProvider(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }


        protected override IScope CreateScope(IDependencyScope scope)
        {
            var context = HttpContextAccessor.HttpContext;
            if (context is null)
                return scope.Provider.CreateScope(new object());

            var requestScope = context.Request.Headers["Scope"].FirstOrDefault();
            if (requestScope is null)
                return scope.Provider.CreateScope(new object());

            return scope.Provider.UseScopeFactory(factory =>
                Scopes.CreateScopeLeftRightWithPriority(factory, requestScope));
        }


    }
}
