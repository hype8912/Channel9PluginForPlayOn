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
    using System.Xml;

    #endregion

    /// <summary>
    /// Class for only selecting items that were published today.
    /// </summary>
    internal sealed class NewTodayRssSelector : RssSelector
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
        /// <value><c>True</c> if [sync date time]; otherwise, <c>false</c>.</value>
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
                ////this.Today = Convert.ToDateTime("11/9/2009 12:00:00 AM");
                this.Today = DateTime.Now;
            }

            ICollection<XmlNode> removeNodes = new List<XmlNode>();
            foreach (XmlNode node in base.Select(doc))
            {
                var pubDateNode = node.SelectSingleNode("pubDate");
                if (pubDateNode == null)
                {
                    continue;
                }

                DateTime pubDate;
                if (!DateTime.TryParse(pubDateNode.InnerText, out pubDate))
                {
                    continue;
                }

                if (pubDate.Date != this.Today.Date)
                {
                    removeNodes.Add(node);
                }
            }

// ReSharper disable PossibleNullReferenceException
            foreach (var node in removeNodes.Where(n => n.ParentNode != null))
            {
                node.ParentNode.RemoveChild(node);
            }
// ReSharper restore PossibleNullReferenceException

            return base.Select(doc);
        }
    }
}