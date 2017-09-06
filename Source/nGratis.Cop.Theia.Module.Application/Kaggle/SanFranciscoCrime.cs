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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class SanFranciscoCrime
    {
        public DateTime OffenceDate { get; set; }

        public Category Category { get; set; }

        public DayOfWeek DayOfWeek => this.OffenceDate.DayOfWeek;

        public PoliceDepartment PoliceDepartment { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public class CsvConfiguration : CsvHelper.Configuration.CsvConfiguration
        {
            static CsvConfiguration()
            {
                CsvConfiguration.Instance = new CsvConfiguration
                {
                    BufferSize = 1 << 24,
                    HasHeaderRecord = true,
                };

                CsvConfiguration.Instance.RegisterClassMap(CsvMap.Instance);
            }

            private CsvConfiguration()
            {
            }

            public static CsvConfiguration Instance { get; private set; }
        }

        private sealed class CsvMap : CsvClassMap<SanFranciscoCrime>
        {
            static CsvMap()
            {
                CsvMap.Instance = new CsvMap();

                CsvMap
                    .Instance
                    .Map(entry => entry.OffenceDate)
                    .Index(0)
                    .TypeConverterOption(DateTimeStyles.None);

                CsvMap
                    .Instance
                    .Map(entry => entry.Category)
                    .Index(1)
                    .TypeConverter(CategoryConverter.Instance);

                CsvMap
                    .Instance
                    .Map(entry => entry.PoliceDepartment)
                    .Index(4)
                    .TypeConverter(PoliceDepartmentConverter.Instance);

                CsvMap
                    .Instance
                    .Map(entry => entry.Longitude)
                    .Index(7)
                    .TypeConverterOption(NumberStyles.Float);

                CsvMap
                    .Instance
                    .Map(entry => entry.Latitude)
                    .Index(8)
                    .TypeConverterOption(NumberStyles.Float);
            }

            private CsvMap()
            {
            }

            public static CsvMap Instance { get; private set; }
        }

        private sealed class CategoryConverter : ITypeConverter
        {
            private static readonly IDictionary<string, Category> Lookup =
                new Dictionary<string, Category>
                {
                    { "SUSPICIOUS OCC", Category.SuspiciousOccurrence },
                    { "TREA", Category.Trespass }
                };

            private CategoryConverter()
            {
            }

            public static CategoryConverter Instance { get; } = new CategoryConverter();

            public string ConvertToString(TypeConverterOptions options, object value)
            {
                return value.ToString();
            }

            public object ConvertFromString(TypeConverterOptions options, string text)
            {
                if (CategoryConverter.Lookup.TryGetValue(text, out Category category))
                {
                    return category;
                }

                return Enum.Parse(
                    typeof(Category),
                    Regexes.InputCleaner.Replace(text, string.Empty).Split('/')[0],
                    true);
            }

            public bool CanConvertFrom(Type type)
            {
                return type == typeof(string);
            }

            public bool CanConvertTo(Type type)
            {
                return type == typeof(string);
            }

            private static class Regexes
            {
                public static readonly Regex InputCleaner = new Regex(@"[\s-]", RegexOptions.Compiled);
            }
        }

        private sealed class PoliceDepartmentConverter : ITypeConverter
        {
            static PoliceDepartmentConverter()
            {
                PoliceDepartmentConverter.Instance = new PoliceDepartmentConverter();
            }

            private PoliceDepartmentConverter()
            {
            }

            public static PoliceDepartmentConverter Instance { get; private set; }

            public string ConvertToString(TypeConverterOptions options, object value)
            {
                return value.ToString();
            }

            public object ConvertFromString(TypeConverterOptions options, string text)
            {
                return Enum.Parse(typeof(PoliceDepartment), text, true);
            }

            public bool CanConvertFrom(Type type)
            {
                return type == typeof(string);
            }

            public bool CanConvertTo(Type type)
            {
                return type == typeof(string);
            }
        }
    }

    public enum PoliceDepartment
    {
        Unknown,
        Bayview,
        Central,
        Ingleside,
        Mission,
        Northern,
        Park,
        Richmond,
        Southern,
        Taraval,
        Tenderloin
    }

    public enum Category
    {
        Unknown,
        Arson,
        Assault,
        BadChecks,
        Bribery,
        Burglary,
        DisorderlyConduct,
        DrivingUnderTheInfluence,
        Drug,
        Drunkenness,
        Embezzlement,
        Extortion,
        FamilyOffenses,
        Forgery,
        Fraud,
        Gambling,
        Kidnapping,
        Larceny,
        LiquorLaws,
        Loitering,
        MissingPerson,
        NonCriminal,
        OtherOffenses,
        Pornography,
        Prostitution,
        RecoveredVehicle,
        Robbery,
        Runaway,
        SecondaryCodes,
        SexOffensesForcible,
        SexOffensesNonForcible,
        StolenProperty,
        Suicide,
        SuspiciousOccurrence,
        Trespass,
        Vandalism,
        VehicleTheft,
        Warrants,
        WeaponLaws
    }
}