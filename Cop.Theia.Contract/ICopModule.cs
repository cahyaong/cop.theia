namespace Cop.Theia.Contract
{
    using System;
    using System.Collections.Generic;

    public interface ICopModule
    {
        Guid Id { get; }

        IEnumerable<CopFeature> Features { get; }
    }
}