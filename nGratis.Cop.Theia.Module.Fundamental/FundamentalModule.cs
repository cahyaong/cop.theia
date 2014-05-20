// --------------------------------------------------------------------------------
// <copyright file="FundamentalModule.cs" company="nGratis">
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

namespace nGratis.Cop.Theia.Module.Fundamental
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using nGratis.Cop.Core.Contract;

    [Export(typeof(IModule))]
    internal class FundamentalModule : IModule
    {
        public FundamentalModule()
        {
            this.Id = new Guid("484BF7AD-9B9A-43E0-9BF0-C84C30AC7C38");

            // FIXME: Use custom attribute to generate features and their pages.

            var histogramPage = new Page("Histogram", @"/nGratis.Cop.Theia.Module.Fundamental;component/Histogram/HistogramView.xaml");
            var fundamentalFeature = new Feature("Fundamental", new List<Page> { histogramPage });

            this.Features = new List<Feature> { fundamentalFeature };
        }

        public Guid Id { get; private set; }

        public IEnumerable<Feature> Features { get; private set; }
    }
}