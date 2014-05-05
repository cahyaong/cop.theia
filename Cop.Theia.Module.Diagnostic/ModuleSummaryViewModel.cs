namespace Cop.Theia.Module.Diagnostic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Cop.Theia.Contract;

    using ReactiveUI;

    [Export]
    public class ModuleSummaryViewModel : ReactiveObject
    {
        private IEnumerable<AssemblyViewModel> assemblyVms;

        [ImportingConstructor]
        public ModuleSummaryViewModel(IModuleProvider moduleProvider)
        {
            if (moduleProvider == null)
            {
                throw new ArgumentNullException();
            }

            this.AssemblyVms = moduleProvider
                .FindModuleAssemblies()
                .Where(assembly => assembly.FullName.Contains("Cop.Theia.Module"))
                .Select(assmebly => new AssemblyViewModel(assmebly))
                .ToList();
        }

        public IEnumerable<AssemblyViewModel> AssemblyVms
        {
            get { return this.assemblyVms; }
            set { this.RaiseAndSetIfChanged(ref this.assemblyVms, value); }
        }
    }
}