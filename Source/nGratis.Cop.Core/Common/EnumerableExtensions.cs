// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace System.Collections.Generic
// ReSharper restore CheckNamespace
{
    using System;
    using System.Linq;

    using nGratis.Cop.Core;

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> lefts, IEnumerable<T> rights)
        {
            Assumption.ThrowWhenNullArgument(() => lefts);
            Assumption.ThrowWhenNullArgument(() => rights);

            foreach (var left in lefts)
            {
                yield return left;
            }

            foreach (var right in rights)
            {
                yield return right;
            }
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> lefts, IEnumerable<T> rights)
        {
            Assumption.ThrowWhenNullArgument(() => lefts);
            Assumption.ThrowWhenNullArgument(() => rights);

            foreach (var right in rights)
            {
                yield return right;
            }

            foreach (var left in lefts)
            {
                yield return left;
            }
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> lefts, T right)
        {
            Assumption.ThrowWhenNullArgument(() => lefts);

            foreach (var left in lefts)
            {
                yield return left;
            }

            yield return right;
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> lefts, T right)
        {
            Assumption.ThrowWhenNullArgument(() => lefts);

            yield return right;

            foreach (var left in lefts)
            {
                yield return left;
            }
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> lefts, IEnumerable<T> rights, Func<T, T, bool> isEqual)
        {
            Assumption.ThrowWhenNullArgument(() => lefts);
            Assumption.ThrowWhenNullArgument(() => rights);
            Assumption.ThrowWhenNullArgument(() => isEqual);

            return lefts.Except(rights, new DelegateEqualityComparer<T>(isEqual));
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> perform)
        {
            Assumption.ThrowWhenNullArgument(() => items);
            Assumption.ThrowWhenNullArgument(() => perform);

            foreach (var item in items)
            {
                perform(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T, int> apply)
        {
            Assumption.ThrowWhenNullArgument(() => items);
            Assumption.ThrowWhenNullArgument(() => apply);

            var index = 0;

            foreach (var item in items)
            {
                apply(item, index++);
            }
        }

        public static IList<T> PutInList<T>(this T item)
        {
            return new List<T> { item };
        }
    }
}