// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldGroupViewModel.cs" company="nGratis">
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

namespace nGratis.Cop.Core.Wpf
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using JetBrains.Annotations;
    using nGratis.Cop.Core.Contract;
    using ReactiveUI;

    public class FieldGroupViewModel : ReactiveObject
    {
        private readonly List<ObjectBinder> fieldBinders;

        private FieldMode mode;

        private ICollection<FieldViewModel> fields;

        public FieldGroupViewModel(object instance, FieldMode mode)
        {
            Guard.AgainstNullArgument(() => instance);
            Guard.AgainstInvalidArgument(mode == FieldMode.Unknown, () => mode);

            var notifyingInstance = instance as INotifyPropertyChanged;

            Guard.AgainstInvalidArgument(notifyingInstance == null, () => instance);

            this.fieldBinders = new List<ObjectBinder>();

            this.Mode = mode;
            this.Fields = new ObservableCollection<FieldViewModel>();

            instance
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Select(property => new { Property = property, FieldAttribute = property.GetCustomAttribute<AsFieldAttribute>() })
                .Where(annon => annon.FieldAttribute != null && annon.FieldAttribute.Mode == mode)
                .ToList()
                .ForEach(annon =>
                    {
                        var field = new FieldViewModel(annon.Property.PropertyType, annon.FieldAttribute);
                        this.Fields.Add(field);

                        var binder = new ObjectBinder(notifyingInstance, annon.Property, field, FieldViewModel.ValueProperty);
                        binder.BindSourceCallback();
                        this.fieldBinders.Add(binder);
                    });
        }

        [UsedImplicitly]
        public FieldMode Mode
        {
            get { return this.mode; }
            private set { this.RaiseAndSetIfChanged(ref this.mode, value); }
        }

        [UsedImplicitly]
        public ICollection<FieldViewModel> Fields
        {
            get { return this.fields; }
            private set { this.RaiseAndSetIfChanged(ref this.fields, value); }
        }
    }
}