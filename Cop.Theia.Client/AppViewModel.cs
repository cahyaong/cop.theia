namespace Cop.Theia.Client
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Cop.Theia.Contract;

    using ReactiveUI;

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class AppViewModel : ReactiveObject
    {
        public AppViewModel()
        {
            this.Modules = Enumerable.Empty<ICopModule>();
        }

        [ImportingConstructor]
        public AppViewModel([ImportMany] IEnumerable<ICopModule> modules)
        {
            this.Modules = modules;
        }

        public IEnumerable<ICopModule> Modules { get; private set; }
    }
}