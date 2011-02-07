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

    /// <summary>
    /// Responsible for building main virtual folders.
    /// </summary>
    ////[Export]
    internal class NavigationBuilder
    {
        #region Declarations

        /// <summary>
        /// URL path to the default Channel 9 show image.
        /// </summary>
        private const string NinerImage = @"http://mschnlnine.vo.llnwd.net/d1/Dev/App_Themes/C9/images/feedimage.png";

        #endregion

        /// <summary>
        /// Builds the specified root id.
        /// </summary>
        /// <param name="rootId">The root id.</param>
        /// <param name="rootName">Name of the root.</param>
        /// <returns>A new media folder.</returns>
        internal static MediaFolder Build(string rootId, string rootName)
        {
            var rootFolder = Owner.Provider.FolderFactory.Create(rootName, string.Empty, false, null);
            rootFolder.OwnerId = rootId;

            rootFolder.CreateFolder(@"Main Feed", @"http://channel9.msdn.com/Feeds/RSS/", true, NinerImage);
            var folder = rootFolder.CreateFolder(@"New Today", @"http://channel9.msdn.com/Feeds/RSS/", true, NinerImage);
            folder.RssSelector = new NewTodayRssSelector();

            #region Shows

            folder = rootFolder.CreateFolder(@"Shows");
            folder.CreateFolder(@"10-4", @"http://channel9.msdn.com/shows/10-4/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/previewImages/220/518971_220x165.jpg");
            folder.CreateFolder(@"ARCast with Ron Jacobs", @"http://channel9.msdn.com/shows/ARCast+with+Ron+Jacobs/feed/wmvhigh/", false, @"http://channel9.msdn.com/Link/26c931de-d4c6-4935-86e3-48fadee77dea/");
            folder.CreateFolder(@"ARCast.TV", @"http://channel9.msdn.com/shows/ARCast.TV/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/ARCTV_220x165.png");
            folder.CreateFolder(@"Behind The Code", @"http://channel9.msdn.com/shows/Behind+The+Code/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/BTC_220x165.png");
            folder.CreateFolder(@"But Why?", @"http://channel9.msdn.com/shows/ButWhy/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/previewImages/220/495089_220x165.jpg");
            folder.CreateFolder(@"Cloud Cover", @"http://channel9.msdn.com/shows/Cloud+Cover/feed/wmvhigh/");
            folder.CreateFolder(@"Code To Live", @"http://channel9.msdn.com/shows/Code+To+Live/feed/wmvhigh/");
            folder.CreateFolder(@"Coding4Fun TV", @"http://channel9.msdn.com/shows/Coding4FunTV/feed/wmvhigh/");
            folder.CreateFolder(@"Communicating", @"http://channel9.msdn.com/shows/Communicating/feed/wmvhigh/");
            folder.CreateFolder(@"Developer Meet Server", @"http://channel9.msdn.com/shows/Developer+Meet+Server/feed/wmvhigh/");
            
            folder.CreateFolder(@"Developers for Developers", @"http://channel9.msdn.com/shows/Devs4Devs/feed/wmvhigh/", false, @"http://channel9.msdn.com/Link/6a8e89f3-3c9a-4305-a9f2-dd772f0b1c89/?default=large");
            folder.CreateFolder(@"endpoint.tv", @"http://channel9.msdn.com/shows/Endpoint/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/EPTV_220x165.png");
            folder.CreateFolder(@"geekSpeak", @"http://channel9.msdn.com/shows/geekSpeak/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/GS_220x165.png");
            folder.CreateFolder(@"Going Deep", @"http://channel9.msdn.com/shows/Going+Deep/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/GD_220x165.png");
            folder.CreateFolder(@"Hot Apps", @"http://channel9.msdn.com/Shows/Hot-Apps/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/576c8706-167e-4b35-83f8-5526ea3b88c0.jpg");
            folder.CreateFolder(@"IIS Show", @"http://channel9.msdn.com/shows/IIS+Show/feed/wmvhigh/");
            folder.CreateFolder(@"In the Office", @"http://channel9.msdn.com/shows/In+the+Office/feed/wmvhigh/");
            folder.CreateFolder(@"Inside Channel 9", @"http://channel9.msdn.com/shows/InsideChannel9/feed/wmvhigh/");
            folder.CreateFolder(@"Inside Out", @"http://channel9.msdn.com/shows/Inside+Out/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/previewImages/220/494124_220x165.jpg");
            folder.CreateFolder(@"Inside Windows Phone", @"http://channel9.msdn.com/shows/Inside+Windows+Phone/feed/wmvhigh/", false, @"http://channel9.msdn.com/styles/images/defaults/c9-220x165b.png");
            
            folder.CreateFolder(@"InsideXbox", @"http://channel9.msdn.com/shows/InsideXbox/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/IX_220x165.png");
            folder.CreateFolder(@"InterFace", @"http://channel9.msdn.com/shows/InterFace/feed/wmvhigh/", false, @"http://channel9.msdn.com/styles/images/defaults/c9-220x165b.png");
            folder.CreateFolder(@"IT Heroes", @"http://channel9.msdn.com/shows/IT+Heroes/feed/wmvhigh/");
            folder.CreateFolder(@"Microsoft Conversations with J", @"http://channel9.msdn.com/shows/Microsoft+Conversations+with+J/feed/wmvhigh/");
            folder.CreateFolder(@"MIX07 Buzzcast", @"http://channel9.msdn.com/Shows/MIX07+Buzzcast/feed/wmvhigh/");
            folder.CreateFolder(@"Pimp My App", @"http://channel9.msdn.com/shows/Pimp+My+App/feed/wmvhigh/");
            folder.CreateFolder(@"Ping!", @"http://channel9.msdn.com/shows/PingShow/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/Ping_220x165.jpg");
            folder.CreateFolder(@"SharePoint Sideshow", @"http://channel9.msdn.com/shows/SharePointSideshow/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/6927ac6b-9aef-4601-a7d4-b33c208b4fd8.png");
            folder.CreateFolder(@"Silverlight TV", @"http://channel9.msdn.com/shows/SilverlightTV/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/SLTV_220x165.jpg");
            folder.CreateFolder(@"Stefan Is...", @"http://channel9.msdn.com/shows/StefanIs/feed/wmvhigh/");
            
            folder.CreateFolder(@"Striking Pixels", @"http://channel9.msdn.com/shows/Striking+Pixels/feed/wmvhigh/");
            folder.CreateFolder(@"TCSWeekly", @"http://channel9.msdn.com/shows/TCSWeekly/feed/wmvhigh/");
            folder.CreateFolder(@"TechFairSV", @"http://channel9.msdn.com/shows/TechFairSV/feed/wmvhigh/");
            ////folder.CreateFolder(@"TechNet Radio", @"http://channel9.msdn.com/shows/TechNet+Radio/feed/wmvhigh/");
            folder.CreateFolder(@"Technology Roundtable", @"http://channel9.msdn.com/shows/Technology+Roundtable/feed/wmvhigh/");
            folder.CreateFolder(@"The Access Show", @"http://channel9.msdn.com/shows/Access/feed/wmvhigh/");
            folder.CreateFolder(@"The Code Room", @"http://channel9.msdn.com/shows/The+Code+Room/feed/wmvhigh/");
            folder.CreateFolder(@"The Continuum Show", @"http://channel9.msdn.com/shows/Continuum/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/CNT_220x165.png");
            folder.CreateFolder(@"The DFO Show", @"http://channel9.msdn.com/shows/The+DFO+Show/feed/wmvhigh/");
            folder.CreateFolder(@"The EndPoint", @"http://channel9.msdn.com/shows/The+EndPoint/feed/wmvhigh/");
            
            folder.CreateFolder(@"The HPC Show", @"http://channel9.msdn.com/shows/The+HPC+Show/feed/wmvhigh/");
            folder.CreateFolder(@"The Id Element", @"http://channel9.msdn.com/shows/Identity/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/ID_220x165.png");
            folder.CreateFolder(@"The Knowledge Chamber", @"http://channel9.msdn.com/shows/The+Knowledge+Chamber/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/KC_220x165.png");
            folder.CreateFolder(@"The MicroISV Show", @"http://channel9.msdn.com/shows/The+MicroISV+Show/feed/wmvhigh/");
            folder.CreateFolder(@"The Office Blog", @"http://channel9.msdn.com/shows/TheOfficeBlog/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/OC_220x165.png");
            folder.CreateFolder(@"The Voice of Support", @"http://channel9.msdn.com/shows/The+Voice+of+Support/feed/wmvhigh/");
            folder.CreateFolder(@"This Week On Channel 9", @"http://channel9.msdn.com/shows/This+Week+On+Channel+9/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/images/TWOC9_220x165.jpg");
            folder.CreateFolder(@"Toolshed", @"http://channel9.msdn.com/shows/toolshed/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/TS_220x165.png");
            folder.CreateFolder(@"Visual Studio 2010 Launch", @"http://channel9.msdn.com/shows/VS2010Launch/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/previewImages/220/542734_220x165.jpg");
            folder.CreateFolder(@"Web Camps TV", @"http://channel9.msdn.com/shows/Web+Camps+TV/feed/wmvhigh/", false, @"https://rev9.blob.core.windows.net/thumbnail/070e77f0-0fcd-4be0-baa8-5ab695d6c6d5.png");
            
            folder.CreateFolder(@"Windows Vista Show-and-Tell", @"http://channel9.msdn.com/shows/Windows+Vista+Show-and-Tell/feed/wmvhigh/");
            folder.CreateFolder(@"WM_IN", @"http://channel9.msdn.com/shows/WM_IN/feed/wmvhigh/", false, @"http://channel9.msdn.com/Link/08924dd5-9210-446c-aa48-b60d49f8e187/");

            #endregion

            #region Series

            folder = rootFolder.CreateFolder(@"Series");
            folder.CreateFolder(@"ALM Summit 2010", @"http://channel9.msdn.com/Series/ALM-Summit-2010/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/65babcd3-c3a9-4344-83f3-2efe13993847.png");
            folder.CreateFolder(@"Dynamics CRM 2011 Developer Training", @"http://channel9.msdn.com/Series/DynamicsCRM2011/feed/wmvhigh/");
            folder.CreateFolder(@"Live from the Edge of Space", @"http://channel9.msdn.com/series/Live-from-the-Edge-of-Space/feed/wmvhigh/", false, @"https://rev9.blob.core.windows.net/thumbnail/e59939b9-9849-4c1c-b354-23463e00dfa5.jpg");
            folder.CreateFolder(@"Microsoft Azure in de Echte Wereld", @"http://channel9.msdn.com/Series/Microsoft-Azure-in-de-Echte-Wereld/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/a6787efe-bb3a-4cdf-b79a-fc717c8cd44f.png");
            folder.CreateFolder(@"Microsoft Campus Tours", @"http://channel9.msdn.com/Series/CampusTours/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/0d7de829-caeb-4ae5-98bf-74c62dadfcb8.jpg");
            folder.CreateFolder(@"SharePoint 2010 Firestarter", @"http://channel9.msdn.com/Series/SharePoint-2010-Firestarter/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/22a9354d-eec3-400f-a175-7b0ed45eb4dc.png");
            folder.CreateFolder(@"Silverlight Firestarter", @"http://channel9.msdn.com/Series/Silverlight-Firestarter/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/b8d5b861-dc17-4039-a996-338eb4a63a60.png");
            folder.CreateFolder(@"Silverlight Firestarter Labs", @"http://channel9.msdn.com/Series/Silverlight-Firestarter-Labs/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/99183348-0fec-4661-8b46-778677202ce5.png");
            folder.CreateFolder(@"The Full Stack", @"http://channel9.msdn.com/Series/The-Full-Stack/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/c3c770b0-5a27-44bf-9460-02943beb9c15.jpg");
            
            folder.CreateFolder(@"The History of Microsoft", @"http://channel9.msdn.com/series/History/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/HOM_220x165.png");
            folder.CreateFolder(@"The Visual Studio Documentary", @"http://channel9.msdn.com/series/VisualStudioDocumentary/feed/wmvhigh/", false, @"http://ecn.channel9.msdn.com/o9/content/areas/VSD_220x165.png");
            folder.CreateFolder(@"Unified Communications ""14"" Labs", @"http://channel9.msdn.com/Series/Unified-Communications-14-Labs/feed/wmvhigh/");
            folder.CreateFolder(@"Web Camp Belgium 2011", @"http://channel9.msdn.com/Series/Web-Camp-Belgium-2011/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/02769b2d-3f52-4bf3-90e0-0f8198868a52.png");
            folder.CreateFolder(@"Windows Phone 7 Development for Absolute Beginners", @"http://channel9.msdn.com/Series/Windows-Phone-7-Development-for-Absolute-Beginners/feed/wmvhigh/", false, @"http://files.channel9.msdn.com/thumbnail/a2a073c8-8184-498a-a26f-b187130d191d.png");

            #endregion

            #region Code

            folder = rootFolder.CreateFolder(@"Code");
            folder.CreateFolder(@".Net Framework", @"http://channel9.msdn.com/tags/.NET+Framework/RSS/");
            folder.CreateFolder(@"Architecture", @"http://channel9.msdn.com/Tags/architecture/RSS/");
            folder.CreateFolder(@"Asp.Net", @"http://channel9.msdn.com/tags/ASP.NET/RSS/");
            folder.CreateFolder(@"C++", @"http://channel9.msdn.com/tags/C++/RSS/");
            folder.CreateFolder(@"C#", @"http://channel9.msdn.com/tags/CSharp/RSS/");
            folder.CreateFolder(@"F#", @"http://channel9.msdn.com/tags/FSharp/RSS/");
            folder.CreateFolder(@"JavaScript", @"http://channel9.msdn.com/tags/Javascript/RSS/");
            folder.CreateFolder(@"jQuery", @"http://channel9.msdn.com/tags/jQuery/RSS/");
            folder.CreateFolder(@"Linq", @"http://channel9.msdn.com/tags/LINQ/RSS/");
            folder.CreateFolder(@"PHP", @"http://channel9.msdn.com/tags/PHP/RSS/");
            folder.CreateFolder(@"Programming", @"http://channel9.msdn.com/Tags/programming/RSS/");
            folder.CreateFolder(@"Rest", @"http://channel9.msdn.com/tags/REST/RSS/");
            folder.CreateFolder(@"Reactive Extensions for .NET", @"http://channel9.msdn.com/tags/Reactive+Extensions/RSS/");
            folder.CreateFolder(@"Ruby", @"http://channel9.msdn.com/tags/Ruby/RSS/");
            folder.CreateFolder(@"Silverlight", @"http://channel9.msdn.com/tags/Silverlight/RSS/");
            folder.CreateFolder(@"Windows Communication Framework", @"http://channel9.msdn.com/tags/WCF/RSS/");
            folder.CreateFolder(@"Windows Presentation Foundation", @"http://channel9.msdn.com/tags/WPF/RSS/");
            folder.CreateFolder(@"Windows Workflow", @"http://channel9.msdn.com/tags/Windows+Workflow/RSS/");
            folder.CreateFolder(@"Xaml", @"http://channel9.msdn.com/tags/XAML/RSS/");
            folder.CreateFolder(@"Xml", @"http://channel9.msdn.com/tags/XML/RSS/");

            #endregion

            #region Media

            folder = rootFolder.CreateFolder(@"Media");
            folder.CreateFolder(@"Channel 9 Videos", @"http://channel9.msdn.com/Media/Videos/feed/wmvhigh/");
            folder.CreateFolder(@"Channel 9 Podcasts", @"http://channel9.msdn.com/Media/Podcasts/feed/wma/");
            folder.CreateFolder(@"Channel 9 Screencasts", @"http://channel9.msdn.com/Media/Screencasts/feed/wmv/");

            #endregion

            #region Technologies

            folder = rootFolder.CreateFolder(@"Technologies");
            folder.CreateFolder(@"Azure", @"http://channel9.msdn.com/Tags/azure/RSS/");
            folder.CreateFolder(@"Expression", @"http://channel9.msdn.com/tags/Expression/RSS/");
            folder.CreateFolder(@"Office", @"http://channel9.msdn.com/tags/Office/RSS/");
            folder.CreateFolder(@"Sharepoint", @"http://channel9.msdn.com/tags/Sharepoint/RSS/");
            folder.CreateFolder(@"SQL Server 2008", @"http://channel9.msdn.com/tags/SQL+Server+2008/RSS/");
            folder.CreateFolder(@"Windows 7", @"http://channel9.msdn.com/tags/Windows+7/RSS/");
            folder.CreateFolder(@"Windows Azure", @"http://channel9.msdn.com/tags/Windows+Azure/RSS/");
            folder.CreateFolder(@"Windows Mobile", @"http://channel9.msdn.com/tags/Windows+Mobile/RSS/");
            folder.CreateFolder(@"Windows Phone", @"http://channel9.msdn.com/Tags/windows+phone/RSS/");
            folder.CreateFolder(@"Windows Server", @"http://channel9.msdn.com/tags/Windows+Server/RSS/");
            folder.CreateFolder(@"Windows Vista", @"http://channel9.msdn.com/tags/Windows+Vista/RSS/");
            folder.CreateFolder(@"Visual Studio", @"http://channel9.msdn.com/tags/Visual+Studio/RSS/");
            folder.CreateFolder(@"Visual Studio 2010", @"http://channel9.msdn.com/Tags/visual+studio+2010/RSS/");

            #endregion

            #region People

            folder = rootFolder.CreateFolder(@"People");
            folder.CreateFolder(@"Adam Kinney", @"http://channel9.msdn.com/Niners/AdamKinney/RSS/", false, @"http://files.channel9.msdn.com/avatar/4cb3122b-53f6-4f91-b2d0-c4949bb45fe4.jpg");
            folder.CreateFolder(@"Bill Gates", @"http://channel9.msdn.com/tags/Bill+Gates/RSS/");
            folder.CreateFolder(@"Scott Guthrie", @"http://channel9.msdn.com/tags/Scott+Guthrie/RSS/");
            folder.CreateFolder(@"Brian Beckman", @"http://channel9.msdn.com/tags/Brian+Beckman/RSS/");
            folder.CreateFolder(@"Erik Meijer", @"http://channel9.msdn.com/tags/Erik+Meijer/RSS/");
            folder.CreateFolder(@"Sanjay Jain", @"http://channel9.msdn.com/tags/Sanjay+Jain/RSS/");
            folder.CreateFolder(@"Paul Allen", @"http://channel9.msdn.com/tags/Paul+Allen/RSS/");
            folder.CreateFolder(@"Larry Larsen", @"http://channel9.msdn.com/posts/LarryLarsen/feed/wmvhigh/");
            folder.CreateFolder(@"Talking Architects", @"http://channel9.msdn.com/posts/mattdeacon/feed/wmvhigh/");
            folder.CreateFolder(@"MS Personalities", @"http://channel9.msdn.com/Tags/ms+personalities/RSS/");

            #endregion

            ////folder = rootFolder.CreateFolder(@"Channel 10");
            ////folder.CreateFolder(@"Main Feed", @"http://on10.net/Feeds/RSS/");

            #region MIX

            folder = rootFolder.CreateFolder(@"MIX Online");
            folder.CreateFolder(@"MIX 10", @"http://live.visitmix.com/Sessions/RSS");
            folder.CreateFolder(@"MIX 09", @"http://videos.visitmix.com/MIX09/RSS");
            folder.CreateFolder(@"MIX 08", @"http://videos.visitmix.com/MIX08/RSS");
            folder.CreateFolder(@"MIX 07", @"http://videos.visitmix.com/MIX07/RSS");
            folder.CreateFolder(@"MIX 06", @"http://videos.visitmix.com/MIX06/RSS");

            #endregion

            #region PDC

            folder = rootFolder.CreateFolder(@"Microsoft PDC");
            folder.CreateFolder(@"PDC 09", @"http://microsoftpdc.com/Sessions/RSS");
            folder.CreateFolder(@"PDC 08", @"http://channel9.msdn.com/tags/Breakout+Session/RSS/");
            folder.CreateFolder(@"PDC 05", @"http://channel9.msdn.com/tags/PDC05/RSS/");

            #endregion

            #region TechNet

            folder = rootFolder.CreateFolder(@"TechNet");
            folder.CreateFolder(@"Edge Main Feed", @"http://edge.technet.com/Feeds/RSS/");
            folder.CreateFolder(@"Edge How Do I? Videos", @"http://www.microsoft.com/feeds/technet/en-us/how-to-videos/TechNet_How-to_Videos.xml");
            folder.CreateFolder(@"Port 25 Open Source", @"http://port25.technet.com/rss.aspx?Tags=Media/Video&AndTags=1");

            #endregion

            folder = rootFolder.CreateFolder("WindowsClient.Net");
            ////folder.CreateFolder("Learn");
            folder.CreateFolder("WPF", "http://windowsclient.net/blogs/MainFeed.aspx?GroupID=11&Type=AllBlogs");

            ////folder = rootFolder.CreateFolder("Microsoft Partners Network");
            ////folder.CreateFolder("Main Feed", "http://www.microsoftpartnercasts.com/_layouts/feed.aspx?xsl=3&web=%2F&page=21ea9d48-16cf-41b2-8c70-97f7cc565698&wp=a8bc16e5-72b3-42f3-9606-eaf29eed6735");

            return rootFolder;
        }
    }
}