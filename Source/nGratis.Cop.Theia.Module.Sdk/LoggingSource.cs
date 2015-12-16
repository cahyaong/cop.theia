// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingSource.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 2 May 2015 1:01:11 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Theia.Module.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using nGratis.Cop.Core;
    using nGratis.Cop.Core.Contract;
    using ReactiveUI;

    public class LoggingSource : ReactiveObject
    {
        private bool isExecuting;

        private string name;

        public LoggingSource(IInfrastructureManager infrastructureManager, string name)
        {
            Guard.AgainstNullArgument(() => infrastructureManager);
            Guard.AgainstNullOrWhitespaceArgument(() => name);

            this.Name = name;
            this.Logger = infrastructureManager.LoggingProvider.GetLoggerFor("<SDK>");

            this.GenerateFatalCommand = ReactiveCommand.CreateAsyncTask(
                this.WhenAnyValue(vm => vm.IsExecuting, isExecuting => !isExecuting)
                    .ObserveOn(RxApp.MainThreadScheduler),
                _ => Task.Run(() => this.GenerateFatal()));

            this.GenerateTracesCommand = ReactiveCommand.CreateAsyncTask(
                this.WhenAnyValue(vm => vm.IsExecuting, isExecuting => !isExecuting)
                    .ObserveOn(RxApp.MainThreadScheduler),
                _ => Task.Run(() => this.GenerateTraces()));
        }

        public bool IsExecuting
        {
            get { return this.isExecuting; }
            private set { this.RaiseAndSetIfChanged(ref this.isExecuting, value); }
        }

        public string Name
        {
            get { return this.name; }
            private set { this.RaiseAndSetIfChanged(ref this.name, value); }
        }

        public ICommand GenerateFatalCommand { get; private set; }

        public ICommand GenerateTracesCommand { get; private set; }

        protected ILogger Logger { get; private set; }

        private void GenerateFatal()
        {
            this.IsExecuting = true;
            this.Logger.LogAsFatal(new CopException(), "Unhandled exception.");
            this.IsExecuting = false;
        }

        private void GenerateTraces()
        {
            this.IsExecuting = true;

            var random = new Random(Environment.TickCount);
            var numberItems = random.Next(1, 25);

            Enumerable
                .Range(0, numberItems)
                .ForEach(index =>
                    {
                        this.Logger.LogAsTrace("Processing item {0} of {1}.", index + 1, numberItems);
                        Thread.Sleep((int)(random.NextDouble() * 1000));
                    });

            this.IsExecuting = false;
        }
    }
}