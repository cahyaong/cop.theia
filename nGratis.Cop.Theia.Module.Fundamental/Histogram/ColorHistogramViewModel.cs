// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorHistogramViewModel.cs" company="nGratis">
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

namespace nGratis.Cop.Theia.Module.Fundamental
{
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Windows.Media;

    using nGratis.Cop.Core;
    using nGratis.Cop.Core.Media;
    using nGratis.Cop.Core.Wpf;

    using ReactiveUI;

    [Export]
    public class ColorHistogramViewModel : BasePageViewModel
    {
        private readonly IImageProvider imageProvider;

        private ImageSource rawImage;

        [ImportingConstructor]
        public ColorHistogramViewModel(IImageProvider imageProvider)
        {
            Assumption.ThrowWhenNullArgument(() => imageProvider);

            this.imageProvider = imageProvider;
        }

        [AsField(FieldMode.Input, FieldType.File, "Image File Path:")]
        public string ImageFilePath { get; set; }

        [AsField(FieldMode.Output, FieldType.Image, "Raw Image:")]
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
    }
}