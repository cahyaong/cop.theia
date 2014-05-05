using System;
using System.Collections.Generic;

namespace Cop.Theia.Module.Diagnostic
{
    using System.ComponentModel.Composition;

    using Cop.Theia.Contract;

    [Export(typeof(IModule))]
    public class DiagnosticModule : IModule
    {
        public DiagnosticModule()
        {
            this.Id = new Guid("FAD2D7B3-60B5-46F8-93CC-CE0F21C440E4");

            var moduleSummaryPage = new Page("Module Summary", "/Cop.Theia.Module.Diagnostic;component/ModuleSummaryView.xaml");

            this.Features = new List<Feature>
                {
                    new Feature("Diagnostic", new List<Page> { moduleSummaryPage })
                };
        }

        public Guid Id { get; private set; }

        public IEnumerable<Feature> Features { get; private set; }
    }
}