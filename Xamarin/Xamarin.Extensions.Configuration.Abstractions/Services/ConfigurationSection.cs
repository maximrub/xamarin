using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Extensions.Configuration.Abstractions.Interfaces;

namespace Xamarin.Extensions.Configuration.Abstractions.Services
{
    /// <summary>
    /// Represents a section of application configuration values.
    /// </summary>
    public class ConfigurationSection : IConfigurationSection
    {
        private readonly ConfigurationRoot r_Root;
        private readonly string r_Path;
        private string m_Key;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="i_Root">The configuration root.</param>
        /// <param name="i_Path">The path to this section.</param>
        public ConfigurationSection(ConfigurationRoot i_Root, string i_Path)
        {
            if (i_Root == null)
            {
                throw new ArgumentNullException(nameof(i_Root));
            }

            if (i_Path == null)
            {
                throw new ArgumentNullException(nameof(i_Path));
            }

            r_Root = i_Root;
            r_Path = i_Path;
        }

        /// <summary>
        /// Gets the full path to this section from the <see cref="IConfigurationRoot"/>.
        /// </summary>
        public string Path => r_Path;

        /// <summary>
        /// Gets the key this section occupies in its parent.
        /// </summary>
        public string Key
        {
            get
            {
                if(m_Key == null)
                {
                    // Key is calculated lazily as last portion of Path
                    m_Key = ConfigurationPath.GetSectionKey(r_Path);
                }

                return m_Key;
            }
        }

        /// <summary>
        /// Gets or sets the section value.
        /// </summary>
        public string Value
        {
            get
            {
                return r_Root[Path];
            }

            set
            {
                r_Root[Path] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value corresponding to a configuration key.
        /// </summary>
        /// <param name="i_Key">The configuration key.</param>
        /// <returns>The configuration value.</returns>
        public string this[string i_Key]
        {
            get
            {
                return r_Root[ConfigurationPath.Combine(Path, i_Key)];
            }

            set
            {
                r_Root[ConfigurationPath.Combine(Path, i_Key)] = value;
            }
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
        public IConfigurationSection GetSection(string i_Key) => r_Root.GetSection(ConfigurationPath.Combine(Path, i_Key));

        /// <summary>
        /// Gets the immediate descendant configuration sub-sections.
        /// </summary>
        /// <returns>The configuration sub-sections.</returns>
        public IEnumerable<IConfigurationSection> GetChildren() => r_Root.GetChildrenImplementation(Path);
    }
}
