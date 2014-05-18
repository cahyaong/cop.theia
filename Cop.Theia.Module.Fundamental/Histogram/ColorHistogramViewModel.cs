// --------------------------------------------------------------------------------
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
// --------------------------------------------------------------------------------

namespace Cop.Theia.Module.Fundamental
{
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Windows.Media;

    using Cop.Theia.Core;
    using Cop.Theia.Core.Media;
    using Cop.Theia.Core.Wpf;
    using Cop.Theia.Module.Fundamental.Annotations;

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

        [AsField("ED9DED51-7A16-4348-9759-6FED24244CA4", FieldMode.Input, FieldType.File, "Image File Path:")]
        private string ImageFilePath { get; [UsedImplicitly] set; }

        [AsField("EB0EBB79-6D0B-490F-9CB5-F00A61966A48", FieldMode.Output, FieldType.Image, "Raw Image:")]
        public ImageSource RawImage
        {
            get { return this.rawImage; }
            private set { this.RaiseAndSetIfChanged(ref this.rawImage, value); }
        }

        [AsFieldCallback("ED9DED51-7A16-4348-9759-6FED24244CA4")]
        private void OnImageFilePathChanged()
        {
            if (!File.Exists(this.ImageFilePath))
            {
                this.RawImage = null;
                return;
            }

            this.RawImage = this
                .imageProvider
                .LoadImage(new Uri(this.ImageFilePath))
                .ToImageSource();
        }
    }
}