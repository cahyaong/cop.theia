namespace Cop.Theia.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Reflection;

    using Caliburn.Micro;

    internal class AppBootstrapper : Bootstrapper<AppViewModel>
    {
        private CompositionContainer mefContainer;

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }

        protected override void Configure()
        {
            var copCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

            this.mefContainer = new CompositionContainer(
                new AggregateCatalog(copCatalog),
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