namespace Cop.Theia.Contract
{
    using System;
    using System.Collections.Generic;

    public interface IModule
    {
        Guid Id { get; }

        IEnumerable<Topic> Topics { get; }
    }
}