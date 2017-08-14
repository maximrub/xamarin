using System.Collections.Generic;

namespace Xamarin.Extensions.Configuration.Abstractions.Interfaces
{
    /// <summary>
    /// Provides configuration key/values for an application.
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Tries to get a configuration value for the specified key.
        /// </summary>
        /// <param name="i_Key">The key.</param>
        /// <param name="i_Value">The value.</param>
        /// <returns><c>True</c> if a value for the specified key was found, otherwise <c>false</c>.</returns>
        bool TryGet(string i_Key, out string i_Value);

        /// <summary>
        /// Sets a configuration value for the specified key.
        /// </summary>
        /// <param name="i_Key">The key.</param>
        /// <param name="i_Value">The value.</param>
        void Set(string i_Key, string i_Value);

        /// <summary>
        /// Loads configuration values from the source represented by this <see cref="IConfigurationProvider"/>.
        /// </summary>
        void Load();

        /// <summary>
        /// Returns the immediate descendant configuration keys for a given parent path based on this
        /// <see cref="IConfigurationProvider"/>'s data and the set of keys returned by all the preceding
        /// <see cref="IConfigurationProvider"/>s.
        /// </summary>
        /// <param name="i_EarlierKeys">The child keys returned by the preceding providers for the same parent path.</param>
        /// <param name="i_ParentPath">The parent path.</param>
        /// <returns>The child keys.</returns>
        IEnumerable<string> GetChildKeys(IEnumerable<string> i_EarlierKeys, string i_ParentPath);
    }
}
