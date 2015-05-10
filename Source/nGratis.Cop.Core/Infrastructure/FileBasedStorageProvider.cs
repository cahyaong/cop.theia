﻿// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="FileBasedStorageProvider.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Friday, 3 April 2015 12:10:59 AM</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using JetBrains.Annotations;
    using nGratis.Cop.Core.Contract;

    [UsedImplicitly]
    public class FileBasedStorageProvider : IStorageProvider
    {
        public FileBasedStorageProvider(Uri rootFolderUri)
        {
            Guard.AgainstNullArgument(() => rootFolderUri);
            Guard.AgainstInvalidArgument(!rootFolderUri.IsFile, () => rootFolderUri);

            var rootFolderPath = Path.GetDirectoryName(rootFolderUri.AbsolutePath);
            Guard.AgainstUnexpectedNullValue(rootFolderPath);

            this.RootUri = new Uri(rootFolderPath, UriKind.Absolute);
        }

        [UsedImplicitly]
        public Uri RootUri { get; private set; }

        public Stream LoadData(IDataSpecification dataSpecification)
        {
            Guard.AgainstNullArgument(() => dataSpecification);

            return File.Open(
                Path.Combine(this.RootUri.LocalPath, dataSpecification.FullName),
                FileMode.Open);
        }

        public void SaveData(IDataSpecification dataSpecification, Stream dataStream)
        {
            Guard.AgainstNullArgument(() => dataSpecification);
            Guard.AgainstNullArgument(() => dataStream);

            var filePath = Path.Combine(this.RootUri.LocalPath, dataSpecification.FullName);

            Guard.AgainstInvalidOperation(
                File.Exists(filePath),
                () => "Failed to save data".WithMessageDetails(MessageDetail.New("Path", filePath)));

            dataStream.Position = 0;

            if (dataSpecification.ContentMime.IsTextDocument())
            {
                using (var reader = new StreamReader(dataStream))
                using (var writer = new StreamWriter(File.OpenWrite(filePath), Encoding.UTF8))
                {
                    writer.Write(reader.ReadToEnd());
                    writer.Flush();
                }
            }
            else
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dataStream.CopyTo(fileStream);
                    dataStream.Flush();
                }
            }
        }
    }
}