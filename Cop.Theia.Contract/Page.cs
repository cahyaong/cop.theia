namespace Cop.Theia.Contract
{
    using System;

    public class Page
    {
        public Page(string name, string source)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentException();
            }

            this.Name = name;
            this.SourceUri = new Uri(source, UriKind.Relative);
        }

        public string Name { get; private set; }

        public Uri SourceUri { get; private set; }
    }
}