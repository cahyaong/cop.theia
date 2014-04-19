namespace Cop.Theia.Client
{
    using System;
    using System.Windows;

    using Caliburn.Micro;

    using FirstFloor.ModernUI.Windows;

    internal class CaliburnContentLoader : DefaultContentLoader
    {
        protected override object LoadContent(Uri uri)
        {
            var content = base.LoadContent(uri);

            if (content == null)
            {
                return null;
            }

            var vm = ViewModelLocator.LocateForView(content);

            if (vm == null)
            {
                return null;
            }

            if (content is DependencyObject)
            {
                ViewModelBinder.Bind(vm, content as DependencyObject, null);
            }

            return content;
        }
    }
}