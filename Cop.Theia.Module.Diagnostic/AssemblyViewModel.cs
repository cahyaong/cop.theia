namespace Cop.Theia.Module.Diagnostic
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using ReactiveUI;

    public class AssemblyViewModel : ReactiveObject
    {
        private static readonly Regex TitleRegex = new Regex(@"^Cop\.Theia\.Module\.(?<name>\w+)$", RegexOptions.Singleline);

        private string name;

        private string version;

        private DateTime modifiedTimestamp;

        private string fileName;

        private string configuration;

        public AssemblyViewModel(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException();
            }

            var titleMatch = AssemblyViewModel.TitleRegex.Match(assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title);

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
            get { return this.name; }
            private set { this.RaiseAndSetIfChanged(ref this.name, value); }
        }

        public string Version
        {
            get { return this.version; }
            private set { this.RaiseAndSetIfChanged(ref this.version, value); }
        }

        public DateTime ModifiedTimestamp
        {
            get { return this.modifiedTimestamp; }
            private set { this.RaiseAndSetIfChanged(ref this.modifiedTimestamp, value); }
        }

        public string FileName
        {
            get { return this.fileName; }
            private set { this.RaiseAndSetIfChanged(ref this.fileName, value); }
        }

        public string Configuration
        {
            get { return this.configuration; }
            private set { this.RaiseAndSetIfChanged(ref this.configuration, value); }
        }
    }
}