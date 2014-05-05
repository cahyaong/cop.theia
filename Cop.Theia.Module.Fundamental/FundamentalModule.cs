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
            this.Id = new Guid("E0DD329A-15C6-441E-A713-7DD827F3CD5B");

            // Use custom attribute to generate the topics and sub-topics.

            var histogramPage = new Page("Histogram", @"/Cop.Theia.Module.Fundamental;component/Histogram/HistogramView.xaml");
            var fundamentalFeature = new Feature("Fundamental", new List<Page> { histogramPage });

            this.Features = new List<Feature> { fundamentalFeature };
        }

        public Guid Id { get; private set; }

        public IEnumerable<Feature> Features { get; private set; }
    }
}