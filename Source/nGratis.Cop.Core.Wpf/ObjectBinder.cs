// ------------------------------------------------------------------------------------------------------------------------------------------------------------
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
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

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

        private readonly INotifyPropertyChanged source;
        private readonly INotifyPropertyChanged target;
        private readonly PropertyInfo sourceProperty;
        private readonly PropertyInfo targetProperty;
        private MethodInfo sourceCallbackMethod;
        private MethodInfo targetCallbackMethod;

        public ObjectBinder(INotifyPropertyChanged source, PropertyInfo sourceProperty, INotifyPropertyChanged target, PropertyInfo targetProperty)
        {
            Assumption.ThrowWhenNullArgument(() => source);
            Assumption.ThrowWhenNullArgument(() => sourceProperty);
            Assumption.ThrowWhenNullArgument(() => target);
            Assumption.ThrowWhenNullArgument(() => targetProperty);

            this.source = source;
            this.target = target;

            this.sourceProperty = sourceProperty;
            this.targetProperty = targetProperty;

            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(source, "PropertyChanged", this.OnSourcePropertyChanged);
            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(target, "PropertyChanged", this.OnTargetPropertyChanged);
        }

        public void BindSourceCallback()
        {
            var callbackMethodName = "On{0}Changed".WithFormat(this.sourceProperty.Name);

            this.sourceCallbackMethod = this.source
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .SingleOrDefault(method => method.Name == callbackMethodName && method.GetCustomAttribute<AsFieldCallbackAttribute>() != null);
        }

        public void BindTargetCallback()
        {
            var callbackMethodName = "On{0}Changed".WithFormat(this.targetProperty.Name);

            this.targetCallbackMethod = this.source
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .SingleOrDefault(method => method.Name == callbackMethodName && method.GetCustomAttribute<AsFieldCallbackAttribute>() != null);
        }

        private void OnTargetPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != this.targetProperty.Name)
            {
                return;
            }

            var value = this.targetProperty.GetValue(this.target);
            this.sourceProperty.SetValue(this.source, value);

            if (this.sourceCallbackMethod != null)
            {
                this.sourceCallbackMethod.Invoke(this.source, new object[] { });
            }
        }

        private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != this.sourceProperty.Name)
            {
                return;
            }

            var value = this.sourceProperty.GetValue(this.source);
            this.targetProperty.SetValue(this.target, value);

            if (this.targetCallbackMethod != null)
            {
                this.targetCallbackMethod.Invoke(this.target, new object[] { });
            }
        }
    }
}