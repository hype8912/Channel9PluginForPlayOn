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
    using System.Collections.Specialized;
    using Properties;

    #endregion

    /// <summary>
    /// Class for implementing PlayOn settings interface.
    /// </summary>
    public class PluginSettings : MediaMallTechnologies.Plugin.IPlayOnProviderSettings
    {
        #region Properties

        /// <summary>
        /// Gets an Image icon for this content provider.
        /// </summary>
        /// <value></value>
        /// <remarks>The image must be minimum 48 x 48 pixels. PNG format images are
        /// preferred.</remarks>
        /// <returns>The Image for this plugin, to be displayed in PlayOn Settings.
        /// </returns>
        public System.Drawing.Image Image
        {
            get { return Resources.Channel9; }
        }

        /// <summary>
        /// Gets a value indicating whether this plugin has custom configurable
        /// options.
        /// </summary>
        /// <value></value>
        /// <remarks>A plugin is optionally able to have arbitrary custom
        /// options in a separate window. If this property returns <b>
        /// <c>true</c></b> then an "Options" button will be visible and enabled
        /// in PlayOn Settings for this plugin. When the user presses this
        /// button, the
        /// <see cref="M:MediaMallTechnologies.Plugin.IPlayOnProviderSettings.ConfigureOptions(System.Collections.Specialized.NameValueCollection,System.EventHandler)"/>
        /// method will be invoked.</remarks>
        /// <seealso cref="M:MediaMallTechnologies.Plugin.IPlayOnProviderSettings.ConfigureOptions(System.Collections.Specialized.NameValueCollection,System.EventHandler)"/>
        /// <returns><b><c>True</c></b> if this plugin has custom configurable
        /// options, <b><c>false</c></b> otherwise.
        /// </returns>
        public bool HasOptions
        {
            get { return false; }
        }

        /// <summary>
        /// Gets an HTTP link to the provider's homepage.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// This property should return an HTTP link <i>without</i> the prepended
        /// "http://".
        /// </remarks>
        /// <returns>A string value URL link for this plugin provider.</returns>
        public string Link 
        {
            get { return Resources.Link; }
        }

        /// <summary>
        /// Gets the display name that will show in the plugin tab of PlayOn Settings.
        /// </summary>
        /// <value></value>
        /// <returns>A string value for the name of this plugin provider.</returns>
        public string Name 
        {
            get { return Resources.Name; }
        }

        /// <summary>
        /// Gets a text description that will show in the plugin tab of PlayOn Settings.
        /// </summary>
        /// <value></value>
        /// <remarks>The description value should be a maximum of approximately 20 words.
        /// </remarks>
        /// <returns>A string value description for this plugin.</returns>
        public string Description 
        {
            get { return Resources.Description; }
        }

        /// <summary>
        /// Gets a value indicating whether a login control should be displayed.
        /// </summary>
        /// <value></value>
        /// <remarks>A plugin is optionally able to include account login
        /// information. If this property returns <b><c>true</c></b> then a
        /// login user control with test button will be displayed automatically
        /// on the configuration pane for this plugin in PlayOn Settings. 
        /// </remarks>
        /// <seealso cref="M:MediaMallTechnologies.Plugin.IPlayOnProviderSettings.TestLogin(System.String,System.String)"/>
        /// <returns><b><c>True</c></b> if this plugin should include an account
        /// login user control, <b><c>false</c></b> otherwise.
        /// </returns>
        public bool RequiresLogin 
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a unique ID used for caching and identifying the settings for this
        /// plugin.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// The settings ID should be a GUID, to avoid name clashes with other plugins.
        /// This ID is only used for caching optional settings for the plugin, and is never
        /// displayed to the user.
        /// </remarks>
        /// <returns>The string ID for the settings for this plugin.</returns>
        public string ID
        {
            get { return Resources.ID; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks to see if an update is available for the given plugin
        /// (presumably online).
        /// </summary>
        /// <returns>
        /// A string value of a URL to browse to for download instructions;
        /// otherwise <b>
        /// <c>null</c>.</b>
        /// </returns>
        /// <remarks>This function should check (presumably somewhere online) to
        /// see if a newer version of the plugin is available for download. If
        /// so, it should return a URL to navigate to in the user's browser for
        /// upgrade instructions, or <b><c>null</c></b> if no update is
        /// available or required. _Note that this method will be called every
        /// time PlayOn Settings is opened. It is the responsibility of this
        /// plugin to moderate any web requests that may otherwise overwhelm a
        /// web server with version check requests.
        /// </remarks>
        public string CheckForUpdate() 
        {
            return null;
        }

        /// <summary>
        /// Returns whether the indicated login parameters work successfully.
        /// </summary>
        /// <param name="username">The username or email address to login with.
        /// </param>
        /// <param name="password">The password to login with.</param>
        /// <returns>
        ///     <b><c>True</c></b> if the login test works, <b><c>false</c></b>
        ///     otherwise.
        /// </returns>
        /// <remarks>
        /// Is never invoked unless 
        /// <see cref="P:MediaMallTechnologies.Plugin.IPlayOnProviderSettings.RequiresLogin"/>
        ///  is also <c>true</c>. If successful, the values will be encrypted
        ///  and stored locally on the PC, and returned via the
        /// <see cref="P:MediaMallTechnologies.Plugin.IPlayOnHost.Properties"/>
        /// field as "username" and "password".
        /// </remarks>
        public bool TestLogin(string username, string password)
        {
            return true;
        }

        ////public System.Windows.Forms.Control ConfigureOptions(NameValueCollection options)
        ////{
        ////    throw new NotImplementedException();
        ////}

        /// <summary>
        /// Allows a plugin to show a custom configuration window to set custom string
        /// value options.
        /// </summary>
        /// <param name="options">A <c>NameValueCollection</c> of options as cached by
        /// PlayOn, and that can be modified through the custom Control. Any changes made
        /// by the user on the custom Control must be applied to this collection, so that
        /// PlayOn can cache the values.</param>
        /// <param name="changeHandler">An <c>EventHandler</c> that should be invoked
        /// whenever changes are made by the user on the custom Control. This 
        /// <c>EventHandler</c> is used to indicate to PlayOn Settings that changes have
        /// occurred, so that an Apply/Save button can be appropriately enabled.</param>
        /// <returns>
        /// A Control that has configurable options that the user can adjust.
        /// </returns>
        /// <remarks>Allows a plugin to show its own separate configuration window, and
        /// make changes to a <c>NameValueCollection"</c> of keys and values, which will be
        /// cached and can be queried later through 
        /// <see cref="P:MediaMallTechnologies.Plugin.IPlayOnHost.Properties"/>.
        /// <para>
        /// When 
        /// <see cref="P:MediaMallTechnologies.Plugin.IPlayOnProviderSettings.HasOptions"/>
        /// returns <b><c>true</c></b>, an "Options" button will be available for this plugin in
        /// PlayOn Settings. When pressed, the button will invoke this function to retrieve
        /// a custom Control that will be displayed in a modal dialog for the user. This
        /// custom Control can initialize based on the arbitrary values in the <i>options
        /// </i> parameter. Any changes that the user makes through the user interface must
        /// then be reflected directly in the <i>options</i> reference, such that they can
        /// then be cached by PlayOn.
        /// </para>
        ///     <para>
        /// It is critical that the supplied <see cref="System.EventHandler"/> be invoked
        /// whenever the user makes any relevant changes in the custom Control, so that an
        /// Apply/Save button can be appropriately enabled.
        /// </para>
        ///     <para>
        ///         <b>IMPORTANT:</b> The key names "username" and "password" are reserved and
        ///         must not be used or modified through this method. Instead, these login
        ///         parameters are used in the
        /// <see cref="M:MediaMallTechnologies.Plugin.IPlayOnProviderSettings.TestLogin(System.String,System.String)"/>
        /// method.
        /// </para>
        /// </remarks>
        public System.Windows.Forms.Control ConfigureOptions(NameValueCollection options, EventHandler changeHandler)
        {
            return null;
        }

        #endregion
    }
}