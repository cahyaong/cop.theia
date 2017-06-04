// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Mime.cs" company="nGratis">
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
// <creation_timestamp>Sunday, 29 March 2015 7:10:38 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using nGratis.Cop.Core.Contract;

    public sealed class Mime
    {
        public static readonly Mime Unknown = new Mime("unknown", 0, 0, string.Empty);

        public static readonly Mime Csv = new Mime("text/csv", 4180, 0, "csv");

        public static readonly Mime Html = new Mime("text/html", 2854, 0, "html");

        public static readonly Mime Json = new Mime("application/json", 4627, 0, "json");

        public static readonly Mime Jpeg = new Mime("image/jpeg", 1341, 10918, "jpeg", "jpg");

        public static readonly Mime Mpeg4 = new Mime("video/mp4", 4337, 0, "mp4");

        public static readonly Mime Saz = new Mime("application/x-fiddler-session-archive", 0, 0, "saz");

        public static readonly Mime Png = new Mime("image/png", 2083, 15948, "png");

        public static readonly Mime Warc = new Mime("application/warc", 0, 28500, "warc");

        public static readonly Mime WebForm = new Mime("application/x-www-form-urlencoded", 0, 0, string.Empty);

        public static readonly Mime Xml = new Mime("text/xml", 3023, 0, "xml");

        private static readonly IDictionary<string, Mime> UniqueIdToMimeMapping;

        private static readonly IDictionary<string, Mime> NameToMimeMapping;

        static Mime()
        {
            var mimes = typeof(Mime)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => field.GetValue(typeof(Mime)))
                .OfType<Mime>()
                .ToList();

            Mime.UniqueIdToMimeMapping = mimes
                .ToDictionary(mime => mime.UniqueId, mime => mime);

            Mime.NameToMimeMapping = mimes
                .SelectMany(mime => mime
                    .Names
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Select(name => new { Name = name, Mime = mime }))
                .ToDictionary(annon => annon.Name, annon => annon.Mime);
        }

        private Mime(string uniqueId, int rfcId, int isoId, params string[] names)
        {
            Guard.Require.IsNotEmpty(uniqueId);
            Guard.Require.IsNotEmpty(names);

            this.UniqueId = uniqueId;
            this.RfcId = rfcId;
            this.IsoId = isoId;
            this.Names = names;
        }

        public string UniqueId { get; }

        public int RfcId { get; }

        public int IsoId { get; }

        public IEnumerable<string> Names { get; }

        public static Mime ParseByUniqueId(string uniqueId)
        {
            Guard.Require.IsNotEmpty(uniqueId);
            Guard.Require.IsTrue(Mime.UniqueIdToMimeMapping.ContainsKey(uniqueId));

            return Mime.UniqueIdToMimeMapping[uniqueId];
        }

        public static Mime ParseByName(string name)
        {
            Guard.Require.IsNotEmpty(name);

            name = name.Replace(".", string.Empty);
            Guard.Require.IsTrue(Mime.NameToMimeMapping.ContainsKey(name));

            return Mime.NameToMimeMapping[name];
        }

        public bool IsTextDocument()
        {
            return this.UniqueId.Split('/').First() == "text";
        }
    }
}