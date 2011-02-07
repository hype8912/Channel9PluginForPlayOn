// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

namespace Channel9Plugin
{
    #region Usings

    using System;
    using System.Collections.Generic;
    ////using System.ComponentModel.Composition;

    #endregion

    /// <summary>
    /// Class responsible for creating new media folders.
    /// </summary>
    ////[Export]
    ////[PartCreationPolicy(CreationPolicy.Shared)]
    internal sealed class MediaFolderFactory
    {
        #region Declarations

        /// <summary>
        /// Container for holding media folders and ids.
        /// </summary>
        private readonly IDictionary<string, MediaFolder> folderLookup = new Dictionary<string, MediaFolder>();

        #endregion

        /// <summary>
        /// Creates the specified title.
        /// </summary>
        /// <param name="title">The folder title.</param>
        /// <param name="sourceUrl">The source URL.</param>
        /// <param name="dynamic">If set to <c><c>true</c></c> [dynamic].</param>
        /// <param name="thumbnail">The thumbnail url.</param>
        /// <returns>A new media folder.</returns>
        /// <exception cref="ApplicationException">Could not find <see cref="MediaFolder"/> export for Source URL.</exception>
        internal MediaFolder Create(string title, string sourceUrl, bool dynamic, string thumbnail)
        {
            ////ExportCollection<MediaFolder> folderExports = Owner.Provider.Container.GetExports<MediaFolder>();

            ////Export<MediaFolder> mediaFolderExport = null;
            ////foreach (Export<MediaFolder> export in folderExports)
            ////{
            ////    if (!export.Metadata.ContainsKey("Site"))
            ////    {
            ////        mediaFolderExport = export;
            ////    }
            ////    else if (sourceUrl.StartsWith(export.Metadata["Site"].ToString()))
            ////    {
            ////        mediaFolderExport = export;
            ////        break;
            ////    }
            ////}

            ////if (mediaFolderExport == null)
            ////{
            ////    throw new ApplicationException("Could not find MediaFolder export for Source URL.");
            ////}

            ////MediaFolder folder = mediaFolderExport.GetExportedObject();

            MediaFolder folder = new DefaultMediaFolder
                                     {
                                         Id = Owner.Provider.CreateGuid(),
                                         Title = title,
                                         SourceUrl = sourceUrl,
                                         Dynamic = dynamic,
                                         Thumbnail = thumbnail,
                                         OwnerId = @"-1",
                                         Identifier = null,
                                         LastLoad = DateTime.MinValue
                                     };

            this.folderLookup.Add(folder.Id, folder);
            return folder;
        }

        /// <summary>
        /// Determines if the specified id exists.
        /// </summary>
        /// <param name="id">The media folder id.</param>
        /// <returns><c>True</c> if the <see cref="MediaFolderFactory"/> contains an element
        /// with the specified value; otherwise, <c>false</c>.</returns>
        internal bool Exists(string id)
        {
            return this.folderLookup.ContainsKey(id);
        }

        /// <summary>
        /// Gets the <see cref="MediaFolder"/> by specified id.
        /// </summary>
        /// <param name="id">The folder id.</param>
        /// <returns>The media folder.</returns>
        internal MediaFolder Get(string id)
        {
            return this.folderLookup[id];
        }
    }
}