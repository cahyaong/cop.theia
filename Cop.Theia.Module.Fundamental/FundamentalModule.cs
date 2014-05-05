namespace Cop.Theia.Module.Fundamental
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
            this.Id = new Guid("484BF7AD-9B9A-43E0-9BF0-C84C30AC7C38");

            // Use custom attribute to generate the topics and sub-topics.

            var histogramPage = new Page("Histogram", @"/Cop.Theia.Module.Fundamental;component/Histogram/HistogramView.xaml");
            var fundamentalFeature = new Feature("Fundamental", new List<Page> { histogramPage });

            this.Features = new List<Feature> { fundamentalFeature };
        }

        public Guid Id { get; private set; }

        public IEnumerable<Feature> Features { get; private set; }
    }
}