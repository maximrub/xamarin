using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Extensions.Configuration.Abstractions.Interfaces;

namespace Xamarin.Extensions.Configuration.Abstractions.Services
{
    /// <summary>
    /// The root node for a configuration.
    /// </summary>
    public class ConfigurationRoot : IConfigurationRoot
    {
        private readonly IList<IConfigurationProvider> r_Providers;

        /// <summary>
        /// Initializes a Configuration root with a list of providers.
        /// </summary>
        /// <param name="i_Providers">The <see cref="IConfigurationProvider"/>s for this configuration.</param>
        public ConfigurationRoot(IList<IConfigurationProvider> i_Providers)
        {
            if (i_Providers == null)
            {
                throw new ArgumentNullException(nameof(i_Providers));
            }

            r_Providers = i_Providers;
            foreach (var p in i_Providers)
            {
                p.Load();
            }
        }

        /// <summary>
        /// The <see cref="IConfigurationProvider"/>s for this configuration.
        /// </summary>
        public IEnumerable<IConfigurationProvider> Providers => r_Providers;

        /// <summary>
        /// Gets or sets the value corresponding to a configuration key.
        /// </summary>
        /// <param name="i_Key">The configuration key.</param>
        /// <returns>The configuration value.</returns>
        public string this[string i_Key]
        {
            get
            {
                foreach (var provider in r_Providers.Reverse())
                {
                    string value;

                    if (provider.TryGet(i_Key, out value))
                    {
                        return value;
                    }
                }

                return null;
            }

            set
            {
                if (!r_Providers.Any())
                {
                    throw new InvalidOperationException("No Sources provided");
                }

                foreach (var provider in r_Providers)
                {
                    provider.Set(i_Key, value);
                }
            }
        }

        /// <summary>
        /// Gets the immediate children sub-sections.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IConfigurationSection> GetChildren() => GetChildrenImplementation(null);

        internal IEnumerable<IConfigurationSection> GetChildrenImplementation(string i_Path)
        {
            return r_Providers.Aggregate(Enumerable.Empty<string>(), (i_Seed, i_Source) => i_Source.GetChildKeys(i_Seed, i_Path)).Distinct()
                .Select(i_Key => GetSection(i_Path == null ? i_Key : ConfigurationPath.Combine(i_Path, i_Key)));
        }

        /// <summary>
        /// Gets a configuration sub-section with the specified key.
        /// </summary>
        /// <param name="i_Key">The key of the configuration section.</param>
        /// <returns>The <see cref="IConfigurationSection"/>.</returns>
        /// <remarks>
        ///     This method will never return <c>null</c>. If no matching sub-section is found with the specified key,
        ///     an empty <see cref="IConfigurationSection"/> will be returned.
        /// </remarks>
        public IConfigurationSection GetSection(string i_Key)
        {
            return new ConfigurationSection(this, i_Key);
        }
    }
}
