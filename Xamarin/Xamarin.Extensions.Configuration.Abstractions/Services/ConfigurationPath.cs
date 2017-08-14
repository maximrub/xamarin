using System;
using System.Collections.Generic;

namespace Xamarin.Extensions.Configuration.Abstractions.Services
{
    /// <summary>
    /// Utility methods and constants for manipulating Configuration paths
    /// </summary>
    public static class ConfigurationPath
    {
        /// <summary>
        /// The delimiter ":" used to separate individual keys in a path.
        /// </summary>
        public static string KeyDelimiter { get; } = ":";

        /// <summary>
        /// Combines path segments into one path.
        /// </summary>
        /// <param name="i_PathSegments">The path segments to combine.</param>
        /// <returns>The combined path.</returns>
        public static string Combine(params string[] i_PathSegments)
        {
            if(i_PathSegments == null)
            {
                throw new ArgumentNullException(nameof(i_PathSegments));
            }

            return string.Join(KeyDelimiter, i_PathSegments);
        }

        /// <summary>
        /// Combines path segments into one path.
        /// </summary>
        /// <param name="i_PathSegments">The path segments to combine.</param>
        /// <returns>The combined path.</returns>
        public static string Combine(IEnumerable<string> i_PathSegments)
        {
            if(i_PathSegments == null)
            {
                throw new ArgumentNullException(nameof(i_PathSegments));
            }

            return string.Join(KeyDelimiter, i_PathSegments);
        }

        /// <summary>
        /// Extracts the last path segment from the path.
        /// </summary>
        /// <param name="i_Path">The path.</param>
        /// <returns>The last path segment of the path.</returns>
        public static string GetSectionKey(string i_Path)
        {
            if(string.IsNullOrEmpty(i_Path))
            {
                return i_Path;
            }

            int lastDelimiterIndex = i_Path.LastIndexOf(KeyDelimiter, StringComparison.OrdinalIgnoreCase);

            return lastDelimiterIndex == -1 ? i_Path : i_Path.Substring(lastDelimiterIndex + 1);
        }

        /// <summary>
        /// Extracts the path corresponding to the parent node for a given path.
        /// </summary>
        /// <param name="i_Path">The path.</param>
        /// <returns>The original path minus the last individual segment found in it. Null if the original path corresponds to a top level node.</returns>
        public static string GetParentPath(string i_Path)
        {
            if(string.IsNullOrEmpty(i_Path))
            {
                return null;
            }

            var lastDelimiterIndex = i_Path.LastIndexOf(KeyDelimiter, StringComparison.OrdinalIgnoreCase);

            return lastDelimiterIndex == -1 ? null : i_Path.Substring(0, lastDelimiterIndex);
        }
    }
}