// --------------------------------------------------------------------------------
// <copyright file="AsFieldAttribute.cs" company="nGratis">
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

namespace nGratis.Cop.Core.Wpf
{
    using System;

    using JetBrains.Annotations;

    using nGratis.Cop.Core;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false), MeansImplicitUse]
    public class AsFieldAttribute : Attribute
    {
        public AsFieldAttribute(string id, FieldMode mode, string label)
            : this(id, mode, FieldType.Text, label)
        {
        }

        public AsFieldAttribute(string id, FieldMode mode, FieldType type, string label)
        {
            Assumption.ThrowWhenNullOrWhitespaceArgument(() => id);
            Assumption.ThrowWhenInvalidArgument(mode == FieldMode.Unknown, () => label);
            Assumption.ThrowWhenInvalidArgument(type == FieldType.Unknown, () => type);
            Assumption.ThrowWhenNullOrWhitespaceArgument(() => label);

            this.Id = id;
            this.Mode = mode;
            this.Type = type;
            this.Label = label;
        }

        public string Id { get; private set; }

        public FieldMode Mode { get; private set; }

        public FieldType Type { get; private set; }

        public string Label { get; private set; }
    }
}