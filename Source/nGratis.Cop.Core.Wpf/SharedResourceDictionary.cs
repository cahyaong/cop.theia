// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="SharedResourceDictionary.cs" company="nCognito">
//     Copyright (c) 2013 - 2014 nCognito. All rights reserved.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class SharedResourceDictionary : ResourceDictionary
    {
        private static readonly Dictionary<Uri, ResourceDictionary> SharedDictionaries = new Dictionary<Uri, ResourceDictionary>();

        private Uri source;

        public new Uri Source
        {
            get
            {
                return this.source;
            }

            set
            {
                this.source = value;

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