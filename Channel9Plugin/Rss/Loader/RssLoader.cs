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

    ////using System.ComponentModel.Composition;
    using System.IO;
    using System.Net;
    using System.Xml;

    #endregion

    /// <summary>
    /// Class for requesting RSS feed and loading it as an Xml document.
    /// </summary>
    ////[Export]
    internal sealed class RssLoader
    {
        /// <summary>
        /// Gets and loads the specified URL.
        /// </summary>
        /// <param name="url">The web URL.</param>
        /// <returns>An xml document object retrieved.</returns>
        internal static XmlDocument Load(string url)
        {
            string xml;
            var req = (HttpWebRequest)WebRequest.Create(url);
            using (var reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                xml = reader.ReadToEnd();
            }

            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }
    }
}