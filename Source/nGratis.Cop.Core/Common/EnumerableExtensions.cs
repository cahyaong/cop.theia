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
        public static IEnumerable<TItem> Append<TItem>(this IEnumerable<TItem> leftItems, IEnumerable<TItem> rightItems)
        {
            Assumption.ThrowWhenNullArgument(() => leftItems);
            Assumption.ThrowWhenNullArgument(() => rightItems);

            foreach (var leftItem in leftItems)
            {
                yield return leftItem;
            }

            foreach (var rightItem in rightItems)
            {
                yield return rightItem;
            }
        }

        public static IEnumerable<TItem> Prepend<TItem>(this IEnumerable<TItem> leftItems, IEnumerable<TItem> rightItems)
        {
            Assumption.ThrowWhenNullArgument(() => leftItems);
            Assumption.ThrowWhenNullArgument(() => rightItems);

            foreach (var rightItem in rightItems)
            {
                yield return rightItem;
            }

            foreach (var leftItem in leftItems)
            {
                yield return leftItem;
            }
        }

        public static IEnumerable<TItem> Append<TItem>(this IEnumerable<TItem> leftItems, TItem rightItem)
        {
            Assumption.ThrowWhenNullArgument(() => leftItems);

            foreach (var leftItem in leftItems)
            {
                yield return leftItem;
            }

            yield return rightItem;
        }

        public static IEnumerable<TItem> Prepend<TItem>(this IEnumerable<TItem> leftItems, TItem rightItem)
        {
            Assumption.ThrowWhenNullArgument(() => leftItems);

            yield return rightItem;

            foreach (var leftItem in leftItems)
            {
                yield return leftItem;
            }
        }

        public static IEnumerable<TItem> Except<TItem>(this IEnumerable<TItem> leftItems, IEnumerable<TItem> rightItems, Func<TItem, TItem, bool> isEqual)
        {
            Assumption.ThrowWhenNullArgument(() => leftItems);
            Assumption.ThrowWhenNullArgument(() => rightItems);
            Assumption.ThrowWhenNullArgument(() => isEqual);

            return leftItems.Except(rightItems, new DelegateEqualityComparer<TItem>(isEqual));
        }

        public static void ForEach<TItem>(this IEnumerable<TItem> items, Action<TItem> perform)
        {
            Assumption.ThrowWhenNullArgument(() => items);
            Assumption.ThrowWhenNullArgument(() => perform);

            foreach (var item in items)
            {
                perform(item);
            }
        }

        public static void ForEach<TItem>(this IEnumerable<TItem> items, Action<TItem, int> apply)
        {
            Assumption.ThrowWhenNullArgument(() => items);
            Assumption.ThrowWhenNullArgument(() => apply);

            var index = 0;

            foreach (var item in items)
            {
                apply(item, index++);
            }
        }
    }
}