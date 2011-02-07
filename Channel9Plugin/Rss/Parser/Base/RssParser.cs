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
    ////using System.ComponentModel.Composition;
    using System.Xml;

    #endregion

    /// <summary>
    /// Abstract RSS parser to be consumed for general RSS parsing.
    /// </summary>
    internal abstract class RssParser
    {
        #region Individual Parsers

        /// <summary>
        /// Gets the description node inner text.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>The item description.</returns>
        internal static string GetDescription(XmlNode item)
        {
            var text = string.Empty;
            if (item == null)
            {
                goto end;
            }

            var descNode = item.SelectSingleNode(@"description");
            if (descNode == null)
            {
                goto end;
            }

            text = descNode.InnerText;
            if (text.Contains('<') || text.Contains('&'))
            {
                text = text.StripHtmlTags();
            }

            end:
            return text;
        }

        /// <summary>
        /// Gets the enclosure node.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>An xml node for enclosure.</returns>
        internal static XmlNode GetEnclosure(XmlNode item)
        {
            return item == null ? null : item.SelectSingleNode(@"enclosure");
        }

        /// <summary>
        /// Gets the enclosure URL value.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>The enclosure URL attribute value.</returns>
        internal static string GetEnclosureUrl(XmlNode item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            var enclosureNode = GetEnclosure(item);
            if (enclosureNode == null || enclosureNode.Attributes == null || enclosureNode.Attributes[@"url"] == null)
            {
                return string.Empty;
            }

            return enclosureNode.Attributes[@"url"].Value;
        }

        /// <summary>
        /// Gets the pub date.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>The pub date as a <see cref="DateTime"/> object or <c>null</c>.</returns>
        internal static DateTime? GetPubDate(XmlNode item)
        {
            if (item == null)
            {
                return null;
            }

            var pubDateNode = item.SelectSingleNode(@"pubDate");
            if (pubDateNode != null)
            {
                DateTime pubDate;
                if (DateTime.TryParse(pubDateNode.InnerText, out pubDate))
                {
                    return pubDate;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the title node inner text.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>The item title.</returns>
        internal static string GetTitle(XmlNode item)
        {
            var text = string.Empty;
            if (item == null)
            {
                goto end;
            }

            var titleNode = item.SelectSingleNode(@"title");
            if (titleNode == null)
            {
                goto end;
            }

            text = titleNode.InnerText;
            if (text.Contains(@"<") || text.Contains(@"&"))
            {
                text = text.StripHtmlTags();
            }

        end:
            return text;
        }

        #endregion

        #region Overall Parser

        /// <summary>
        /// Parses the items node list.
        /// </summary>
        /// <param name="items">The RSS node items.</param>
        /// <returns>A list of <see cref="MediaItem"/>s.</returns>
        internal IEnumerable<MediaItem> Parse(XmlNodeList items)
        {
            return items.Cast<XmlNode>().Select(this.ParseItem).Where(m => m != null).ToList();
        }

        #endregion

        /// <summary>
        /// Parses the item.
        /// </summary>
        /// <param name="node">The item node.</param>
        /// <returns>A new media item.</returns>
        protected abstract MediaItem ParseItem(XmlNode node); 
    }
}