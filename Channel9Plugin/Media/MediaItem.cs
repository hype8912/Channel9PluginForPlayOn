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
    using System;

    /// <summary>
    /// Class for creating a media item list.
    /// </summary>
    internal class MediaItem
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The item id.</value>
        internal string Id { get; set; }

        /// <summary>
        /// Gets or sets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        internal string OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        internal string Description { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        internal long Duration { get; set; }

        /// <summary>
        /// Gets or sets the pub date.
        /// </summary>
        /// <value>The pub date.</value>
        internal DateTime PubDate { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The item title.</value>
        internal string Title { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail URL.
        /// </summary>
        /// <value>The thumbnail URL.</value>
        internal string ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the high media URL.
        /// </summary>
        /// <value>The high media URL.</value>
        internal string HighMediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the medium media URL.
        /// </summary>
        /// <value>The medium media URL.</value>
        internal string MediumMediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the low media URL.
        /// </summary>
        /// <value>The low media URL.</value>
        internal string LowMediaUrl { get; set; }
    }
}