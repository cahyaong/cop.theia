namespace nGratis.Cop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Markup;

    [assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "nGratis.Cop.Core")]
    public class SharedResourceDictionary : ResourceDictionary
    {
        private static readonly Dictionary<Uri, ResourceDictionary> _SharedDictionaries = new Dictionary<Uri, ResourceDictionary>();

        private Uri _sourceUri;

        public new Uri Source
        {
            get
            {
                return this._sourceUri;
            }

            set
            {
                this._sourceUri = value;

                if (!_SharedDictionaries.ContainsKey(value))
                {
                    base.Source = value;
                    SharedResourceDictionary._SharedDictionaries.Add(value, this);
                }
                else
                {
                    this.MergedDictionaries.Add(SharedResourceDictionary._SharedDictionaries[value]);
                }
            }
        }
    }
}