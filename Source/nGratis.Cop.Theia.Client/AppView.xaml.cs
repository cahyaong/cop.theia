// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="AppView.xaml.cs" company="nGratis">
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
// <creation_timestamp>Wednesday, 24 December 2014 12:14:47 AM</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Theia.Client
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using nGratis.Cop.Core.Contract;

    public partial class AppView
    {
        public AppView()
        {
            this.InitializeComponent();
        }

        private async void OnViewLoaded(object sender, RoutedEventArgs args)
        {
            var vm = (AppViewModel)this.DataContext;
            var page = default(IPage);

            await Task.Delay(TimeSpan.FromSeconds(0.1d));

            await Task.Run(() =>
                {
                    page = vm
                        .Modules
                        .SelectMany(module => module.Features)
                        .OrderBy(feature => feature.Order)
                        .ThenBy(feature => feature.Name)
                        .SelectMany(feature => feature.Pages)
                        .FirstOrDefault();
                });

            this.ContentSource = page != null ? page.SourceUri : null;
        }
    }
}