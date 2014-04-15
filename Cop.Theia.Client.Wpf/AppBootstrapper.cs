namespace Cop.Theia.Client.Wpf
{
    using System.Windows.Media;

    using Caliburn.Micro;

    using FirstFloor.ModernUI.Presentation;

    internal class AppBootstrapper : Bootstrapper<AppViewModel>
    {
        public AppBootstrapper()
        {
            AppearanceManager.Current.AccentColor = Color.FromRgb(0x53, 0x81, 0x35);
        }
    }
}