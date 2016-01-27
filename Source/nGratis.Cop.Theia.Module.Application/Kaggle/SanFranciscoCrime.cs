// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SanFranciscoCrime.cs" company="nGratis">
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
// <creation_timestamp>Friday, 22 January 2016 10:54:09 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Theia.Module.Application.Kaggle
{
    using System;
    using LINQtoCSV;

    internal class SanFranciscoCrime
    {
        [CsvColumn(Name = "Dates", FieldIndex = 1)]
        public DateTime OffenceDate { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public string Category { get; set; }

        [CsvColumn(Name = "Descript", FieldIndex = 3)]
        public string Description { get; set; }

        [CsvColumn(FieldIndex = 4)]
        public string DayOfWeek { get; set; }

        [CsvColumn(Name = "PdDistrict", FieldIndex = 5)]
        public string PoliceDepartment { get; set; }

        [CsvColumn(FieldIndex = 6)]
        public string Resolution { get; set; }

        [CsvColumn(FieldIndex = 7)]
        public string Address { get; set; }

        [CsvColumn(Name = "X", FieldIndex = 8)]
        public double Longitude { get; set; }

        [CsvColumn(Name = "Y", FieldIndex = 9)]
        public double Latitude { get; set; }
    }
}