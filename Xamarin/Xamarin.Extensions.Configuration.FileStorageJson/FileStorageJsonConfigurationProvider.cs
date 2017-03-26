using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xamarin.FileStorage.Abstractions;

namespace Xamarin.Extensions.Configuration.FileStorageJson
{
    public class FileStorageJsonConfigurationProvider : ConfigurationProvider
    {
        private readonly IFileStorage r_FileStorage;
        private readonly string r_FileName;

        public FileStorageJsonConfigurationProvider(IFileStorage i_FileStorage, string i_FileName)
        {
            r_FileStorage = i_FileStorage;
            r_FileName = i_FileName;
        }

        /// <exception cref="FormatException">The file is not a valid JSON.</exception>
        public override void Load()
        {
            JsonConfigurationFileParser parser = new JsonConfigurationFileParser();

            string content = r_FileStorage.Read(r_FileName);
            try
            {
                Data = parser.Parse(content);
            }
            catch(JsonReaderException jsonReaderException)
            {
                throw new FormatException($"Error at line {jsonReaderException.LineNumber}", jsonReaderException);
            }
        }
    }
}