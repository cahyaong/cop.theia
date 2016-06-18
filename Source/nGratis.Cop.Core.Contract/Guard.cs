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
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using JetBrains.Annotations;

    public static class Guard
    {
        [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class Require
        {
            [DebuggerStepThrough]
            [ContractAnnotation("argument:null => halt")]
            public static void IsNotNull(object argument)
            {
                if (argument != null)
                {
                    return;
                }

                Fire.PreconditionException($"Argument cannot be { Constants.Values.Null }.");
            }

            [DebuggerStepThrough]
            [ContractAnnotation("argument:null => halt")]
            public static void IsNotEqualTo(object argument, object value)
            {
                if (!object.Equals(argument, value))
                {
                    return;
                }

                Fire.PreconditionException($"Argument cannot be equal to [{ value }].");
            }

            [DebuggerStepThrough]
            [ContractAnnotation("argument:null => halt")]
            [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
            public static void IsEqualTo<T>(Type argument)
            {
                if (argument == null)
                {
                    Fire.PreconditionException($"Argument cannot be { Constants.Values.Null }.");
                }

                if (!typeof(T).IsAssignableFrom(argument))
                {
                    Fire.PreconditionException($"Argument must be assignable to [{ typeof(T).FullName }].");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("argument:null => halt")]
            public static void IsNotDefault<T>(T argument)
            {
                if (!object.Equals(argument, default(T)))
                {
                    return;
                }

                Fire.PreconditionException($"Argument cannot be { Constants.Values.Default }.");
            }

            [DebuggerStepThrough]
            [ContractAnnotation("argument:null => halt")]
            public static void IsNotEmpty(string argument)
            {
                if (argument == null)
                {
                    Fire.PreconditionException($"Argument cannot be { Constants.Values.Null }.");
                }

                if (string.IsNullOrWhiteSpace(argument))
                {
                    Fire.PreconditionException($"Argument cannot be { Constants.Values.Empty }.");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("argument:null => halt")]
            public static void IsNotEmpty<T>(IEnumerable<T> argument)
            {
                if (argument == null)
                {
                    Fire.PreconditionException($"Argument cannot be { Constants.Values.Null }.");
                }

                if (!argument.Any())
                {
                    Fire.PreconditionException($"Argument cannot be { Constants.Values.Empty }.");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("argument:null => halt")]
            [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
            public static void IsTypeOf<T>(object argument)
            {
                if (argument == null)
                {
                    Fire.PreconditionException($"Argument cannot be { Constants.Values.Null }. ");
                }

                if (!(argument is T))
                {
                    Fire.PreconditionException(
                        $"Argument of type [{ argument.GetType().FullName }] must be assignable to " +
                        $"[{ typeof(T).FullName }].");
                }
            }

            [DebuggerStepThrough]
            [ContractAnnotation("isSatisfied:false => halt")]
            [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
            public static void IsSatisfied(bool isSatisfied, string reason = null)
            {
                if (isSatisfied)
                {
                    return;
                }

                Fire.PreconditionException(
                    @"Precondition is not satisfied. " +
                    $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
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
            public static void IsNotEmpty<T>(IEnumerable<T> value, string reason = null)
            {
                if (value == null)
                {
                    Fire.PostconditionException(
                        $"Value cannot be { Constants.Values.Null }. " +
                        $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
                }

                if (!value.Any())
                {
                    Fire.PostconditionException(
                        $"Value cannot be { Constants.Values.Empty }. " +
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
            [ContractAnnotation(" => halt")]
            [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
            public static void IsEnumerationSupported(object value, string reason = null)
            {
                Fire.PostconditionException(
                    $"Enumeration [{ value }] is not supported. " +
                    $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
            }

            [DebuggerStepThrough]
            [ContractAnnotation("isSatisfied:false => halt")]
            [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
            public static void IsSatisfied(bool isSatisfied, string reason = null)
            {
                if (isSatisfied)
                {
                    return;
                }

                Fire.PostconditionException(
                    @"Postcondition is not satisfied. " +
                    $"Reason: { reason.Coalesce(Constants.Values.Unknown) }");
            }
        }
    }
}