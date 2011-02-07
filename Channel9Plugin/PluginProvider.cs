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
    ////using System.ComponentModel.Composition.Hosting;
    using MediaMallTechnologies.Plugin;

    #endregion

    /// <summary>
    /// Implements PlayOnProvider interface.
    /// </summary>
    public class PluginProvider : IPlayOnProvider
    {
        #region Declarations

        /// <summary>
        /// Place holder for root folder.
        /// </summary>
        private MediaFolder rootFolder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginProvider"/> class.
        /// </summary>
        public PluginProvider()
        {
            Owner.Provider = this;
            this.Settings = new PluginSettings();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the human-readable name of this plugin.
        /// </summary>
        /// <value></value>
        /// <returns>The name of this plugin.</returns>
        public string Name
        {
            get { return this.Settings.Name; }
        }

        /// <summary>
        /// Gets a unique ID for this provider, no longer than 24 characters.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// The ID for a plugin should preferably be a combination of the unique domain
        /// namespace for the creator of the plugin and the human-readable name of the
        /// plugin, in all lower case and without spaces, and with a maximum of 24
        /// characters. This ID must be prepended to all media and folder IDs, followed by
        /// a hyphen, and postpended with GUID's, as in <i>plugin name-guid</i>.
        /// <para>
        /// _Note that since the suggested plugin IDs are human readable, there is always a
        /// risk of name clashes with other plugins, so be sure to choose an ID that you
        /// are confident is "unique", but no longer than 24 characters.
        /// </para>
        /// </remarks>
        /// <example>
        /// A plugin named "My Plugin" created by "example.com" could have a root level ID
        /// of "example.myplugin", and all sub-items and sub-folders with IDs as
        /// "example.myplugin-<i>GUID</i>".
        /// </example>
        /// <returns>The string ID for this root level plugin, no longer than 24
        /// characters.</returns>
        public string ID
        {
            get { return this.Settings.ID; }
        }

        /// <summary>
        /// Gets an Image icon for this content provider.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// The image must be 78 x 78 pixels. PNG format images are preferred. This image
        /// will be potentially displayed by client media devices as the root folder for
        /// this plugin, if such display is possible for the given device.
        /// </remarks>
        /// <returns>The image for this plugin.</returns>
        public System.Drawing.Image Image
        {
            get { return this.Settings.Image; }
        }

        /// <summary>
        /// Gets or sets the folder factory.
        /// </summary>
        /// <value>The folder factory.</value>
        ////[Import]
        internal MediaFolderFactory FolderFactory { get; set; }

        /// <summary>
        /// Gets or sets the item factory.
        /// </summary>
        /// <value>The item factory.</value>
        ////[Import]
        internal MediaItemFactory ItemFactory { get; set; }

        /// <summary>
        /// Gets or sets the navigation builder.
        /// </summary>
        /// <value>The navigation builder.</value>
        ////[Import]
        internal NavigationBuilder NavigationBuilder { get; set; }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        ////internal CompositionContainer Container { get; set; }

        /// <summary>
        /// Gets the playon host.
        /// </summary>
        /// <value>The playon host.</value>
        internal IPlayOnHost Host { get; private set; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The plugin settings.</value>
        internal PluginSettings Settings { get; private set; }

        #endregion

        /// <summary>
        /// Sets the play on host.
        /// </summary>
        /// <param name="host">The play on host.</param>
        public void SetPlayOnHost(IPlayOnHost host)
        {
            this.Host = host;
            this.Initialize();
        }

        /// <summary>
        /// Returns a <see cref="T:MediaMallTechnologies.Plugin.Payload">Payload</see> for the given ID and request parameters.
        /// </summary>
        /// <param name="id">The ID requested, as provided from a previous GetSharedMedia query.</param>
        /// <param name="includeChildren">Indicator whether children items should be included in the Payload.</param>
        /// <param name="startIndex">The starting index for items that should be included in the Payload.</param>
        /// <param name="requestCount">The total maximum count of items to be returned in the Payload.</param>
        /// <returns>
        /// A Payload object that matches the requested query.
        /// </returns>
        /// <remarks>
        /// This method is the main function for returning hierarchical metadata for folders
        /// and items. The <see cref="T:MediaMallTechnologies.Plugin.Payload">Payload</see> object that is returned
        /// must contain the appropriate metadata result from the query itself, as well as any potential
        /// <see cref="T:MediaMallTechnologies.Plugin.SharedMediaFileInfo"/> and
        /// <see cref="T:MediaMallTechnologies.Plugin.SharedMediaFolderInfo"/> objects.
        /// </remarks>
        public Payload GetSharedMedia(string id, bool includeChildren, int startIndex, int requestCount)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var factory = new PayloadFactory(new MediaMapper());
                if (id == this.ID)
                {
                    return factory.Create(this.rootFolder, startIndex, requestCount);
                }

                if (this.ItemFactory.Exists(id))
                {
                    return factory.Create(this.ItemFactory.Get(id));
                }

                if (this.FolderFactory.Exists(id))
                {
                    var folder = this.FolderFactory.Get(id);
                    folder.Update(false);
                    return factory.Create(folder, startIndex, requestCount);
                }
            }

            return new Payload(@"-1", @"-1", @"[Unknown]", 0, new List<AbstractSharedMediaInfo>(0));
        }

        /// <summary>
        /// Resolves a given <see cref="T:MediaMallTechnologies.Plugin.SharedMediaFileInfo"/>
        /// if further logic is required
        /// to obtain the media URL or to include advertisement URLs, and returns XML information
        /// about these URLs.
        /// </summary>
        /// <param name="fileInfo">The <see cref="SharedMediaFileInfo"/> object that should be resolved.</param>
        /// <returns>
        /// The XML description of URLs for video playback.
        /// </returns>
        /// <remarks>
        /// The main video URL must include a "type" param that is either "wmp" (for Windows Media Player)
        /// or "<c>fp</c>" (for Flash Player) to indicate what type of player is used to display
        /// the given source media. The optional "startTime" param indicates a start time for the source media,
        /// in 100-nanoseconds. <b>_Note that the "startTime" param only works when the "type" is "wmp".</b>
        /// Advertisements must include the timestamp in
        /// 100-nanoseconds for when they should be shown, relative to the main media playback, and also include a "type" parameter.
        /// The XML format is as follows:
        /// <code>
        ///         <![CDATA[
        /// <media>
        /// <url type="wmp|fp" [startTime="100-nanos"]>...</url>
        /// <ad timestamp="100-nanos" type="wmp|fp">...</ad>
        /// <ad timestamp="100-nanos" type="wmp|fp">...</ad>
        /// </media>
        /// ]]>
        ///     </code>
        /// The "type" parameter must be one of either "wmp" or "<c>fp</c>". The 100-nanoseconds
        /// parameters must be non-negative numerical values, and are expressed as <i>seconds</i> * 10^7.
        /// </remarks>
        public string Resolve(SharedMediaFileInfo fileInfo) 
        {
           return string.Format(@"<media><url type=""wmp"">{0}</url></media>", fileInfo.Path);
        }

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The error message output.</param>
        ////[Export("LogMessage")]
        internal void LogMessage(string message)
        {
            this.Host.LogMessage(message);
        }

        /// <summary>
        /// Creates the a GUID.
        /// </summary>
        /// <returns>A new GUID value.</returns>
        internal string CreateGuid()
        {
            return string.Concat(this.ID, @"-", Guid.NewGuid());
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            ////AssemblyCatalog catalog = new AssemblyCatalog(typeof(PluginProvider).Assembly);
            ////this.Container = new CompositionContainer(catalog);
            ////CompositionBatch batch = new CompositionBatch();
            ////batch.AddPart(this);
            ////this.Container.Compose(batch);

            this.FolderFactory = new MediaFolderFactory();
            this.ItemFactory = new MediaItemFactory();
            this.NavigationBuilder = new NavigationBuilder();
            this.rootFolder = NavigationBuilder.Build(this.ID, this.Name);
        }
    }
}