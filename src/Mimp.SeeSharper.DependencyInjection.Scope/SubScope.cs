using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public class SubScope : IEquatable<SubScope?>
    {


        public object Parent { get; }

        public object Scope { get; }


        public SubScope(object parent, object scope)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }


        public override bool Equals(object? obj)
        {
            return Equals(obj as SubScope);
        }

        public bool Equals(SubScope? other)
        {
            return other is not null &&
                   EqualityComparer<object>.Default.Equals(Parent, other.Parent) &&
                   EqualityComparer<object>.Default.Equals(Scope, other.Scope);
        }


        public override int GetHashCode()
        {
#if NETFRAMEWORK
            int hashCode = -125621968;
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Parent);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Scope);
            return hashCode;
#else
            return HashCode.Combine(Parent, Scope);
#endif
        }


        public static bool operator ==(SubScope? left, SubScope? right)
        {
            return EqualityComparer<SubScope?>.Default.Equals(left, right);
        }

        public static bool operator !=(SubScope? left, SubScope? right)
        {
            return !(left == right);
        }


        public static SubScope Create(IEnumerable<object> scopes)
        {
            if (scopes is null)
                throw new ArgumentNullException(nameof(scopes));

            void ThrowAtLeast() =>
                throw new ArgumentException($"{nameof(scopes)} have to have at least 2 scopes.", nameof(scopes));
            object ThrowNull() =>
                throw new ArgumentNullException(nameof(scopes), "At least one scope is null.");

            var enumerator = scopes.GetEnumerator();
            if (!enumerator.MoveNext())
                ThrowAtLeast();
            var parent = enumerator.Current ?? ThrowNull();

            if (!enumerator.MoveNext())
                ThrowAtLeast();

            var subScope = new SubScope(parent, enumerator.Current ?? ThrowNull());
            while (enumerator.MoveNext())
                subScope = new SubScope(subScope, enumerator.Current ?? ThrowNull());

            return subScope;
        }

        public static SubScope Create(object parent, object scope, params object[] subScopes)
        {
            if (parent is null)
                throw new ArgumentNullException(nameof(parent));
            if (scope is null)
                throw new ArgumentNullException(nameof(scope));
            if (subScopes is null)
                throw new ArgumentNullException(nameof(subScopes));

            return Create(new[] { parent, scope }.Concat(
                subScopes.Select(s => s ?? throw new ArgumentNullException(nameof(subScopes), "At least on subscope is null."))));
        }


    }
}
