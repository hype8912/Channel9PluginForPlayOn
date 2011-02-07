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
    using System.Xml;

    #endregion

    /// <summary>
    /// Class for only selecting items that were published today.
    /// </summary>
    internal class NewTodayRssSelector : RssSelector
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewTodayRssSelector"/> class.
        /// </summary>
        internal NewTodayRssSelector()
        {
            this.Today = DateTime.Now;
            this.SyncDateTime = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the today.
        /// </summary>
        /// <value>The today date time value.</value>
        public DateTime Today { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [sync date time].
        /// </summary>
        /// <value><c>True</c> if [sync date time]; otherwise, <c><c>false</c></c>.</value>
        public bool SyncDateTime { get; set; }

        #endregion

        /// <summary>
        /// Selects all element tags in doc named item.
        /// </summary>
        /// <param name="doc">The xml document.</param>
        /// <returns>A list of all the nodes found.</returns>
        internal override XmlNodeList Select(XmlDocument doc)
        {
            if (this.SyncDateTime)
            {
                this.Today = DateTime.Now;
            }

            List<XmlNode> removeNodes = new List<XmlNode>();
            XmlNodeList nodes = base.Select(doc);
            foreach (XmlNode node in nodes)
            {
                XmlNode pubDateNode = node.SelectSingleNode("pubDate");
                if (pubDateNode != null)
                {
                    DateTime pubDate;
                    if (DateTime.TryParse(pubDateNode.InnerText, out pubDate))
                    {
                        if (pubDate.Date != this.Today.Date)
                        {
                            removeNodes.Add(node);
                        }
                    }
                } 
            }

            foreach (XmlNode node in removeNodes)
            {
                node.ParentNode.RemoveChild(node);
            }

            return base.Select(doc);
        }
    }
}