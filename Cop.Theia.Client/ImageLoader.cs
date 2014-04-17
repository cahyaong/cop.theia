namespace Cop.Theia.Client
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using FirstFloor.ModernUI.Windows;

    internal class ImageLoader : IContentLoader
    {
        public Task<object> LoadContentAsync(Uri uri, CancellationToken cancellationToken)
        {
            using (var stream = File.OpenRead(@"E:\02_GeekFactory\cop.theia.resource\39CB98A5-9043-4BC1-BF91-074CDB0F59F5.png"))
            {
                var bitmap = new BitmapImage();

                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();

                return Task.FromResult(new Image { Source = bitmap } as object);
            }
        }
    }
}