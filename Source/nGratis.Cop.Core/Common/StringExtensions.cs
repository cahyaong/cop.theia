// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="nGratis">
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
// <creation_timestamp>Sunday, 29 March 2015 6:39:08 AM</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using JetBrains.Annotations;
    using nGratis.Cop.Core;

    public static class StringExtensions
    {
        [StringFormatMethod("format")]
        public static string WithFormat(this string format, params object[] args)
        {
            return string.IsNullOrWhiteSpace(format)
                ? format
                : string.Format(CultureInfo.InvariantCulture, format, args);
        }

        public static string WithMessageDetails(this string input, params MessageDetail[] details)
        {
            return string.IsNullOrWhiteSpace(input) || details == null || !details.Any()
                ? input
                : "{0} [{1}]".WithFormat(input, string.Join(" | ", details.Select(detail => detail.ToString())));
        }

        public static string WithLowerCaseAtBeginning(this string input)
        {
            return string.IsNullOrWhiteSpace(input)
                ? input
                : "{0}{1}".WithFormat(char.ToLower(input.First()), input.Substring(1, input.Length - 1));
        }
    }
}