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
    //using System.ComponentModel.Composition;

    /// <summary>
    /// Creates media folder for channel 9 data.
    /// </summary>
    //[Export(typeof(MediaFolder))]
    //[ExportMetadata("Site", "http://channel9.msdn.com")]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    internal class Channel9MediaFolder : MediaFolder
    {
        /// <summary>
        /// Gets or sets the RSS selector.
        /// </summary>
        /// <value>The RSS selector.</value>
        //[Import]
        internal override RssSelector RssSelector { get; set; }

        /// <summary>
        /// Gets or sets the RSS loader.
        /// </summary>
        /// <value>The RSS loader.</value>
        //[Import]
        internal override RssLoader RssLoader { get; set; }

        /// <summary>
        /// Gets or sets the RSS parser.
        /// </summary>
        /// <value>The RSS parser.</value>
        //[Import(typeof(Channel9RssParser))]
        internal override RssParser RssParser { get; set; }
    }
}