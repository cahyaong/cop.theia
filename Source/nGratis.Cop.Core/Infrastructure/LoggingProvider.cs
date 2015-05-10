// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingProvider.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 25 April 2015 12:21:18 PM</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using nGratis.Cop.Core.Contract;

    internal class LoggingProvider : ILoggingProvider
    {
        private readonly ConcurrentDictionary<string, ILogger> loggerLookup = new ConcurrentDictionary<string, ILogger>();

        private readonly CompositeLogger aggregatingLogger = new CompositeLogger("*");
        private bool isDisposed;

        static LoggingProvider()
        {
            Instance = new LoggingProvider();
        }

        private LoggingProvider()
        {
        }

        ~LoggingProvider()
        {
            this.Dispose(false);
        }

        public static ILoggingProvider Instance { get; private set; }

        public ILogger GetLoggerFor(Type type)
        {
            Assumption.ThrowWhenNullArgument(() => type);

            var logger = this
                .loggerLookup
                .GetOrAdd("TYP_{0}".WithInvariantFormat(type.FullName), key => new NLogger(key, "<undefined>"));

            this.aggregatingLogger.RegisterLoggers(logger);

            return logger;
        }

        public ILogger GetLoggerFor(string component)
        {
            Assumption.ThrowWhenNullOrWhitespaceArgument(() => component);

            if (component == "*")
            {
                return this.aggregatingLogger;
            }

            var logger = this
                .loggerLookup
                .GetOrAdd("COM_{0}".WithInvariantFormat(component), key => new NLogger(key, component));

            this.aggregatingLogger.RegisterLoggers(logger);

            return logger;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                this.aggregatingLogger.Dispose();
            }

            this.isDisposed = true;
        }
    }
}