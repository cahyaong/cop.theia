// --------------------------------------------------------------------------------------------------------------------
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CsvHelper;
    using Humanizer;
    using nGratis.Cop.Core.Wpf;
    using ReactiveUI;

    [Export]
    public class SanFranciscoCrimeViewModel : BaseFormViewModel
    {
        private Category category;

        private IEnumerable<SanFranciscoCrime> crimes;

        private ChartConfiguration chartConfiguration;

        [ImportingConstructor]
        public SanFranciscoCrimeViewModel()
        {
        }

        [AsField(FieldMode.Input, FieldType.File, "Data file path:")]
        public string DataFilePath
        {
            get;
            set;
        }

        [AsField(FieldMode.Input, FieldType.Auto, "Category:")]
        public Category Category
        {
            get { return this.category; }
            set { this.RaiseAndSetIfChanged(ref this.category, value); }
        }

        public IEnumerable<SanFranciscoCrime> Crimes
        {
            get { return this.crimes; }
            private set { this.RaiseAndSetIfChanged(ref this.crimes, value); }
        }

        public ChartConfiguration ChartConfiguration
        {
            get { return this.chartConfiguration; }
            private set { this.RaiseAndSetIfChanged(ref this.chartConfiguration, value); }
        }

        [AsFieldCallback]
        private async Task<CallbackResult> OnDataFilePathChanged()
        {
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
                        try
                        {
                            this.Crimes = parser
                                .GetRecords<SanFranciscoCrime>()
                                .ToList();
                        }
                        catch (FormatException exception)
                        {
                            throw new ValueUpdateException(
                                "CSV file does not contain San Francisco crime data",
                                exception);
                        }
                    }
                });

            this.Category = Category.Arson;

            return CallbackResult.OnSuccessful();
        }

        [AsFieldCallback]
        private async Task<CallbackResult> OnCategoryChanged()
        {
            if (this.Category == Category.Unknown || this.Crimes == null)
            {
                return CallbackResult.OnSuccessful();
            }

            var configurations = await Task.Run(() =>
                {
                    return this
                       .Crimes
                       .AsParallel()
                       .Where(crime => crime.Category == this.Category)
                       .GroupBy(crime => new { crime.Category, crime.OffenceDate.Year }, crime => crime)
                       .Select(group => new { group.Key.Category, group.Key.Year, Occurrence = group.Count() })
                       .GroupBy(annon => annon.Category, annon => annon)
                       .Select(group => new
                       {
                           Title = group.Key.ToString().Humanize(LetterCasing.Title),
                           Points = group.OrderBy(annon => annon.Year).ToList()
                       })
                       .Select(annon => new SeriesConfiguration(annon.Title, annon.Points, "Year", "Occurrence"))
                       .ToList();
                });

            this.ChartConfiguration = new ChartConfiguration("Occurrence by Category", configurations);

            return CallbackResult.OnSuccessful();
        }
    }
}