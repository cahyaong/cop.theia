// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScenarioExtensions.cs" company="nGratis">
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
// <creation_timestamp>Monday, 20 April 2015 1:14:58 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core.Testing
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.Windows;
    using nGratis.Cop.Core;
    using nGratis.Cop.Core.Testing.Properties;

    public static class ScenarioExtensions
    {
        public static Guid AsGuid(this DataRow dataRow, string columnName)
        {
            Assumption.ThrowWhenNullArgument(() => dataRow);

            var value = Guid.Empty;

            Assumption.ThrowWhenInvalidOperation(
                () => !Guid.TryParse(dataRow.AsString(columnName), out value),
                Messages.Error_Scenario_InvalidFormat.WithInvariantFormat(columnName, dataRow.Table.TableName, "GUID"));

            return value;
        }

        public static short AsInt16(this DataRow dataRow, string columnName)
        {
            Assumption.ThrowWhenNullArgument(() => dataRow);

            var value = (short)0;

            Assumption.ThrowWhenInvalidOperation(
                () => !short.TryParse(dataRow.AsString(columnName), NumberStyles.Integer, CultureInfo.InvariantCulture, out value),
                Messages.Error_Scenario_InvalidFormat.WithInvariantFormat(columnName, dataRow.Table.TableName, "INT16"));

            return value;
        }

        public static int AsInt32(this DataRow dataRow, string columnName)
        {
            Assumption.ThrowWhenNullArgument(() => dataRow);

            var value = 0;

            Assumption.ThrowWhenInvalidOperation(
                () => !int.TryParse(dataRow.AsString(columnName), NumberStyles.Integer, CultureInfo.InvariantCulture, out value),
                Messages.Error_Scenario_InvalidFormat.WithInvariantFormat(columnName, dataRow.Table.TableName, "INT32"));

            return value;
        }

        public static DateTime AsDateTime(this DataRow dataRow, string columnName)
        {
            Assumption.ThrowWhenNullArgument(() => dataRow);

            var value = DateTime.MinValue;

            Assumption.ThrowWhenInvalidOperation(
                () => !DateTime.TryParseExact(dataRow.AsString(columnName), "O", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out value),
                Messages.Error_Scenario_InvalidFormat.WithInvariantFormat(columnName, dataRow.Table.TableName, "DATETIME"));

            return value;
        }

        public static DateTimeOffset AsDateTimeOffset(this DataRow dataRow, string columnName)
        {
            Assumption.ThrowWhenNullArgument(() => dataRow);

            var value = DateTimeOffset.MinValue;

            Assumption.ThrowWhenInvalidOperation(
                () => !DateTimeOffset.TryParseExact(dataRow.AsString(columnName), "O", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out value),
                Messages.Error_Scenario_InvalidFormat.WithInvariantFormat(columnName, dataRow.Table.TableName, "DATETIMEOFFSET"));

            return value;
        }

        public static Size AsSize(this DataRow dataRow, string columnName)
        {
            Assumption.ThrowWhenNullArgument(() => dataRow);

            var value = default(Size);

            try
            {
                value = Size.Parse(dataRow.AsString(columnName));
            }
            catch (Exception exception)
            {
                Assumption.ThrowWhenInvalidOperation(
                    () => true,
                    Messages.Error_Scenario_InvalidFormat.WithInvariantFormat(columnName, dataRow.Table.TableName, "SIZE"),
                    exception);
            }

            return value;
        }

        public static string AsString(this DataRow dataRow, string columnName)
        {
            Assumption.ThrowWhenNullArgument(() => dataRow);
            Assumption.ThrowWhenNullOrWhitespaceArgument(() => columnName);

            var value = string.Empty;

            try
            {
                value = dataRow[columnName] as string;

                Assumption.ThrowWhenInvalidOperation(
                    () => value == null,
                    Messages.Error_Scenario_InvalidFormat.WithInvariantFormat(columnName, dataRow.Table.TableName, "STRING"));
            }
            catch (ArgumentException exception)
            {
                Assumption.ThrowWhenInvalidOperation(
                    () => true,
                    Messages.Error_Scenario_UndefinedVariable.WithInvariantFormat(columnName, dataRow.Table.TableName),
                    exception);
            }

            // ReSharper disable PossibleNullReferenceException
            return value.Trim();

            // ReSharper restore PossibleNullReferenceException
        }
    }
}