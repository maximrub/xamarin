namespace FreshMvvm.IOC.Common
{
    public abstract class AppInitializer
    {
        private readonly PlatformInitializer r_PlatformInitializer;

        protected AppInitializer(PlatformInitializer i_PlatformInitializer)
        {
            r_PlatformInitializer = i_PlatformInitializer;
        }

        protected IFreshIOC Container
        {
            get
            {
                return FreshIOC.Container;
            }
        }

        public void Init()
        {
            r_PlatformInitializer.Init();
            ConfigureServices();
            Register();
        }

        /// <summary>
        /// Configure Framework services
        /// </summary>
        protected virtual void ConfigureServices()
        {
        }

        /// <summary>
        /// Register Components using FreshIOC provider
        /// </summary>
        protected virtual void Register()
        {
        }
    }
}
