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
    using System.Xml;

    #endregion

    /// <summary>
    /// Class building a media folder.
    /// </summary>
    internal abstract class MediaFolder
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFolder"/> class.
        /// </summary>
        protected MediaFolder()
        {
            this.Items = new List<MediaItem>();
            this.Folders = new List<MediaFolder>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the RSS selector.
        /// </summary>
        /// <value>The RSS selector.</value>
        internal abstract RssSelector RssSelector { get; set; }

        /// <summary>
        /// Gets or sets the RSS loader.
        /// </summary>
        /// <value>The RSS loader.</value>
        internal abstract RssLoader RssLoader { get; set; }

        /// <summary>
        /// Gets or sets the RSS parser.
        /// </summary>
        /// <value>The RSS parser.</value>
        internal abstract RssParser RssParser { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The folder title.</value>
        internal string Title { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        /// <value>The thumbnail.</value>
        internal string Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The folder id.</value>
        internal string Id { get; set; }

        /// <summary>
        /// Gets or sets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        internal string OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        internal string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the last load.
        /// </summary>
        /// <value>The last load.</value>
        internal DateTime LastLoad { get; set; }

        /// <summary>
        /// Gets or sets the source URL.
        /// </summary>
        /// <value>The source URL.</value>
        internal string SourceUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MediaFolder"/> is dynamic.
        /// </summary>
        /// <value><c>True</c> if dynamic; otherwise, <c>false</c>.</value>
        internal bool Dynamic { get; set; }

        /// <summary>
        /// Gets the media items.
        /// </summary>
        /// <value>The media items.</value>
        internal List<MediaItem> Items { get; private set; }

        /// <summary>
        /// Gets the media folders.
        /// </summary>
        /// <value>The media folders.</value>
        internal List<MediaFolder> Folders { get; private set; }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new media folder.
        /// </summary>
        /// <param name="title">The folder title.</param>
        /// <returns>A new media folder.</returns>
        internal MediaFolder CreateFolder(string title)
        {
            return this.CreateFolder(title, string.Empty, false, null);
        }

        /// <summary>
        /// Creates a new media folder.
        /// </summary>
        /// <param name="title">The folder title.</param>
        /// <param name="sourceUrl">The source URL.</param>
        /// <returns>A new media folder.</returns>
        internal MediaFolder CreateFolder(string title, string sourceUrl)
        {
            return this.CreateFolder(title, sourceUrl, true, null);
        }

        /// <summary>
        /// Creates a new media folder.
        /// </summary>
        /// <param name="title">The folder title.</param>
        /// <param name="sourceUrl">The source URL.</param>
        /// <param name="dynamic">If set to <c>true</c> [dynamic].</param>
        /// <param name="thumbnail">The thumbnail url.</param>
        /// <returns>A new media folder.</returns>
        internal MediaFolder CreateFolder(string title, string sourceUrl, bool dynamic, string thumbnail)
        {
            MediaFolder folder = Owner.Provider.FolderFactory.Create(title, sourceUrl, dynamic, thumbnail);
            this.AddFolder(folder);
            return folder;
        }

        #endregion

        /// <summary>
        /// Updates the specified media folder.
        /// </summary>
        /// <param name="force">If set to <c>true</c> [force] update.</param>
        internal void Update(bool force)
        {
            if (!this.Dynamic)
            {
                return;
            }

            if (force || ((DateTime.UtcNow - this.LastLoad).TotalSeconds > 300))
            {
                try
                {
                    this.Items.Clear();

                    switch (this.Title)
                    {
                        case "New Today":
                            this.RssSelector = new NewTodayRssSelector();
                            break;
                        default:
                            this.RssSelector = new RssSelector();
                            break;
                    }

                    this.RssLoader = new RssLoader();

                    ////if (sourceUrl.StartsWith("http://channel9.msdn.com/", true, CultureInfo.InvariantCulture))
                    ////{
                        this.RssParser = new Channel9RssParser();
                    ////}
                    ////else
                    ////{
                    ////    this.RssParser = new BasicRssParser();
                    ////}

                    XmlDocument doc = RssLoader.Load(this.SourceUrl);
                    XmlNodeList nodes = this.RssSelector.Select(doc);
                    List<MediaItem> items = this.RssParser.Parse(nodes);

                    foreach (MediaItem item in items)
                    {
                        this.AddItem(item);
                    }

                    this.LastLoad = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    Owner.Provider.LogMessage("Error: " + ex.Message);
                }
            }
        }

        #region Add

        /// <summary>
        /// Adds the media folder to the folders list.
        /// </summary>
        /// <param name="folder">The media folder.</param>
        private void AddFolder(MediaFolder folder)
        {
            this.Folders.Add(folder);
            folder.OwnerId = this.Id;
            ////    if (folder.Identifier != null)
            ////        this.lookup[folder.Identifier] = folder.Id;
        }

        /// <summary>
        /// Adds the media item to the items list.
        /// </summary>
        /// <param name="item">The media item.</param>
        private void AddItem(MediaItem item)
        {
            item.OwnerId = this.Id;
            this.Items.Add(item);
        }

        #endregion
    }
}