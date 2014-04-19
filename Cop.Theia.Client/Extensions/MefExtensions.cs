namespace Cop.Theia.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;

    internal static class MefExtensions
    {
        public static void AddExport<TKey>(this CompositionBatch compositionBatch, Func<object> createInstance)
        {
            var typeName = typeof(TKey).FullName;

            var export = new Export(
                new ExportDefinition(typeName, new Dictionary<string, object> { { "ExportTypeIdentity", typeName } }),
                createInstance);

            compositionBatch.AddExport(export);
        }
    }
}