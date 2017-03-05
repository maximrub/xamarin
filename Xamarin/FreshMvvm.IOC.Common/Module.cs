namespace FreshMvvm.IOC.Common
{
    public abstract class Module
    {
        /// <summary>
        /// Override to add registrations to the IOC provider.
        /// </summary>
        /// <param name="i_Provider">The FreshIOC provider through which components can be
        /// registered.</param>
        public virtual void Load(IFreshIOC i_Provider)
        {
        }
    }
}
