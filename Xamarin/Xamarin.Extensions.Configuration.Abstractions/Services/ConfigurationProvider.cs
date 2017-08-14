using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Extensions.Configuration.Abstractions.Interfaces;

namespace Xamarin.Extensions.Configuration.Abstractions.Services
{
    /// <summary>
    /// Base helper class for implementing an <see cref="IConfigurationProvider"/>
    /// </summary>
    public abstract class ConfigurationProvider : IConfigurationProvider
    {
        /// <summary>
        /// Initializes a new <see cref="IConfigurationProvider"/>
        /// </summary>
        protected ConfigurationProvider()
        {
            Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// The configuration key value pairs for this provider.
        /// </summary>
        protected IDictionary<string, string> Data { get; set; }

        /// <summary>
        /// Attempts to find a value with the given key, returns true if one is found, false otherwise.
        /// </summary>
        /// <param name="i_Key">The key to lookup.</param>
        /// <param name="i_Value">The value found at key if one is found.</param>
        /// <returns>True if key has a value, false otherwise.</returns>
        public bool TryGet(string i_Key, out string i_Value)
        {
            return Data.TryGetValue(i_Key, out i_Value);
        }

        /// <summary>
        /// Sets a value for a given key.
        /// </summary>
        /// <param name="i_Key">The configuration key to set.</param>
        /// <param name="i_Value">The value to set.</param>
        public void Set(string i_Key, string i_Value)
        {
            Data[i_Key] = i_Value;
        }

        /// <summary>
        /// Loads (or reloads) the data for this provider.
        /// </summary>
        public virtual void Load()
        {
        }

        /// <summary>
        /// Returns the list of keys that this provider has.
        /// </summary>
        /// <param name="i_EarlierKeys">The earlier keys that other providers contain.</param>
        /// <param name="i_ParentPath">The path for the parent IConfiguration.</param>
        /// <returns>The list of keys for this provider.</returns>
        public virtual IEnumerable<string> GetChildKeys(
            IEnumerable<string> i_EarlierKeys,
            string i_ParentPath)
        {
            var prefix = i_ParentPath == null ? string.Empty : i_ParentPath + ConfigurationPath.KeyDelimiter;

            return Data
                .Where(i_Kv => i_Kv.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .Select(i_Kv => segment(i_Kv.Key, prefix.Length))
                .Concat(i_EarlierKeys)
                .OrderBy(i_Key => i_Key, ConfigurationKeyComparer.Instance);
        }

        private string segment(string i_Key, int i_PrefixLength)
        {
            var indexOf = i_Key.IndexOf(ConfigurationPath.KeyDelimiter, i_PrefixLength, StringComparison.OrdinalIgnoreCase);
            return indexOf < 0 ? i_Key.Substring(i_PrefixLength) : i_Key.Substring(i_PrefixLength, indexOf - i_PrefixLength);
        }
    }
}
