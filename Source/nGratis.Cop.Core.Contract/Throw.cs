﻿// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Throw.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
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
// <creation_timestamp>Tuesday, 5 May 2015 2:20:18 PM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    public static class Throw
    {
        [ContractAnnotation(" => halt")]
        public static void NotSupportedException(string message)
        {
            throw new NotSupportedException(message ?? Values.Empty);
        }

        [ContractAnnotation(" => halt")]
        public static void CopException(string message, Exception innerException)
        {
            throw new CopException(message ?? Values.Empty, innerException);
        }

        [ContractAnnotation(" => halt")]
        public static void ArgumentNullException(string parameter, string message)
        {
            throw new ArgumentNullException(parameter ?? Values.Unknown, message ?? Values.Empty);
        }

        [ContractAnnotation(" => halt")]
        public static void ArgumentException(string parameter, string message)
        {
            throw new ArgumentException(parameter ?? Values.Unknown, message ?? Values.Empty);
        }

        [ContractAnnotation(" => halt")]
        public static void InvalidOperationException(string message)
        {
            throw new InvalidOperationException(message ?? Values.Empty);
        }

        private static class Values
        {
            public const string Unknown = "<unknown>";

            public const string Empty = "<empty>";
        }
    }
}