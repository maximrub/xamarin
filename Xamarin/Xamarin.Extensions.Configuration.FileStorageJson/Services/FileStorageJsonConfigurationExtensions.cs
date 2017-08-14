using System;
using Xamarin.Extensions.Configuration.Abstractions.Interfaces;
using Xamarin.FileStorage.Abstractions;

namespace Xamarin.Extensions.Configuration.FileStorageJson.Services
{
    public static class FileStorageJsonConfigurationExtensions
    {
        /// <summary>
        /// Adds a JSON configuration source to <paramref name="i_Builder"/>.
        /// </summary>
        /// <param name="i_Builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="i_FileStorage">The <see cref="IFileStorage"/> to use to access the file.</param>
        /// <param name="i_FileName">The file name.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="i_FileStorage"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException"><paramref name="i_FileName"/> is <see langword="null"/> or empty.</exception>
        public static IConfigurationBuilder AddFileStorageJsonFile(this IConfigurationBuilder i_Builder, IFileStorage i_FileStorage, string i_FileName)
        {
            if (i_FileStorage == null)
            {
                throw new ArgumentNullException(nameof(i_FileStorage));
            }

            if (string.IsNullOrEmpty(i_FileName))
            {
                throw new ArgumentException(nameof(i_FileName));
            }

            FileStorageJsonConfigurationSource source = new FileStorageJsonConfigurationSource
            {
                FileStorage = i_FileStorage,
                FileName = i_FileName
            };
            i_Builder.Add(source);

            return i_Builder;
        }
    }
}
