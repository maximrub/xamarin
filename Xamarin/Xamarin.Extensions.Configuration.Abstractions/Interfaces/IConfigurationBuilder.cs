using System.Collections.Generic;

namespace Xamarin.Extensions.Configuration.Abstractions.Interfaces
{
    /// <summary>
    /// Represents a type used to build application configuration.
    /// </summary>
    public interface IConfigurationBuilder
    {
        /// <summary>
        /// Gets a key/value collection that can be used to share data between the <see cref="IConfigurationBuilder"/>
        /// and the registered <see cref="IConfigurationSource"/>s.
        /// </summary>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// Gets the sources used to obtain configuration values
        /// </summary>
        IList<IConfigurationSource> Sources { get; }

        /// <summary>
        /// Adds a new configuration source.
        /// </summary>
        /// <param name="i_Source">The configuration source to add.</param>
        /// <returns>The same <see cref="IConfigurationBuilder"/>.</returns>
        IConfigurationBuilder Add(IConfigurationSource i_Source);

        /// <summary>
        /// Builds an <see cref="Sources"/> with keys and values from the set of sources registered in
        /// <see cref="IConfigurationRoot"/>.
        /// </summary>
        /// <returns>An <see cref="IConfiguration"/> with keys and values from the registered sources.</returns>
        IConfigurationRoot Build();
    }
}
