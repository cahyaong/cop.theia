namespace Cop.Theia.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Reflection;

    using Caliburn.Micro;

    using Cop.Theia.Contract;
    using Cop.Theia.Core;

    internal class AppBootstrapper : Bootstrapper<AppViewModel>
    {
        private readonly Lazy<IModuleProvider> deferredModuleProvider = new Lazy<IModuleProvider>(() => new CopModuleProvider());

        private CompositionContainer mefContainer;

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return Enumerable
                .Empty<Assembly>()
                .Prepend(Assembly.GetExecutingAssembly())
                .Union(this.deferredModuleProvider.Value.FindModuleAssemblies());
        }

        protected override void Configure()
        {
            var catalogs = Enumerable
                .Empty<Assembly>()
                .Prepend(Assembly.GetExecutingAssembly())
                .Union(this.deferredModuleProvider.Value.FindInternalAssemblies())
                .Union(this.deferredModuleProvider.Value.FindModuleAssemblies())
                .Select(assembly => new AssemblyCatalog(assembly));

            this.mefContainer = new CompositionContainer(
                new AggregateCatalog(catalogs),
                CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);

            var caliburnBatch = new CompositionBatch();
            caliburnBatch.AddExport<IWindowManager>(() => new WindowManager());
            this.mefContainer.Compose(caliburnBatch);
        }

        protected override object GetInstance(Type service, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = this.mefContainer.GetExportedValues<object>(contract);

            return exports.Single();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var contract = AttributedModelServices.GetContractName(service);

            return this.mefContainer.GetExportedValues<object>(contract);
        }

        protected override void BuildUp(object instance)
        {
            this.mefContainer.SatisfyImportsOnce(instance);
        }
    }
}