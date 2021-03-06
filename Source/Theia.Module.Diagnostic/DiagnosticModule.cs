﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticModule.cs" company="nGratis">
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

namespace nGratis.Cop.Theia.Module.Diagnostic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Core.Wpf;

    [Export(typeof(IModule))]
    public class DiagnosticModule : IModule
    {
        public DiagnosticModule()
        {
            this.Id = new Guid("FAD2D7B3-60B5-46F8-93CC-CE0F21C440E4");

            var diagnosticFeature = new Feature(
                "Diagnostic",
                int.MaxValue,
                new Page("Module Summary", "/nGratis.Cop.Theia.Module.Diagnostic;component/ModuleSummaryView.xaml"),
                new Page("Logging", "/nGratis.Cop.Theia.Module.Diagnostic;component/LoggingView.xaml"));

            this.Features = new List<Feature> { diagnosticFeature };
        }

        public Guid Id { get; }

        public IEnumerable<IFeature> Features { get; }
    }
}