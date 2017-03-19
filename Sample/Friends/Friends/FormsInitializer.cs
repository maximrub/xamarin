using System.Reflection;
using Exrin.Abstraction;
using Exrin.IOC.LightInjectServiceProvider;
using Friends.Domain.Peoples.Interfaces;
using Friends.Domain.Validation.Interfaces;
using Friends.Domain.Validation.Services;
using Friends.Infrastructure.Peoples.Repository;
using LightInject;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xamarin.Extensions.Logging.MobileCenter;
using Bootstrapper = Exrin.Framework.Bootstrapper;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Friends
{
    public class FormsInitializer : AppInitializer
    {
        private string k_MobileCenterAndroidAppSecret = "Android App Secret";
        private string k_MobileCenterAppleAppSecret = "iOS App Secret";

        protected override void Register(ServiceContainer i_Container)
        {
            base.Register(i_Container);
            i_Container.Register<IPersonsRepository, MemoryPersonsRepository>(new PerContainerLifetime());
            i_Container.Register<IValidator, Validator>();
        }

        protected override void ConfigureServices(IServiceCollection i_Services)
        {
            base.ConfigureServices(i_Services);
            i_Services.AddLogging();
        }

        protected override void RegisterFrameworkAssemblies(Bootstrapper i_Bootstrapper)
        {
            base.RegisterFrameworkAssemblies(i_Bootstrapper);
            i_Bootstrapper.RegisterAssembly(AssemblyAction.Bootstrapper, this.GetType().GetTypeInfo().Assembly.GetName());
        }

        protected override void OnInitialized(ServiceContainer i_Container)
        {
            base.OnInitialized(i_Container);
            MobileCenter.Start(
                $"android={k_MobileCenterAndroidAppSecret};ios={k_MobileCenterAppleAppSecret}",
                typeof(Analytics),
                typeof(Crashes));
            ILoggerFactory loggerFactory = i_Container.GetInstance<ILoggerFactory>();
            configure(loggerFactory);
        }

        /// <summary>
        /// Use this method to configure the services
        /// </summary>
        /// <param name="i_LoggerFactory"></param>
        private void configure(ILoggerFactory i_LoggerFactory)
        {
            i_LoggerFactory.AddMobileCenter(LogLevel.Debug);
        }
    }
}