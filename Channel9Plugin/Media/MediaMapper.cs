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

    using MediaMallTechnologies.Plugin;

    #endregion

    /// <summary>
    /// Class for sending media to the correct class.
    /// </summary>
    internal sealed class MediaMapper
    {
        /// <summary>
        /// Maps the specified media item.
        /// </summary>
        /// <param name="item">The media item.</param>
        /// <returns>The shared media file info.</returns>
        internal SharedMediaFileInfo Map(MediaItem item)
        {
            var extension = item.HighMediaUrl.Substring(item.HighMediaUrl.LastIndexOf('.'));
            switch (extension)
            {
                case @".wmv":
                case @".asx":
                case @".mp4":
                    return ToVideoResource(item);
                case @".wma":
                case @".mp3":
                    return ToAudioResource(item);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Maps the specified media folder.
        /// </summary>
        /// <param name="folder">The media folder.</param>
        /// <returns>The shared media folder info.</returns>
        internal SharedMediaFolderInfo Map(MediaFolder folder)
        {
            return new SharedMediaFolderInfo(
                                             folder.Id, 
                                             folder.OwnerId, 
                                             folder.Title, 
                                             folder.Items.Count + folder.Folders.Count, 
                                             folder.Thumbnail, 
                                             null);
        }

        /// <summary>
        /// Processes the audio item into a audio resource.
        /// </summary>
        /// <param name="audio">The audio media item object.</param>
        /// <returns>A new audio resource object.</returns>
        private static SharedMediaFileInfo ToAudioResource(MediaItem audio)
        {
            return new AudioResource(
                                     audio.Id,
                                     audio.OwnerId,
                                     audio.Title,
                                     audio.HighMediaUrl,
                                     audio.Description,
                                     audio.ThumbnailUrl,
                                     audio.PubDate,
                                     audio.HighMediaUrl,
                                     null,
                                     audio.Duration,
                                     0,
                                     null,
                                     null,
                                     null,
                                     0);
        }

        /// <summary>
        /// Processes the video item into a video resource.
        /// </summary>
        /// <param name="video">The video media item object.</param>
        /// <returns>A new video resource object.</returns>
        private static SharedMediaFileInfo ToVideoResource(MediaItem video)
        {
            return new VideoResource(
                                     video.Id,
                                     video.OwnerId,
                                     video.Title,
                                     video.HighMediaUrl,
                                     video.Description,
                                     video.ThumbnailUrl,
                                     video.PubDate,
                                     video.HighMediaUrl,
                                     null,
                                     video.Duration,
                                     0,
                                     null,
                                     null);
        }
    }
}