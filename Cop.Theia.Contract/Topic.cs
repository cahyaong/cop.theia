namespace Cop.Theia.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Topic
    {
        public Topic(string name, IEnumerable<Subtopic> subtopics)
        {
            // TODO: Create a helper class to handle exception throwing.

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException();
            }

            this.Name = name;
            this.Subtopics = subtopics ?? Enumerable.Empty<Subtopic>();
        }

        public string Name { get; private set; }

        public IEnumerable<Subtopic> Subtopics { get; private set; }
    }
}