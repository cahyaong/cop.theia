namespace Cop.Theia.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Cop.Theia.Contract;

    [Export(typeof(IModuleProvider))]
    internal class CopModuleProvider : IModuleProvider
    {
        private readonly Lazy<IEnumerable<Assembly>> deferredModuleAssemblies = new Lazy<IEnumerable<Assembly>>(OnModuleAssembliesMaterializing);

        public IEnumerable<Assembly> FindModuleAssemblies()
        {
            return this.deferredModuleAssemblies.Value;
        }

        private static IEnumerable<Assembly> OnModuleAssembliesMaterializing()
        {
            var appAssembly = Assembly.GetExecutingAssembly();
            var moduleFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Modules");
            var assemblies = new List<Assembly> { appAssembly };

            Directory
                .GetFiles(moduleFolderPath, "Cop.Theia.Module.*.dll")
                .ToList()
                .ForEach(file => assemblies.Add(Assembly.LoadFile(file)));

            return assemblies;
        }
    }
}