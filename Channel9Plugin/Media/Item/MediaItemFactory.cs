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
    using System.Collections.Generic;
    ////using System.ComponentModel.Composition;

    /// <summary>
    /// Class for creating and managing <see cref="MediaItem"/>s.
    /// </summary>
    ////[Export]
    ////[PartCreationPolicy(CreationPolicy.Shared)]
    internal sealed class MediaItemFactory
    {
        #region Declarations

        /// <summary>
        /// The place holder media item container.
        /// </summary>
        private readonly IDictionary<string, MediaItem> mediaItemLookup = new Dictionary<string, MediaItem>();

        #endregion

        /// <summary>
        /// Creates a new <see cref="MediaItem"/> and adds the new id to the <see cref="MediaItemFactory"/>.
        /// </summary>
        /// <returns>The newly created <see cref="MediaItem"/>.</returns>
        internal MediaItem Create()
        {
            var item = new MediaItem { Id = Owner.Provider.CreateGuid() };
            this.mediaItemLookup.Add(item.Id, item);
            return item;
        }

        /// <summary>
        /// Determines whether the <see cref="MediaItemFactory"/> contains a specific
        /// value.
        /// </summary>
        /// <param name="id">The value to locate in the <see cref="MediaItemFactory"/>. The
        /// value can be <c>null</c> for reference types.
        /// </param>
        /// <returns><c>True</c> if the <see cref="MediaItemFactory"/> contains an element
        /// with the specified value; otherwise, <c>false</c>.</returns>
        internal bool Exists(string id)
        {
            return this.mediaItemLookup.ContainsKey(id);
        }

        /// <summary>
        /// Gets the <see cref="MediaItem"/> for the specified id.
        /// </summary>
        /// <param name="id">The media items unique id.</param>
        /// <returns>The <see cref="MediaItem"/> for the specified id.</returns>
        internal MediaItem Get(string id)
        {
            MediaItem item;
            return this.mediaItemLookup.TryGetValue(id, out item) ? item : null;
        }
    }
}