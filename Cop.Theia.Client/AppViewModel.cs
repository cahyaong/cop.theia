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
            this.Modules = Enumerable.Empty<IModule>();
        }

        [ImportingConstructor]
        public AppViewModel([ImportMany] IEnumerable<IModule> modules)
        {
            this.Modules = modules;
        }

        public IEnumerable<IModule> Modules { get; private set; }
    }
}