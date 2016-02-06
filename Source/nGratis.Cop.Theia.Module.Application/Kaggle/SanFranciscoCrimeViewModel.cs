﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SanFranciscoCrimeViewModel.cs" company="nGratis">
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
// <creation_timestamp>Sunday, 29 March 2015 4:36:35 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Theia.Module.Application.Kaggle
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Threading.Tasks;
    using CsvHelper;
    using nGratis.Cop.Core.Wpf;

    [Export]
    public class SanFranciscoCrimeViewModel : BaseFormViewModel
    {
        private readonly List<SanFranciscoCrime> crimes = new List<SanFranciscoCrime>();

        [ImportingConstructor]
        public SanFranciscoCrimeViewModel()
        {
        }

        [AsField(FieldMode.Input, FieldType.File, "Data file path:")]
        public string DataFilePath { get; set; }

        [AsFieldCallback]
        private async Task<CallbackResult> OnDataFilePathChanged()
        {
            this.crimes.Clear();

            if (!File.Exists(this.DataFilePath))
            {
                return CallbackResult.OnFailure();
            }

            await Task.Run(() =>
                {
                    using (var stream = File.OpenRead(this.DataFilePath))
                    using (var reader = new StreamReader(stream))
                    using (var parser = new CsvReader(reader, SanFranciscoCrime.CsvConfiguration.Instance))
                    {
                        this.crimes.AddRange(parser.GetRecords<SanFranciscoCrime>());
                    }
                });

            return CallbackResult.OnSuccessful();
        }
    }
}