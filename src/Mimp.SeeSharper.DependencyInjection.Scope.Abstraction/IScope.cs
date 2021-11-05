using Mimp.SeeSharper.DependencyInjection.Abstraction;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope.Abstraction
{
    public interface IScope
    {


        /// <summary>
        /// Indicate that this <see cref="IScope"/> is in <paramref name="scope"/>.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public bool In(IScope scope);

        /// <summary>
        /// Return the <see cref="IScope"/> which should contain 
        /// the requested instance from <paramref name="context"/>.
        /// Null if the <paramref name="context"/> isn't <see cref="In(IScope)"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="scopes">All scopes which have already a instance.</param>
        /// <returns></returns>
        public IScope? InvolvedScope(IDependencyContext context, IEnumerable<IScope> scopes);


    }
}
