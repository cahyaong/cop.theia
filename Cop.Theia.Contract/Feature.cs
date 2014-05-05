namespace Cop.Theia.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Feature
    {
        public Feature(string name, IEnumerable<Page> subtopics)
        {
            // TODO: Create a helper class to handle exception throwing.

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException();
            }

            this.Name = name;
            this.Pages = subtopics ?? Enumerable.Empty<Page>();
        }

        public string Name { get; private set; }

        public IEnumerable<Page> Pages { get; private set; }
    }
}