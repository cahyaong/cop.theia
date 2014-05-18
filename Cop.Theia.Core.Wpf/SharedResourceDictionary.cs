namespace Cop.Theia.Core.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Markup;

    [assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "Cop.Theia.Core.Wpf")]
    public class SharedResourceDictionary : ResourceDictionary
    {
        private static readonly Dictionary<Uri, ResourceDictionary> SharedDictionaries = new Dictionary<Uri, ResourceDictionary>();

        private Uri sourceUri;

        public new Uri Source
        {
            get
            {
                return this.sourceUri;
            }

            set
            {
                this.sourceUri = value;

                if (!SharedDictionaries.ContainsKey(value))
                {
                    base.Source = value;
                    SharedResourceDictionary.SharedDictionaries.Add(value, this);
                }
                else
                {
                    this.MergedDictionaries.Add(SharedResourceDictionary.SharedDictionaries[value]);
                }
            }
        }
    }
}