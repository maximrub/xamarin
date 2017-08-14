namespace Xamarin.Extensions.Configuration.Abstractions.Interfaces
{
    /// <summary>
    /// Represents a source of configuration key/values for an application.
    /// </summary>
    public interface IConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="IConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="i_Builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>An <see cref="IConfigurationProvider"/></returns>
        IConfigurationProvider Build(IConfigurationBuilder i_Builder);
    }
}
