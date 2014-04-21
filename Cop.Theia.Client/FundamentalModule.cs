namespace Cop.Theia.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Cop.Theia.Contract;

    [Export(typeof(IModule))]
    internal class FundamentalModule : IModule
    {
        public FundamentalModule()
        {
            this.Id = new Guid("E0DD329A-15C6-441E-A713-7DD827F3CD5B");

            // Use custom attribute to generate the topics and sub-topics.

            var subtopic = new Subtopic("Histogram", @"Fundamental\HistogramView.xaml");
            var topic = new Topic("Fundamental", new List<Subtopic> { subtopic });

            this.Topics = new List<Topic> { topic };
        }

        public Guid Id { get; private set; }

        public IEnumerable<Topic> Topics { get; private set; }
    }
}