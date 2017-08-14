using System;
using System.Collections.Generic;

namespace Xamarin.Extensions.Configuration.Abstractions.Services
{
    /// <summary>
    /// IComparer implementation used to order configuration keys.
    /// </summary>
    public class ConfigurationKeyComparer : IComparer<string>
    {
        private static readonly string[] sr_KeyDelimiterArray = new[] { ConfigurationPath.KeyDelimiter };

        /// <summary>
        /// The default instance.
        /// </summary>
        public static ConfigurationKeyComparer Instance { get; } = new ConfigurationKeyComparer();

        /// <summary>
        /// Compares two strings.
        /// </summary>
        /// <param name="i_X">First string.</param>
        /// <param name="i_Y">Second string.</param>
        /// <returns></returns>
        public int Compare(string i_X, string i_Y)
        {
            string[] xParts = i_X?.Split(sr_KeyDelimiterArray, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
            string[] yParts = i_Y?.Split(sr_KeyDelimiterArray, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];

            // Compare each part until we get two parts that are not equal
            for (int i = 0; i < Math.Min(xParts.Length, yParts.Length); i++)
            {
                i_X = xParts[i];
                i_Y = yParts[i];
                int value1 = 0;
                int value2 = 0;
                bool xIsInt = i_X != null && int.TryParse(i_X, out value1);
                bool yIsInt = i_Y != null && int.TryParse(i_Y, out value2);

                int result = 0;

                if (!xIsInt && !yIsInt)
                {
                    // Both are strings
                    result = string.Compare(i_X, i_Y, StringComparison.OrdinalIgnoreCase);
                }
                else if (xIsInt && yIsInt)
                {
                    // Both are int 
                    result = value1 - value2;
                }
                else
                {
                    // Only one of them is int
                    result = xIsInt ? -1 : 1;
                }

                if (result != 0)
                {
                    // One of them is different
                    return result;
                }
            }

            // If we get here, the common parts are equal.
            // If they are of the same length, then they are totally identical
            return xParts.Length - yParts.Length;
        }
    }
}
