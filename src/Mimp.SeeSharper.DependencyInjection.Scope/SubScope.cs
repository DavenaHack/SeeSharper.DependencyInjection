using Mimp.SeeSharper.DependencyInjection.Abstraction;
using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;
using System.Collections.Generic;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class SubScope : IScope
    {


        public IScope Parent { get; }

        public IScope Scope { get; }


        public SubScope(IScope parent, IScope scope)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }


        public bool In(IScope scope)
        {
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));

            if (scope is not SubScope subScope)
                return Parent.In(scope);

            var thatStack = GetReverseStack();
            var scopeStack = subScope.GetReverseStack();
            if (scopeStack.Count < thatStack.Count)
                return false;

            while (thatStack.Count > 0)
                if (!thatStack.Pop().In(scopeStack.Pop()))
                    return false;

            return true;
        }


        protected Stack<IScope> GetReverseStack()
        {
            var stack = new Stack<IScope>();

            IScope current = this;
            while (current is SubScope sub)
            {
                stack.Push(sub.Scope);
                current = sub.Parent;
            }
            stack.Push(current);

            return stack;
        }


        public virtual IScope? InvolvedScope(IDependencyContext context, IEnumerable<IScope> scopes)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            if (!In(context.GetScope()))
                return null;

            foreach (var scope in scopes)
                if (In(scope))
                    return scope;

            return this;
        }


        public override string ToString()
        {
            return Parent + "." + Scope;
        }


    }
}
