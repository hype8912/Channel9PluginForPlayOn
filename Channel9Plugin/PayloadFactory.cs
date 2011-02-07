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
    using System.Linq;
    using MediaMallTechnologies.Plugin;

    #endregion

    /// <summary>
    /// Factory for creating payload.
    /// </summary>
    internal sealed class PayloadFactory
    {
        #region Declarations

        /// <summary>
        /// Place holder for sharing media mapper instance.
        /// </summary>
        private readonly MediaMapper mediaMapper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadFactory"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        internal PayloadFactory(MediaMapper mapper)
        {
            this.mediaMapper = mapper;
        }

        #endregion

        /// <summary>
        /// Creates the specified payload item.
        /// </summary>
        /// <param name="item">The media item.</param>
        /// <returns>A new payload item.</returns>
        internal Payload Create(MediaItem item)
        {
            var items = new List<AbstractSharedMediaInfo> { this.mediaMapper.Map(item) };
            return new Payload(item.Id, item.OwnerId, item.Title, items.Count, items, false);
        }

        /// <summary>
        /// Creates the specified payload folder.
        /// </summary>
        /// <param name="folder">The media folder.</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="requestCount">The request count.</param>
        /// <returns>A new payload folder.</returns>
        internal Payload Create(MediaFolder folder, int startIndex, int requestCount)
        {
            var items = folder.Folders.Select(child => this.mediaMapper.Map(child)).Cast<AbstractSharedMediaInfo>().ToList();
            items.AddRange(folder.Items.Select(child => this.mediaMapper.Map(child)).Cast<AbstractSharedMediaInfo>());
            if (requestCount == 0) 
            {
                requestCount = int.MaxValue;
            }

            var selectedItems = startIndex > items.Count ? new List<AbstractSharedMediaInfo>() : items.GetRange(startIndex, Math.Min(requestCount, items.Count - startIndex));
            return new Payload(folder.Id, folder.OwnerId, folder.Title, items.Count, selectedItems, true);
        }
    }
}