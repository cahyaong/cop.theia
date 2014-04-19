namespace Cop.Theia.Client
{
    using System.ComponentModel.Composition;

    using ReactiveUI;

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class AppViewModel : ReactiveObject
    {
    }
}