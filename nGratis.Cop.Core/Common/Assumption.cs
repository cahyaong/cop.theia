// --------------------------------------------------------------------------------
// <copyright file="Assumption.cs" company="nGratis">
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
// --------------------------------------------------------------------------------

namespace nGratis.Cop.Core
{
    using System;
    using System.Linq.Expressions;

    public static class Assumption
    {
        public static void ThrowWhenInvalidArgument<T>(bool isInvalidCondition, Expression<Func<T>> argumentExpression, string reason = null)
        {
            if (isInvalidCondition)
            {
                throw new ArgumentException(reason, argumentExpression.FindPropertyName());
            }
        }

        public static void ThrowWhenNullArgument<T>(Expression<Func<T>> argumentExpression, string reason = null)
             where T : class
        {
            if (argumentExpression.Compile()() == null)
            {
                throw new ArgumentNullException(argumentExpression.FindPropertyName(), reason);
            }
        }

        public static void ThrowWhenNullOrWhitespaceArgument(Expression<Func<string>> argumentExpression, string reason = null)
        {
            var argument = argumentExpression.Compile()();

            if (string.IsNullOrEmpty(argument) || string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException(argumentExpression.FindPropertyName(), reason);
            }
        }

        public static void ThrowWhenUnexpectedNullValue<T>(Expression<Func<T>> argumentExpression, string reason = null)
        {
            if (object.Equals(argumentExpression.Compile()(), null))
            {
                throw new InvalidOperationException(reason);
            }
        }

        public static void ThrowWhenInvalidOperation(bool isInvalidCondition, string reason = null, Exception innerException = null)
        {
            if (isInvalidCondition)
            {
                throw new InvalidOperationException(reason, innerException);
            }
        }
    }
}