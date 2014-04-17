namespace Cop.Theia.Client
{
    using System.Windows;
    using System.Windows.Media;

    using FirstFloor.ModernUI.Presentation;

    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppearanceManager.Current.AccentColor = Color.FromRgb(0x53, 0x81, 0x35);
        }
    }
}