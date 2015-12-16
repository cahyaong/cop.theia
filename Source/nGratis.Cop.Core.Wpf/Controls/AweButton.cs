// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AweButton.cs" company="nGratis">
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
// <creation_timestamp>Sunday, 10 May 2015 12:54:19 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core.Wpf
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class AweButton : Button
    {
        public static readonly DependencyProperty IconGeometryProperty = DependencyProperty.Register(
            "IconGeometry",
            typeof(Geometry),
            typeof(AweButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty AccentColorProperty = DependencyProperty.Register(
            "AccentColor",
            typeof(Color),
            typeof(AweButton),
            new PropertyMetadata(Color.FromArgb(0xFF, 0x7F, 0x7F, 0x7F)));

        public static readonly DependencyProperty MeasurementProperty = DependencyProperty.Register(
            "Measurement",
            typeof(Measurement),
            typeof(AweButton),
            new PropertyMetadata(Measurement.M));

        public static readonly DependencyProperty IsBorderHiddenProperty = DependencyProperty.Register(
            "IsBorderHidden",
            typeof(bool),
            typeof(AweButton),
            new PropertyMetadata(true));

        public Geometry IconGeometry
        {
            get { return (Geometry)this.GetValue(IconGeometryProperty); }
            set { this.SetValue(IconGeometryProperty, value); }
        }

        public Color AccentColor
        {
            get { return (Color)this.GetValue(AccentColorProperty); }
            set { this.SetValue(AccentColorProperty, value); }
        }

        public Measurement Measurement
        {
            get { return (Measurement)this.GetValue(MeasurementProperty); }
            set { this.SetValue(MeasurementProperty, value); }
        }

        public bool IsBorderHidden
        {
            get { return (bool)this.GetValue(IsBorderHiddenProperty); }
            set { this.SetValue(IsBorderHiddenProperty, value); }
        }
    }
}