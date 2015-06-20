#region License
//
// Copyright 2002-2015 Drew Noakes
// Ported from Java to C# by Yakov Danilov for Imazen LLC in 2014
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// More information about this project is available at:
//
//    https://github.com/drewnoakes/metadata-extractor-dotnet
//    https://drewnoakes.com/code/exif/
//
#endregion

using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using MetadataExtractor.Formats.FileSystem;
using MetadataExtractor.IO;

namespace MetadataExtractor.Formats.Photoshop
{
    /// <summary>Obtains metadata from Photoshop's PSD files.</summary>
    /// <author>Drew Noakes https://drewnoakes.com</author>
    public static class PsdMetadataReader
    {
        /// <exception cref="System.IO.IOException"/>
        [NotNull]
        public static IReadOnlyList<Directory> ReadMetadata([NotNull] string filePath)
        {
            var directories = new List<Directory>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                directories.AddRange(new PsdReader().Extract(new SequentialStreamReader(stream)));

            directories.Add(new FileMetadataReader().Read(filePath));

            return directories;
        }

        [NotNull]
        public static IReadOnlyList<Directory> ReadMetadata([NotNull] Stream stream)
        {
            return new PsdReader().Extract(new SequentialStreamReader(stream));
        }
    }
}
