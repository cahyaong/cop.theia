// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SdkModule.cs" company="nGratis">
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

namespace nGratis.Cop.Theia.Module.Sdk
{
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Core.Wpf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    [Export(typeof(IModule))]
    public class SdkModule : IModule
    {
        public SdkModule()
        {
            this.Id = new Guid("959B7271-DCF4-4A66-A9C4-68A2617CC525");

            var diagnosticFeature = new Feature(
                "SDK",
                int.MaxValue,
                new Page("Button", "/nGratis.Cop.Theia.Module.Sdk;component/ButtonView.xaml"),
                new Page("Logging", "/nGratis.Cop.Theia.Module.Sdk;component/LoggingView.xaml"),
                new Page("Map", "/nGratis.Cop.Theia.Module.Sdk;component/MapView.xaml"),
                new Page("Progress Indicator", "/nGratis.Cop.Theia.Module.Sdk;component/ProgressIndicatorView.xaml"),
                new Page("Scroll Viewer", "/nGratis.Cop.Theia.Module.Sdk;component/ScrollViewerView.xaml"));

            this.Features = new List<Feature> { diagnosticFeature };
        }

        public Guid Id
        {
            get;
        }

        public IEnumerable<IFeature> Features
        {
            get;
        }
    }
}