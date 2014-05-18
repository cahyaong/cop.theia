// --------------------------------------------------------------------------------
// <copyright file="FileDialogButton.cs" company="nGratis">
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

namespace Cop.Theia.Core.Wpf
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;

    using FirstFloor.ModernUI.Presentation;

    using Microsoft.Win32;

    public class FileDialogButton : Button
    {
        public static readonly DependencyProperty SelectedFilePathProperty = DependencyProperty.Register(
            "SelectedFilePath", typeof(string), typeof(FileDialogButton), new PropertyMetadata(null));

        public FileDialogButton()
        {
            this.Command = new RelayCommand(this.OnMouseClicked, _ => true);
        }

        public string SelectedFilePath
        {
            get { return (string)this.GetValue(FileDialogButton.SelectedFilePathProperty); }
            set { this.SetValue(FileDialogButton.SelectedFilePathProperty, value); }
        }

        private void OnMouseClicked(object parameter)
        {
            var fileDialog = new OpenFileDialog
                {
                    InitialDirectory = File.Exists(this.SelectedFilePath)
                        ? Path.GetDirectoryName(this.SelectedFilePath)
                        : Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                };

            var isOkSelected = fileDialog.ShowDialog();

            if (isOkSelected.HasValue && isOkSelected.Value)
            {
                this.SelectedFilePath = fileDialog.FileName;
            }
        }
    }
}