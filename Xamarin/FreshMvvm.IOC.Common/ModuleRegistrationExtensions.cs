using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FreshMvvm.IOC.Common
{
    public static class ModuleRegistrationExtensions
    {
        /// <summary>
        /// Add a module to the container.
        /// </summary>
        /// <param name="i_Provider">The FreshIOC provider to register the module with.</param>
        /// <typeparam name="TModule">The module to add.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="i_Provider"/> is <see langword="null"/>.
        /// </exception>
        public static void RegisterModule<TModule>(this IFreshIOC i_Provider) where TModule : Module, new()
        {
            if(i_Provider == null)
            {
                throw new ArgumentNullException(nameof(i_Provider));
            }

            TModule module = new TModule();
            module.Load(i_Provider);
        }

        /// <summary>
        /// Registers modules found in an assembly.
        /// </summary>
        /// <param name="i_Provider">The FreshIOC provider to register the module with.</param>
        /// <param name="i_Assemblies">The assemblies from which to register modules.</param>
        /// <exception cref="ArgumentNullException">
        // Thrown if <paramref name="i_Provider"/> is <see langword="null"/>.
        /// </exception>
        public static void RegisterAssemblyModules(this IFreshIOC i_Provider, params Assembly[] i_Assemblies)
        {
            TypeInfo moduleTypeInfo = typeof(Module).GetTypeInfo();

            if (i_Provider == null)
            {
                throw new ArgumentNullException(nameof(i_Provider));
            }

            foreach(Assembly assembly in i_Assemblies)
            {
                IEnumerable<Type> modules =
                    assembly.ExportedTypes
                        .Where(
                            i_Type =>
                                moduleTypeInfo.IsAssignableFrom(i_Type.GetTypeInfo()));
                foreach(Type moduleType in modules)
                {
                    Module module = Activator.CreateInstance(moduleType) as Module;
                    if(module != null)
                    {
                        module.Load(i_Provider);
                    }
                }
            }
        }
    }
}