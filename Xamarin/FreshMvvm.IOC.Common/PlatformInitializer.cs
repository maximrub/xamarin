namespace FreshMvvm.IOC.Common
{
    public abstract class PlatformInitializer
    {
        protected PlatformInitializer()
        {
        }

        protected IFreshIOC Container
        {
            get
            {
                return FreshIOC.Container;
            }
        }

        internal void Init()
        {
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