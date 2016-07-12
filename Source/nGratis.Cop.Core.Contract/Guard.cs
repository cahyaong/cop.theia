// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Guard.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2016 Cahya Ong
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
// <creation_timestamp>Tuesday, 14 June 2016 9:35:08 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using JetBrains.Annotations;

    public static class Guard
    {
        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class Require
        {
            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            public static void IsNotNull(object value)
            {
                if (value != null)
                {
                    return;
                }

                Fire.PreconditionException($"Value cannot be { Constants.Values.Null }.");
            }

            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            public static void IsNotEqualTo(object value, object anotherValue)
            {
                if (!object.Equals(value, anotherValue))
                {
                    return;
                }

                Fire.PreconditionException($"Value cannot be equal to [{ anotherValue }].");
            }

            [DebuggerStepThrough]
            [ContractAnnotation("type:null => halt")]
            [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
            public static void IsEqualTo<T>(Type type)
            {
                if (type == null)
                {
                    Fire.PreconditionException($"Type cannot be { Constants.Values.Null }.");
                }

                if (!typeof(T).IsAssignableFrom(type))
                {
                    Fire.PreconditionException($"Type must be assignable to [{ typeof(T).FullName }].");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            public static void IsNotDefault<T>(T value)
            {
                if (!object.Equals(value, default(T)))
                {
                    return;
                }

                Fire.PreconditionException($"Value cannot be { Constants.Values.Default }.");
            }

            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            public static void IsNotEmpty(string value)
            {
                if (value == null)
                {
                    Fire.PreconditionException($"String cannot be { Constants.Values.Null }.");
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    Fire.PreconditionException($"String cannot be { Constants.Values.Empty }.");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("enumerable:null => halt")]
            public static void IsNotEmpty<T>(IEnumerable<T> enumerable)
            {
                if (enumerable == null)
                {
                    Fire.PreconditionException($"Enumerable cannot be { Constants.Values.Null }.");
                }

                if (!enumerable.Any())
                {
                    Fire.PreconditionException($"Enumerable cannot be { Constants.Values.Empty }.");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("stream:null => halt")]
            public static void IsNotEmpty(Stream stream)
            {
                if (stream == null)
                {
                    Fire.PreconditionException($"Stream cannot be { Constants.Values.Null }.");
                }

                if (stream.Length == 0)
                {
                    Fire.PreconditionException($"Stream cannot be { Constants.Values.Empty }.");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("table:null => halt")]
            public static void IsNotEmpty(DataTable table)
            {
                if (table == null)
                {
                    Fire.PreconditionException($"Table cannot be { Constants.Values.Null }.");
                }

                if (table.Columns.Count == 0 || table.Rows.Count == 0)
                {
                    Fire.PreconditionException($"Table cannot be { Constants.Values.Empty }.");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
            public static void IsTypeOf<T>(object value)
            {
                if (value == null)
                {
                    Fire.PreconditionException($"Value cannot be { Constants.Values.Null }.");
                }

                if (!(value is T))
                {
                    Fire.PreconditionException($"Value must be assignable to [{ typeof(T).FullName }].");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("value:false => halt")]
            public static void IsTrue(bool value)
            {
                if (value)
                {
                    return;
                }

                Fire.PreconditionException("Value cannot be false.");
            }

            [DebuggerStepThrough]
            public static void IsFile(Uri uri)
            {
                if (uri == null)
                {
                    Fire.PreconditionException($"Value cannot be { Constants.Values.Null }.");
                }

                if (!uri.IsFile)
                {
                    Fire.PreconditionException($"URI must be a file. Path: [{ uri }].");
                }
            }

            [DebuggerStepThrough]
            public static void IsFileNotExist(string path)
            {
                if (!File.Exists(path))
                {
                    return;
                }

                Fire.PreconditionException($"File cannot exist. Path: [{ path }].");
            }

            // TODO: Add reason argument for all methods <Require> class!
            // TODO: Create a template for handling numerical type check!

            [DebuggerStepThrough]
            public static void IsLessThan(double value, double anotherValue)
            {
                if (value < anotherValue)
                {
                    return;
                }

                Fire.PreconditionException($"Value cannot be greater than or equal to [{ anotherValue }].");
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(int value)
            {
                if (value >= 0)
                {
                    return;
                }

                Fire.PreconditionException("Value cannot be negative.");
            }
        }

        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class Ensure
        {
            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
            public static void IsNotNull(object value, string reason = null)
            {
                if (value != null)
                {
                    return;
                }

                Fire.PostconditionException(
                    $"Value cannot be { Constants.Values.Null }. " +
                    $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
            }

            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
            public static void IsNotEmpty(string value, string reason = null)
            {
                if (value == null)
                {
                    Fire.PostconditionException(
                        $"Value cannot be { Constants.Values.Null }. " +
                        $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    Fire.PostconditionException(
                        $"Value cannot be { Constants.Values.Empty }. " +
                        $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("enumerable:null => halt")]
            [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
            public static void IsNotEmpty<T>(IEnumerable<T> enumerable, string reason = null)
            {
                if (enumerable == null)
                {
                    Fire.PostconditionException(
                        $"Enumerable cannot be { Constants.Values.Null }. " +
                        $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
                }

                if (!enumerable.Any())
                {
                    Fire.PostconditionException(
                        $"Enumerable cannot be { Constants.Values.Empty }. " +
                        $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
            [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
            public static void IsTypeOf<T>(object value, string reason = null)
            {
                if (value == null)
                {
                    Fire.PostconditionException(
                        $"Value cannot be { Constants.Values.Null }. " +
                        $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
                }

                if (!(value is T))
                {
                    Fire.PostconditionException(
                        $"Value must be assignable to [{ typeof(T).FullName }]. " +
                        $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("value:false => halt")]
            [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
            public static void IsTrue(bool value, string reason = null)
            {
                if (value)
                {
                    return;
                }

                Fire.PostconditionException(
                    @"Value cannot be false. " +
                    $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
            }

            [DebuggerStepThrough]
            [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
            public static void IsNotNegative(int value, string reason = null)
            {
                if (value >= 0)
                {
                    return;
                }

                Fire.PostconditionException(
                    @"Value cannot be negative. " +
                    $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
            }
        }
    }
}