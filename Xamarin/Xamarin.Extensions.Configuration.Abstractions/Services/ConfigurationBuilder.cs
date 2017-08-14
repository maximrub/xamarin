using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Extensions.Configuration.Abstractions.Interfaces;

namespace Xamarin.Extensions.Configuration.Abstractions.Services
{
    /// <summary>
    /// Used to build key/value based configuration settings for use in an application.
    /// </summary>
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        /// <summary>
        /// Returns the sources used to obtain configuration values.
        /// </summary>
        public IList<IConfigurationSource> Sources { get; } = new List<IConfigurationSource>();

        /// <summary>
        /// Gets a key/value collection that can be used to share data between the <see cref="IConfigurationBuilder"/>
        /// and the registered <see cref="IConfigurationProvider"/>s.
        /// </summary>
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Adds a new configuration source.
        /// </summary>
        /// <param name="i_Source">The configuration source to add.</param>
        /// <returns>The same <see cref="IConfigurationBuilder"/>.</returns>
        public IConfigurationBuilder Add(IConfigurationSource i_Source)
        {
            if (i_Source == null)
            {
                throw new ArgumentNullException(nameof(i_Source));
            }

            Sources.Add(i_Source);

            return this;
        }

        /// <summary>
        /// Builds an <see cref="IConfiguration"/> with keys and values from the set of providers registered in
        /// <see cref="Sources"/>.
        /// </summary>
        /// <returns>An <see cref="IConfigurationRoot"/> with keys and values from the registered providers.</returns>
        public IConfigurationRoot Build()
        {
            List<IConfigurationProvider> providers = new List<IConfigurationProvider>();

            foreach (var source in Sources)
            {
                var provider = source.Build(this);
                providers.Add(provider);
            }

            return new ConfigurationRoot(providers);
        }
    }
}
