namespace Cop.Theia.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Cop.Theia.Contract;

    [Export(typeof(ICopModule))]
    internal class FundamentalModule : ICopModule
    {
        public FundamentalModule()
        {
            this.Id = new Guid("E0DD329A-15C6-441E-A713-7DD827F3CD5B");

            // Use custom attribute to generate the topics and sub-topics.

            var histogramPage = new CopPage("Histogram", @"Fundamental\HistogramView.xaml");
            var fundamentalFeature = new CopFeature("Fundamental", new List<CopPage> { histogramPage });

            this.Features = new List<CopFeature> { fundamentalFeature };
        }

        public Guid Id { get; private set; }

        public IEnumerable<CopFeature> Features { get; private set; }
    }
}