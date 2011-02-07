//-----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="The Boeing Company">
//     Copyright (c) The Boeing Company. All rights reserved.
// </copyright>
// <author name="Joshua DeLong" date="7/18/2010 12:03:04 PM" />
//-----------------------------------------------------------------------

namespace Channel9Plugin
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns a value indicating whether the specified char object occurs
        /// within this string.
        /// </summary>
        /// <param name="text">The text data.</param>
        /// <param name="value">The char to seek.</param>
        /// <returns>
        ///     <c>True</c> if the value parameter occurs within this string, or
        ///     if value is the empty string (""); otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs an ordinal (case-sensitive and
        /// culture-insensitive) comparison.  The search begins at the first
        /// character position of this string and continues through  the last
        /// character position.
        /// </remarks>
        public static bool Contains(this string text, char value)
        {
            return text.IndexOf(value) >= 0;
        }

        #region Cleaners

        /// <summary>
        /// Strips the html tags using a char array.
        /// </summary>
        /// <param name="source">The string source.</param>
        /// <returns>A string without html tags.</returns>
        public static string StripHtmlTags(this string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var let in source)
            {
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

                if (inside)
                {
                    continue;
                }

                array[arrayIndex] = let;
                arrayIndex++;
            }

            var text = new string(array, 0, arrayIndex);
            return text.Replace(@"&quot;", @"""").Replace(@"&nbsp;", @" ");
        }

        #endregion
    }
}