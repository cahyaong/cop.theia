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

        private readonly Lazy<IEnumerable<Assembly>> deferredInternalAssemblies = new Lazy<IEnumerable<Assembly>>(OnInternalAssembliesMaterializing);

        public IEnumerable<Assembly> FindModuleAssemblies()
        {
            return this.deferredModuleAssemblies.Value;
        }

        public IEnumerable<Assembly> FindInternalAssemblies()
        {
            return this.deferredInternalAssemblies.Value;
        }

        private static IEnumerable<Assembly> OnModuleAssembliesMaterializing()
        {
            var assemblies = Directory
                .GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Modules"), "Cop.Theia.Module.*.dll")
                .Select(Assembly.LoadFile);

            return assemblies;
        }

        private static IEnumerable<Assembly> OnInternalAssembliesMaterializing()
        {
            var assemblies = Directory
                .GetFiles(Directory.GetCurrentDirectory(), "Cop.Theia.*.dll")
                .Where(file => !file.Contains("Module"))
                .Select(Assembly.LoadFile);

            return assemblies;
        }
    }
}