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
    ////using System.ComponentModel.Composition;
    using System.Xml;

    /// <summary>
    /// Class for selecting main RSS nodes.
    /// </summary>
    ////[Export]
    internal class RssSelector
    {
        /// <summary>
        /// Selects all element tags in doc named item.
        /// </summary>
        /// <param name="doc">The xml document.</param>
        /// <returns>A list of all the nodes found.</returns>
        internal virtual XmlNodeList Select(XmlDocument doc)
        {
            return doc.GetElementsByTagName(@"item");
        }
    }
}