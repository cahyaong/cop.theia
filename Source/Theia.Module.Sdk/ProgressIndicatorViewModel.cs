﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressIndicatorViewModel.cs" company="nGratis">
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
// <creation_timestamp>Sunday, 21 June 2015 6:39:55 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Theia.Module.Sdk
{
    using System.ComponentModel.Composition;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ReactiveUI;

    [Export]
    public class ProgressIndicatorViewModel : ReactiveObject
    {
        private bool isBusy;

        public ProgressIndicatorViewModel()
        {
            this.StartWorkCommand = ReactiveCommand.CreateFromTask(
                () => Task.Run(() => this.IsBusy = true),
                this.WhenAnyValue(vm => vm.IsBusy, isBusy => !isBusy)
                    .ObserveOn(RxApp.MainThreadScheduler));

            this.StopWorkCommand = ReactiveCommand.CreateFromTask(
                () => Task.Run(() => this.IsBusy = false),
                this.WhenAnyValue(vm => vm.IsBusy)
                    .ObserveOn(RxApp.MainThreadScheduler));
        }

        public bool IsBusy
        {
            get => this.isBusy;
            private set => this.RaiseAndSetIfChanged(ref this.isBusy, value);
        }

        public ICommand StartWorkCommand { get; }

        public ICommand StopWorkCommand { get; }
    }
}