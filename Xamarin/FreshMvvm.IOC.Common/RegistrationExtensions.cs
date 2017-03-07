using System;

namespace FreshMvvm.IOC.Common
{
    public static class RegistrationExtensions
    {
        /// <summary>
        /// Add a module to the container.
        /// </summary>
        /// <param name="i_Provider">The FreshIOC provider to register the module with.</param>
        /// <param name="i_RegisterType">Type to register</param>
        /// <param name="i_RegisterImplementation">Type to instantiate that implements RegisterType</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="i_Provider"/> is <see langword="null"/>.
        /// </exception>
        public static IRegisterOptions Register(this IFreshIOC i_Provider, Type i_RegisterType, Type i_RegisterImplementation)
        {
            if (i_Provider == null)
            {
                throw new ArgumentNullException(nameof(i_Provider));
            }

            return FreshTinyIOCBuiltIn.Current.Register(i_RegisterType, i_RegisterImplementation);
        }
    }
}
