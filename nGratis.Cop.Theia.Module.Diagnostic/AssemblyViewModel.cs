// --------------------------------------------------------------------------------
// <copyright file="AssemblyViewModel.cs" company="nGratis">
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

namespace nGratis.Cop.Theia.Module.Diagnostic
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using ReactiveUI;

    public class AssemblyViewModel : ReactiveObject
    {
        private static readonly Regex _TitleRegex = new Regex(@"^nGratis\.Cop\.Theia\.Module\.(?<name>\w+)$", RegexOptions.Singleline);

        private string _name;

        private string _version;

        private DateTime _modifiedTimestamp;

        private string _fileName;

        private string _configuration;

        public AssemblyViewModel(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException();
            }

            var titleMatch = AssemblyViewModel._TitleRegex.Match(assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title);

            if (!titleMatch.Success)
            {
                throw new InvalidOperationException("Unable to match assembly name for a module");
            }

            this.Name = titleMatch.Groups["name"].Value;
            this.Version = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            this.ModifiedTimestamp = File.GetLastWriteTime(assembly.Location);
            this.FileName = Path.GetFileName(assembly.Location);
            this.Configuration = assembly.GetCustomAttribute<AssemblyConfigurationAttribute>().Configuration;
        }

        public string Name
        {
            get { return this._name; }
            private set { this.RaiseAndSetIfChanged(ref this._name, value); }
        }

        public string Version
        {
            get { return this._version; }
            private set { this.RaiseAndSetIfChanged(ref this._version, value); }
        }

        public DateTime ModifiedTimestamp
        {
            get { return this._modifiedTimestamp; }
            private set { this.RaiseAndSetIfChanged(ref this._modifiedTimestamp, value); }
        }

        public string FileName
        {
            get { return this._fileName; }
            private set { this.RaiseAndSetIfChanged(ref this._fileName, value); }
        }

        public string Configuration
        {
            get { return this._configuration; }
            private set { this.RaiseAndSetIfChanged(ref this._configuration, value); }
        }
    }
}