namespace Cop.Theia.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CopFeature
    {
        public CopFeature(string name, IEnumerable<CopPage> subtopics)
        {
            // TODO: Create a helper class to handle exception throwing.

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException();
            }

            this.Name = name;
            this.Pages = subtopics ?? Enumerable.Empty<CopPage>();
        }

        public string Name { get; private set; }

        public IEnumerable<CopPage> Pages { get; private set; }
    }
}