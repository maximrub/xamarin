using LightInject;
using Microsoft.Extensions.Configuration;
using Xamarin.Extensions.Configuration.FileStorageJson;
using Xamarin.FileStorage.Abstractions;

namespace Friends.IOC
{
    public class ConfigurationCompositionRoot : ICompositionRoot
    {
        private const string k_SettingsFileName = "appsettings.json";

        public void Compose(IServiceRegistry i_ServiceRegistry)
        {
            i_ServiceRegistry.Register<IConfigurationRoot>(
                (i_Factory) =>
                    new ConfigurationBuilder().AddFileStorageJsonFile(
                        i_Factory.GetInstance<IFileStorage>(),
                        k_SettingsFileName).Build(),
                new PerContainerLifetime());
        }
    }
}