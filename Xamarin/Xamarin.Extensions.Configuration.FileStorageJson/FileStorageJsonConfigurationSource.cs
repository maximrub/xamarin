using Microsoft.Extensions.Configuration;
using Xamarin.FileStorage.Abstractions;

namespace Xamarin.Extensions.Configuration.FileStorageJson
{
    public class FileStorageJsonConfigurationSource : IConfigurationSource
    {
        public IFileStorage FileStorage { get; set; }

        public string FileName { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder i_Builder)
        {
            return new FileStorageJsonConfigurationProvider(FileStorage, FileName);
        }
    }
}
