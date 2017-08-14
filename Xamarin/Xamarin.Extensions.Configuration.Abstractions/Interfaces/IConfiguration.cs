using System.Collections.Generic;

namespace Xamarin.Extensions.Configuration.Abstractions.Interfaces
{
    /// <summary>
    /// Represents a set of key/value application configuration properties.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets or sets a configuration value.
        /// </summary>
        /// <param name="i_Key">The configuration key.</param>
        /// <returns>The configuration value.</returns>
        string this[string i_Key] { get; set; }

        /// <summary>
        /// Gets a configuration sub-section with the specified key.
        /// </summary>
        /// <param name="i_Key">The key of the configuration section.</param>
        /// <returns>The <see cref="IConfigurationSection"/>.</returns>
        /// <remarks>
        ///     This method will never return <c>null</c>. If no matching sub-section is found with the specified key,
        ///     an empty <see cref="IConfigurationSection"/> will be returned.
        /// </remarks>
        IConfigurationSection GetSection(string i_Key);

        /// <summary>
        /// Gets the immediate descendant configuration sub-sections.
        /// </summary>
        /// <returns>The configuration sub-sections.</returns>
        IEnumerable<IConfigurationSection> GetChildren();
    }
}
