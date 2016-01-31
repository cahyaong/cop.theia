// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldViewModel.cs" company="nGratis">
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
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core.Wpf
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using nGratis.Cop.Core.Contract;
    using ReactiveUI;

    public class FieldViewModel : ReactiveObject
    {
        public static readonly PropertyInfo ValueProperty = typeof(FieldViewModel).GetProperty(
            "Value",
            BindingFlags.Instance | BindingFlags.Public);

        private FieldMode mode;

        private FieldType type;

        private string label;

        private object value;

        private bool isValueUpdating;

        private bool hasError;

        internal FieldViewModel(Type valueType, AsFieldAttribute asFieldAttribute)
        {
            Guard.AgainstNullArgument(() => valueType);
            Guard.AgainstNullArgument(() => asFieldAttribute);

            this.ValueType = valueType;

            this.Mode = asFieldAttribute.Mode;
            this.Type = asFieldAttribute.Type;
            this.Label = asFieldAttribute.Label;
        }

        public FieldType Type
        {
            get { return this.type; }
            private set { this.RaiseAndSetIfChanged(ref this.type, value); }
        }

        public string Label
        {
            get { return this.label; }
            private set { this.RaiseAndSetIfChanged(ref this.label, value); }
        }

        public object Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value != value)
                {
                    var oldNotifier = this.value as INotifyPropertyChanged;
                    if (oldNotifier != null)
                    {
                        oldNotifier.RemoveEventHandler<INotifyPropertyChanged, PropertyChangedEventArgs>(
                            "PropertyChanged",
                            this.OnInnerPropertyChanged);
                    }

                    var newNotifier = value as INotifyPropertyChanged;
                    if (newNotifier != null)
                    {
                        newNotifier.AddEventHandler<INotifyPropertyChanged, PropertyChangedEventArgs>(
                            "PropertyChanged",
                            this.OnInnerPropertyChanged);
                    }
                }

                this.RaiseAndSetIfChanged(ref this.value, value);
            }
        }

        public FieldMode Mode
        {
            get { return this.mode; }
            private set { this.RaiseAndSetIfChanged(ref this.mode, value); }
        }

        public Type ValueType
        {
            get;
            private set;
        }

        public bool IsValueUpdating
        {
            get { return this.isValueUpdating; }
            set { this.RaiseAndSetIfChanged(ref this.isValueUpdating, value); }
        }

        public bool HasError
        {
            get { return this.hasError; }
            set { this.RaiseAndSetIfChanged(ref this.hasError, value); }
        }

        private void OnInnerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            this.RaisePropertyChanged("Value");
        }
    }
}