// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeLogger.cs" company="nGratis">
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
// <creation_timestamp>Wednesday, 29 April 2015 1:41:08 PM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using nGratis.Cop.Core.Contract;

    public class CompositeLogger : BaseLogger
    {
        private readonly ConcurrentDictionary<string, ILogger> loggerLookup = new ConcurrentDictionary<string, ILogger>();

        private readonly ConcurrentDictionary<string, IDisposable> subscriptionLookup = new ConcurrentDictionary<string, IDisposable>();

        public CompositeLogger(string id)
            : base(id)
        {
        }

        public override IEnumerable<string> Components
        {
            get
            {
                return this
                    .loggerLookup.Values
                    .SelectMany(logger => logger.Components)
                    .Distinct();
            }
        }

        public void RegisterLoggers(params ILogger[] loggers)
        {
            Guard.AgainstNullArgument(() => loggers);

            loggers
                .Where(logger => !this.loggerLookup.ContainsKey(logger.Id))
                .ForEach(logger =>
                    {
                        this.loggerLookup.TryAdd(logger.Id, logger);

                        var subscription = logger
                            .AsObservable()
                            .Subscribe(this.LogWith);

                        this.subscriptionLookup.TryAdd(logger.Id, subscription);
                    });
        }

        public void UnregisterLoggers(params ILogger[] loggers)
        {
            Guard.AgainstNullArgument(() => loggers);

            loggers
                .Where(logger => this.loggerLookup.ContainsKey(logger.Id))
                .ForEach(logger =>
                {
                    this.loggerLookup.TryRemove(logger.Id, out logger);

                    var subscription = default(IDisposable);
                    this.subscriptionLookup.TryRemove(logger.Id, out subscription);

                    if (subscription != null)
                    {
                        subscription.Dispose();
                    }
                });
        }

        public override void LogWith(Verbosity verbosity, string message)
        {
            this
                .loggerLookup
                .Values
                .ForEach(logger => logger.LogWith(verbosity, message));

            base.LogWith(verbosity, message);
        }

        public override void LogWith(Verbosity verbosity, Exception exception, string message)
        {
            this
                .loggerLookup
                .Values
                .ForEach(logger => logger.LogWith(verbosity, exception, message));

            base.LogWith(verbosity, exception, message);
        }
    }
}