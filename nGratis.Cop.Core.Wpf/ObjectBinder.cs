// --------------------------------------------------------------------------------
// <copyright file="ObjectBinder.cs" company="nGratis">
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
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows;

    using nGratis.Cop.Core;

    public class ObjectBinder
    {
        // FIXME: Need to make this class disposable!

        private readonly INotifyPropertyChanged _source;

        private readonly INotifyPropertyChanged _target;

        private readonly PropertyInfo _sourceProperty;

        private readonly PropertyInfo _targetProperty;

        private MethodInfo _sourceCallbackMethod;

        private MethodInfo _targetCallbackMethod;

        public ObjectBinder(INotifyPropertyChanged source, PropertyInfo sourceProperty, INotifyPropertyChanged target, PropertyInfo targetProperty)
        {
            Assumption.ThrowWhenNullArgument(() => source);
            Assumption.ThrowWhenNullArgument(() => sourceProperty);
            Assumption.ThrowWhenNullArgument(() => target);
            Assumption.ThrowWhenNullArgument(() => targetProperty);

            this._source = source;
            this._target = target;

            this._sourceProperty = sourceProperty;
            this._targetProperty = targetProperty;

            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(source, "PropertyChanged", this.OnSourcePropertyChanged);
            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(target, "PropertyChanged", this.OnTargetPropertyChanged);
        }

        public void BindSourceCallback(string id)
        {
            Assumption.ThrowWhenNullOrWhitespaceArgument(() => id);

            var callbackAttribute = null as AsFieldCallbackAttribute;

            this._sourceCallbackMethod = this._source
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .SingleOrDefault(method => (callbackAttribute = method.GetCustomAttribute<AsFieldCallbackAttribute>()) != null && callbackAttribute.Id == id);
        }

        public void BindTargetCallback(string id)
        {
            Assumption.ThrowWhenNullOrWhitespaceArgument(() => id);

            var callbackAttribute = null as AsFieldCallbackAttribute;

            this._targetCallbackMethod = this._source
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .SingleOrDefault(method => (callbackAttribute = method.GetCustomAttribute<AsFieldCallbackAttribute>()) != null && callbackAttribute.Id == id);
        }

        private void OnTargetPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != this._targetProperty.Name)
            {
                return;
            }

            var value = this._targetProperty.GetValue(this._target);
            this._sourceProperty.SetValue(this._source, value);

            if (this._sourceCallbackMethod != null)
            {
                this._sourceCallbackMethod.Invoke(this._source, new object[] { });
            }
        }

        private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != this._sourceProperty.Name)
            {
                return;
            }

            var value = this._sourceProperty.GetValue(this._source);
            this._targetProperty.SetValue(this._target, value);

            if (this._targetCallbackMethod != null)
            {
                this._targetCallbackMethod.Invoke(this._target, new object[] { });
            }
        }
    }
}