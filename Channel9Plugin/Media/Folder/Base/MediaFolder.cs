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
    using System.Collections.Specialized;
    ////using System.ComponentModel.Composition;
    using Properties;

    #endregion

    /// <summary>
    /// Class building a media folder.
    /// </summary>
    internal abstract class MediaFolder
    {
        #region Declarations

        /// <summary>
        /// Collection for holding metadata.
        /// </summary>
        private readonly NameValueCollection metaData;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFolder"/> class.
        /// </summary>
        protected MediaFolder()
        {
            this.Items = new List<MediaItem>();
            this.Folders = new List<MediaFolder>();
            this.metaData = new NameValueCollection();
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
        /// Gets or sets the content MPAA rating.
        /// </summary>
        /// <value>The content MPAA rating.</value>
        internal string ContentRating
        {
            get { return this.GetMetaData(@"ContentRating"); }
            set { this.SetMetaData(@"ContentRating", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MediaFolder"/> is dynamic.
        /// </summary>
        /// <value><c>True</c> if dynamic; otherwise, <c>false</c>.</value>
        internal bool Dynamic { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The folder id.</value>
        internal string Id { get; set; }

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
        /// Gets or sets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        internal string OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the star rating.
        /// </summary>
        /// <value>The star rating.</value>
        internal string Rating
        {
            get { return this.GetMetaData(@"Rating"); }
            set { this.SetMetaData(@"Rating", value); }
        }

        /// <summary>
        /// Gets or sets the index of the sort.
        /// </summary>
        /// <value>The index of the sort.</value>
        internal string SortIndex
        {
            get { return this.GetMetaData(@"SortIndex"); }
            set { this.SetMetaData(@"SortIndex", value); }
        }

        /// <summary>
        /// Gets or sets the source URL.
        /// </summary>
        /// <value>The source URL.</value>
        internal string SourceUrl { get; set; }

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
        /// Gets the media items.
        /// </summary>
        /// <value>The media items.</value>
        internal ICollection<MediaItem> Items { get; private set; }

        /// <summary>
        /// Gets the media folders.
        /// </summary>
        /// <value>The media folders.</value>
        internal ICollection<MediaFolder> Folders { get; private set; }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new media folder.
        /// </summary>
        /// <param name="title">The folder title.</param>
        /// <returns>A new media folder.</returns>
        internal MediaFolder CreateFolder(string title)
        {
            return this.CreateFolder(title, string.Empty, false);
        }

        /// <summary>
        /// Creates a new media folder.
        /// </summary>
        /// <param name="title">The folder title.</param>
        /// <param name="sourceUrl">The source URL.</param>
        /// <returns>A new media folder.</returns>
        internal MediaFolder CreateFolder(string title, string sourceUrl)
        {
            return this.CreateFolder(title, sourceUrl, true);
        }

        /// <summary>
        /// Creates a new media folder.
        /// </summary>
        /// <param name="title">The folder title.</param>
        /// <param name="sourceUrl">The source URL.</param>
        /// <param name="dynamic">If set to <c>true</c> [dynamic].</param>
        /// <param name="thumbnail">The thumbnail URL.</param>
        /// <returns>A new media folder.</returns>
        internal MediaFolder CreateFolder(string title, string sourceUrl, bool dynamic, string thumbnail = null)
        {
            var folder = Owner.Provider.FolderFactory.Create(title, sourceUrl, dynamic, thumbnail);
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

            if (!force && ((DateTime.UtcNow - this.LastLoad).TotalSeconds <= 300))
            {
                return;
            }

            try
            {
                this.Items.Clear();
                switch (this.Title)
                {
                    case @"New Today":
                        this.RssSelector = new NewTodayRssSelector();
                        break;
                    default:
                        this.RssSelector = new RssSelector();
                        break;
                }

                this.RssLoader = new RssLoader();
                this.RssParser = new Channel9RssParser();

                var nodes = this.RssSelector.Select(RssLoader.Load(this.SourceUrl));
                var items = this.RssParser.Parse(nodes);

                foreach (var item in items)
                {
                    this.AddItem(item);
                }

                this.LastLoad = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                Owner.Provider.LogMessage(Resources.ErrorLabel + ex.Message);
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

            // The code below was left over from the previous developer.
            // Wasn't sure if we still needed it so I just left it in.
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

        /// <summary>
        /// Gets the meta data collection items.
        /// </summary>
        /// <param name="key">The key name.</param>
        /// <returns>The associated key value.</returns>
        private string GetMetaData(string key)
        {
            var values = this.metaData.GetValues(key);
            return values != null ? values[0] : string.Empty;
        }

        /// <summary>
        /// Sets the meta data collection items.
        /// </summary>
        /// <param name="key">The key name.</param>
        /// <param name="value">The value description.</param>
        private void SetMetaData(string key, string value)
        {
            this.metaData.Remove(key);
            this.metaData.Add(key, value);
        }
    }
}