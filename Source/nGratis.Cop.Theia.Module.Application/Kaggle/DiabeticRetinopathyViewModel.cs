// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DiabeticRetinopathyViewModel.cs" company="nGratis">
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
// <creation_timestamp>Sunday, 29 March 2015 4:36:35 AM</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Theia.Module.Application.Kaggle
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Windows.Media;
    using nGratis.Cop.Core;
    using nGratis.Cop.Core.Vision.Imaging;
    using nGratis.Cop.Core.Wpf;
    using ReactiveUI;

    [Export]
    public class DiabeticRetinopathyViewModel : BaseFormPageViewModel
    {
        private readonly IImageProvider imageProvider;

        private ImageSource rawImage;

        private Range topBrightnessRange;

        [ImportingConstructor]
        public DiabeticRetinopathyViewModel(IImageProvider imageProvider)
        {
            Assumption.ThrowWhenNullArgument(() => imageProvider);

            this.imageProvider = imageProvider;

            this.TopBrightnessRange = new Range();
        }

        [AsField(FieldMode.Input, FieldType.File, "Image file path:")]
        public string ImageFilePath { get; set; }

        [AsField(FieldMode.Input, FieldType.Auto, "Top n-th brightness percentile:")]
        public Range TopBrightnessRange
        {
            get { return this.topBrightnessRange; }
            set { this.RaiseAndSetIfChanged(ref this.topBrightnessRange, value); }
        }

        [AsField(FieldMode.Output, FieldType.Image, "Raw image:")]
        public ImageSource RawImage
        {
            get { return this.rawImage; }
            private set { this.RaiseAndSetIfChanged(ref this.rawImage, value); }
        }

        [AsFieldCallback]
        private void OnImageFilePathChanged()
        {
            if (!File.Exists(this.ImageFilePath))
            {
                this.RawImage = null;
                return;
            }

            this.RawImage = this
                .imageProvider
                .LoadImage(new Uri(this.ImageFilePath).ToDataSpecification())
                .ToImageSource();
        }

        [AsFieldCallback]
        private void OnTopBrightnessRangeChanged()
        {
        }
    }
}