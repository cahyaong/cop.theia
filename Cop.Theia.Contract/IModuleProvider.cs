using System.Collections.Generic;

namespace Cop.Theia.Contract
{
    using System.Reflection;

    public interface IModuleProvider
    {
        IEnumerable<Assembly> FindModuleAssemblies();

        IEnumerable<Assembly> FindInternalAssemblies();
    }
}