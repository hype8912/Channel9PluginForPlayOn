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
    //using System.ComponentModel.Composition;
    using System.Xml;

    #endregion

    /// <summary>
    /// Basis RSS parsing functions.
    /// </summary>
    //[Export(typeof(RssParser))]
    internal class BasicRssParser : RssParser
    {
        /// <summary>
        /// Parses the item.
        /// </summary>
        /// <param name="node">The item node.</param>
        /// <returns>A new media item.</returns>
        protected override MediaItem ParseItem(XmlNode node)
        {
            MediaItem item = Owner.Provider.ItemFactory.Create();

            item.Description = this.GetDescription(node);

            item.MediumMediaUrl = this.GetEnclosureUrl(node);
            item.LowMediaUrl = item.MediumMediaUrl;
            item.HighMediaUrl = item.MediumMediaUrl;

            item.Duration = 0;

            DateTime? pubDate = this.GetPubDate(node);
            if (pubDate != null)
            {
                item.PubDate = pubDate.Value;
            }

            item.Title = this.GetTitle(node);

            return item;
        }
    }
}