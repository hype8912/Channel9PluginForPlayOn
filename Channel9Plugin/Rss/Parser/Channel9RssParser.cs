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
    using System.Linq;
    ////using System.ComponentModel.Composition;
    using System.Xml;

    #endregion

    /// <summary>
    /// Specific RSS parser for parsing Channel 9 RSS feeds.
    /// </summary>
    ////[Export(typeof(Channel9RssParser))]
    internal sealed class Channel9RssParser : RssParser
    {
        #region Declarations

        /// <summary>
        /// Place holder for xml namespace manager.
        /// </summary>
        private XmlNamespaceManager namespaceManager;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the item count.
        /// </summary>
        /// <value>The item count.</value>
        internal int Count { get; set; }

        #endregion

        /// <summary>
        /// Method for parsing RSS item node..
        /// </summary>
        /// <param name="node">The item node.</param>
        /// <returns>A pre-filled media item.</returns>
        protected override MediaItem ParseItem(XmlNode node)
        {
            var item = Owner.Provider.ItemFactory.Create();
            this.Count += 1;

            try
            {
                // Set namespace manager.
                this.namespaceManager = new XmlNamespaceManager(node.OwnerDocument.NameTable);
                this.namespaceManager.AddNamespace(@"media", @"http://search.yahoo.com/mrss/");
                this.namespaceManager.AddNamespace(@"evnet", @"http://www.mscommunities.com/rssmodule/");

#if DEBUG
                Owner.Provider.LogMessage(@"Starting item.");
#endif

                // Get item description.
                item.Description = this.GetEvnetPreviewText(node);
                if (string.IsNullOrEmpty(item.Description))
                {
                    item.Description = GetDescription(node);
                }

                // Get item media content nodes.
                var contentNodes = this.GetMediaContent(node, "@type='video/x-ms-wmv'");
                if (contentNodes != null)
                {
                    foreach (var url in from XmlNode contentNode in contentNodes
                                           select GetMediaContentUrl(contentNode))
                    {
                        if (url.Contains(@"2MB") || url.Contains(@"/wmv-hq/"))
                        {
                            item.HighMediaUrl = url;
                        }
                        else if (url.Contains(@"Zune"))
                        {
                            item.LowMediaUrl = url;
                        }
                        else if (url.EndsWith(@".wmv", StringComparison.OrdinalIgnoreCase))
                        {
                            item.MediumMediaUrl = url;
                        }
                    }
                }

                // Assign media values.
                if (string.IsNullOrEmpty(item.MediumMediaUrl))
                {
                    // Get item enclosure.
                    item.MediumMediaUrl = GetEnclosureUrl(node);
                }

                if (string.IsNullOrEmpty(item.MediumMediaUrl))
                {
#if DEBUG
                    Owner.Provider.LogMessage(@"Ending item - MediumMediaUrl not found.");
#endif
                    return null;
                }

                if (string.IsNullOrEmpty(item.LowMediaUrl))
                {
                    item.LowMediaUrl = item.MediumMediaUrl;
                }

                if (string.IsNullOrEmpty(item.HighMediaUrl))
                {
                    item.HighMediaUrl = item.MediumMediaUrl;
                }

                if (item.MediumMediaUrl.Contains(@".mp3") || item.MediumMediaUrl.Contains(@".wma"))
                {
#if DEBUG
                    Owner.Provider.LogMessage(@"Ending item - Audio file found.");
#endif
                    return null;
                }

                // Get item duration.
                item.Duration = this.GetMediaContentDuration(node);

                // Get the publication date if one exists.
                var pubDate = GetPubDate(node);
                if (pubDate != null)
                {
                    item.PubDate = pubDate.Value;
                }

                // Get item title.
                item.Title = GetTitle(node);

                // Get item thumbnail url.
                item.ThumbnailUrl = this.GetMediaThumbnailUrl(node);

#if DEBUG
                Owner.Provider.LogMessage(@"Ending item. " + item.Title);
#endif
            }
            catch (Exception ex)
            {
                Owner.Provider.LogMessage(ex.Message);
                return null;
            }

            return item;
        }

        /// <summary>
        /// Gets the media content URL item.
        /// </summary>
        /// <param name="content">The media:content node.</param>
        /// <returns>The string media url.</returns>
        private static string GetMediaContentUrl(XmlNode content)
        {
            if (content == null || content.Attributes == null)
            {
                return string.Empty;
            }

            return content.Attributes[@"url"] != null ? content.Attributes[@"url"].Value : string.Empty;
        }

        /// <summary>
        /// Gets the evnet preview text inner text.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>The preview text.</returns>
        private string GetEvnetPreviewText(XmlNode item)
        {
            var text = string.Empty;
            if (item == null)
            {
                goto end;
            }

            var descNode = item.SelectSingleNode(@".//evnet:previewtext", this.namespaceManager);
            if (descNode == null)
            {
                goto end;
            }

            text = descNode.InnerText;
            if (text.Contains('<'))
            {
                text = text.StripHtmlTags();
            }

        end:
            return text;
        }

        /// <summary>
        /// Gets the content of the media node.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>A single xml node for the item's media content.</returns>
        private XmlNode GetMediaContent(XmlNode item)
        {
            return item == null ? null : item.SelectSingleNode(@".//media:content", this.namespaceManager);
        }

        /// <summary>
        /// Gets the content of the media.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <param name="filters">The media content filters.</param>
        /// <returns>A list of item media content nodes.</returns>
        private XmlNodeList GetMediaContent(XmlNode item, string filters)
        {
            return item == null ? null : item.SelectNodes(@".//media:content[" + filters + @"]", this.namespaceManager);
        }

        /// <summary>
        /// Gets the duration of the media content.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>A long value for the length of the media.</returns>
        private long GetMediaContentDuration(XmlNode item)
        {
            var durationString = @"0";
            if (item == null)
            {
                goto end;
            }

            var durationNode = this.GetMediaContent(item);
            if (durationNode != null && durationNode.Attributes != null && durationNode.Attributes[@"duration"] != null)
            {
                durationString = durationNode.Attributes[@"duration"].Value;
            }

            long value;
            if (long.TryParse(durationString, out value))
            {
                return (value + 1) * 1000;
            }

        end:
            return 1000;
        }

        /// <summary>
        /// Gets the media:thumbnail node.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>An xml node for enclosure.</returns>
        private XmlNode GetMediaThumbnail(XmlNode item)
        {
            return item == null ? null : item.SelectSingleNode(@"media:thumbnail[@height=240]", this.namespaceManager);
        }

        /// <summary>
        /// Gets the media thumbnail URL attribute.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>The enclosure url attribute value.</returns>
        private string GetMediaThumbnailUrl(XmlNode item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            var thumbnailNode = this.GetMediaThumbnail(item);
            if (thumbnailNode == null || thumbnailNode.Attributes == null || thumbnailNode.Attributes[@"url"] == null)
            {
                return string.Empty;
            }

            return thumbnailNode.Attributes[@"url"].Value;
        }
    }
}