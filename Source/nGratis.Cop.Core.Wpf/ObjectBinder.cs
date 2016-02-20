// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectBinder.cs" company="nGratis">
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
// <creation_timestamp>Wednesday, 24 December 2014 12:14:47 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core.Wpf
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using nGratis.Cop.Core.Contract;

    public sealed class ObjectBinder
    {
        private readonly INotifyPropertyChanged source;

        private readonly INotifyPropertyChanged target;

        private readonly PropertyInfo sourceProperty;

        private readonly PropertyInfo targetProperty;

        private MethodInfo sourceCallbackMethod;

        private MethodInfo targetCallbackMethod;

        private Action onSourceValueUpdated;

        private Action onSourceValueUpdating;

        private Action onTargetValueUpdating;

        private Action onTargetValueUpdated;

        private Action onSourceErrorEncountered;

        private Action onTargetErrorEncountered;

        public ObjectBinder(
            INotifyPropertyChanged source,
            PropertyInfo sourceProperty,
            INotifyPropertyChanged target,
            PropertyInfo targetProperty)
        {
            Guard.AgainstNullArgument(() => source);
            Guard.AgainstNullArgument(() => sourceProperty);
            Guard.AgainstNullArgument(() => target);
            Guard.AgainstNullArgument(() => targetProperty);

            this.source = source;
            this.target = target;

            this.sourceProperty = sourceProperty;
            this.targetProperty = targetProperty;

            source.PropertyChanged += async (_, args) => await this.OnSourcePropertyChangedAsync(args.PropertyName);
            target.PropertyChanged += async (_, args) => await this.OnTargetPropertyChangedAsync(args.PropertyName);
        }

        public void BindSourceCallback(
            Action onValueUpdating = null,
            Action onValueUpdated = null,
            Action onErrorEncountered = null)
        {
            var methodName = "On{0}Changed".WithInvariantFormat(this.sourceProperty.Name);

            this.sourceCallbackMethod = this
                .source
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .SingleOrDefault(method =>
                    method.GetCustomAttribute<AsFieldCallbackAttribute>() != null &&
                    method.Name == methodName && !method.GetParameters().Any());

            this.onSourceValueUpdating = onValueUpdating;
            this.onSourceValueUpdated = onValueUpdated;
            this.onSourceErrorEncountered = onErrorEncountered;
        }

        public void BindTargetCallback(
            Action onValueUpdating = null,
            Action onValueUpdated = null,
            Action onErrorEncountered = null)
        {
            var methodName = "On{0}Changed".WithInvariantFormat(this.sourceProperty.Name);

            this.targetCallbackMethod = this
                .target
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .SingleOrDefault(method =>
                    method.GetCustomAttribute<AsFieldCallbackAttribute>() != null &&
                    method.Name == methodName && !method.GetParameters().Any());

            this.onTargetValueUpdating = onValueUpdating;
            this.onTargetValueUpdated = onValueUpdated;
            this.onTargetErrorEncountered = onErrorEncountered;
        }

        private async Task OnSourcePropertyChangedAsync(string propertyName)
        {
            if (this.sourceProperty.Name != propertyName)
            {
                return;
            }

            try
            {
                var value = this.sourceProperty.GetValue(this.source);
                this.targetProperty.SetValue(this.target, value);

                if (this.onSourceValueUpdating != null)
                {
                    this.onSourceValueUpdating();
                }

                if (this.targetCallbackMethod != null)
                {
                    if (typeof(Task<CallbackResult>).IsAssignableFrom(this.targetCallbackMethod.ReturnType))
                    {
                        var result = await (Task<CallbackResult>)this.targetCallbackMethod.Invoke(this.target, null);

                        if (result.HasError)
                        {
                            if (this.onSourceErrorEncountered != null)
                            {
                                this.onSourceErrorEncountered();
                            }

                            return;
                        }
                    }
                    else if (typeof(Task).IsAssignableFrom(this.targetCallbackMethod.ReturnType))
                    {
                        await (Task)this.targetCallbackMethod.Invoke(this.target, null);
                    }
                    else
                    {
                        this.targetCallbackMethod.Invoke(this.target, null);
                    }
                }

                if (this.onSourceValueUpdated != null)
                {
                    this.onSourceValueUpdated();
                }
            }
            catch (ValueUpdateException)
            {
                if (this.onSourceErrorEncountered != null)
                {
                    this.onSourceErrorEncountered();
                }
            }
        }

        private async Task OnTargetPropertyChangedAsync(string propertyName)
        {
            if (this.targetProperty.Name != propertyName)
            {
                return;
            }

            try
            {
                var value = this.targetProperty.GetValue(this.target);
                this.sourceProperty.SetValue(this.source, value);

                if (this.onTargetValueUpdating != null)
                {
                    this.onTargetValueUpdating();
                }

                if (this.sourceCallbackMethod != null)
                {
                    if (typeof(Task<CallbackResult>).IsAssignableFrom(this.sourceCallbackMethod.ReturnType))
                    {
                        var result = await (Task<CallbackResult>)this.sourceCallbackMethod.Invoke(this.source, null);

                        if (result.HasError)
                        {
                            if (this.onTargetErrorEncountered != null)
                            {
                                this.onTargetErrorEncountered();
                            }

                            return;
                        }
                    }
                    else if (typeof(Task).IsAssignableFrom(this.sourceCallbackMethod.ReturnType))
                    {
                        await (Task)this.sourceCallbackMethod.Invoke(this.source, null);
                    }
                    else
                    {
                        this.sourceCallbackMethod.Invoke(this.source, null);
                    }
                }

                if (this.onTargetValueUpdated != null)
                {
                    this.onTargetValueUpdated();
                }
            }
            catch (ValueUpdateException)
            {
                if (this.onTargetErrorEncountered != null)
                {
                    this.onTargetErrorEncountered();
                }
            }
        }
    }
}