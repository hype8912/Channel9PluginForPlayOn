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
    /// Abstract RSS parser to be consumed for general RSS parsing.
    /// </summary>
    internal abstract class RssParser
    {
        /// <summary>
        /// Strips the html tags using a char array.
        /// </summary>
        /// <param name="source">The string source.</param>
        /// <returns>A string without html tags.</returns>
        internal static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }

                if (let == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }

            string text = new string(array, 0, arrayIndex);
            text = text.Replace("&quot;", "\"").Replace("&nbsp;", " ");
            return text;
        }

        /// <summary>
        /// Gets the description node inner text.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>The item description.</returns>
        internal static string GetDescription(XmlNode item)
        {
            string text = string.Empty;
            if (item == null)
            {
                goto end;
            }

            XmlNode descNode = item.SelectSingleNode("description");
            if (descNode != null)
            {
                text = descNode.InnerText;
            }

            if (text.Contains("<") || text.Contains("&"))
            {
                text = StripTagsCharArray(text);
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
            if (item == null)
            {
                return null;
            }

            return item.SelectSingleNode("enclosure");
        }

        /// <summary>
        /// Gets the enclosure URL value.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>The enclosure url attribute value.</returns>
        internal static string GetEnclosureUrl(XmlNode item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            XmlNode enclosureNode = GetEnclosure(item);
            if (enclosureNode != null)
            {
                XmlAttribute enclosureAttr = enclosureNode.Attributes["url"];
                if (enclosureAttr != null)
                {
                    return enclosureAttr.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the pub date.
        /// </summary>
        /// <param name="item">The item node.</param>
        /// <returns>The pub date as a <see cref="DateTime"/> object or null.</returns>
        internal static DateTime? GetPubDate(XmlNode item)
        {
            if (item == null)
            {
                return null;
            }

            XmlNode pubDateNode = item.SelectSingleNode("pubDate");
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
            string text = string.Empty;
            if (item == null)
            {
                goto end;
            }

            XmlNode titleNode = item.SelectSingleNode("title");
            if (titleNode != null)
            {
                text = titleNode.InnerText;
            }

            if (text.Contains("<") || text.Contains("&"))
            {
                text = StripTagsCharArray(text);
            }

        end:
            return text;
        }

        /// <summary>
        /// Parses the items node list.
        /// </summary>
        /// <param name="items">The RSS node items.</param>
        /// <returns>A list of <see cref="MediaItem"/>s.</returns>
        internal List<MediaItem> Parse(XmlNodeList items)
        {
            List<MediaItem> result = new List<MediaItem>();

            foreach (XmlNode node in items)
            {
                MediaItem mediaItem = this.ParseItem(node);
                if (mediaItem != null)
                {
                    result.Add(mediaItem);
                }
            }

            return result;
        }

        /// <summary>
        /// Parses the item.
        /// </summary>
        /// <param name="node">The item node.</param>
        /// <returns>A new media item.</returns>
        protected abstract MediaItem ParseItem(XmlNode node); 
    }
}