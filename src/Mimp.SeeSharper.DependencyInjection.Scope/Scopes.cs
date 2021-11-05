using Mimp.SeeSharper.DependencyInjection.Scope.Abstraction;
using System;

namespace Mimp.SeeSharper.DependencyInjection.Scope
{
    public static class Scopes
    {


        public static IScope No { get; } = new NoScope();

        public static IScope Any { get; } = new AnyScope();


        public static IScope CreateScopeLeftRightWithPriority(IScopeFactory factory, string scope) =>
            CreateScopeLeftRightWithPriority(factory, scope, 0);

        private static IScope CreateScopeLeftRightWithPriority(IScopeFactory factory, string value, int startOffset)
        {
            if (GetOperatorGroups(value, startOffset, '.', out var groups))
                return CreateSubScope(factory, groups!.Item1.Item1, groups.Item1.Item2, groups.Item2.Item1, groups.Item2.Item2);
            if (GetOperatorGroups(value, startOffset, '|', out groups))
                return CreateOrScope(factory, groups!.Item1.Item1, groups.Item1.Item2, groups.Item2.Item1, groups.Item2.Item2);
            if (GetOperatorGroups(value, startOffset, '^', out groups))
                return CreateXorScope(factory, groups!.Item1.Item1, groups.Item1.Item2, groups.Item2.Item1, groups.Item2.Item2);
            if (GetOperatorGroups(value, startOffset, '&', out groups))
                return CreateAndScope(factory, groups!.Item1.Item1, groups.Item1.Item2, groups.Item2.Item1, groups.Item2.Item2);

            return CreateScope(factory, value, startOffset);

            static bool GetOperatorGroups(string value, int startOffset, char separator, out Tuple<Tuple<string, int>, Tuple<string, int>>? groups)
            {
                var i = 0;
                var b = 0;
                var inBrackets = false;
                while (i < value.Length)
                {
                    var c = value[i];
                    if (c == '(')
                    {
                        if (i == 0)
                            inBrackets = true;
                        b++;
                    }
                    else if (c == ')')
                    {
                        b--;
                        if (b == 0 && i != value.Length)
                            inBrackets = false;
                        else if (b < 0)
                            throw new ArgumentException($"No open bracket for ')' at {startOffset + i}.");
                    }
                    else if (c == separator && b == 0)
                    {
                        groups = Tuple.Create(Tuple.Create(value.Substring(0, i), startOffset), Tuple.Create(value.Substring(i + 1), startOffset + i + 1));
                        return true;
                    }
                    i++;
                }
                if (b > 0)
                    throw new ArgumentException($"Not all '(' are closed.");

                if (inBrackets)
                    GetOperatorGroups(value.Substring(1, value.Length - 2), startOffset + 1, separator, out groups);

                groups = null;
                return false;
            }
        }

        private static IScope CreateSubScope(IScopeFactory factory, string scope, int scopeOffset, string value, int startOffset) =>
            CreateScopeLeftRightWithPriority(factory, scope, scopeOffset).Sub(CreateScopeLeftRightWithPriority(factory, value, startOffset));

        private static IScope CreateOrScope(IScopeFactory factory, string scope, int scopeOffset, string value, int startOffset) =>
            CreateScopeLeftRightWithPriority(factory, scope, scopeOffset).Or(CreateScopeLeftRightWithPriority(factory, value, startOffset));

        private static IScope CreateXorScope(IScopeFactory factory, string scope, int scopeOffset, string value, int startOffset) =>
            CreateScopeLeftRightWithPriority(factory, scope, scopeOffset).Xor(CreateScopeLeftRightWithPriority(factory, value, startOffset));

        private static IScope CreateAndScope(IScopeFactory factory, string scope, int scopeOffset, string value, int startOffset) =>
            CreateScopeLeftRightWithPriority(factory, scope, scopeOffset).And(CreateScopeLeftRightWithPriority(factory, value, startOffset));

        private static IScope CreateScope(IScopeFactory factory, string value, int startOffset) =>
            string.IsNullOrEmpty(value) ? throw new ArgumentException($"Scope can't be empty: {startOffset}")
            : value == "*" ? Any : factory.CreateScope(value);


    }
}
