using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Extensions.Logging.Abstractions.Interfaces;

namespace Xamarin.Extensions.Logging.Abstractions.Services
{
    /// <summary>
    /// ILoggerFactory extension methods for common scenarios.
    /// </summary>
    public static class LoggerFactoryExtensions
    {
        /// <summary>
        /// Creates a new ILogger instance using the full name of the given type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="i_Factory">The factory.</param>
        public static ILogger<T> CreateLogger<T>(this ILoggerFactory i_Factory)
        {
            if (i_Factory == null)
            {
                throw new ArgumentNullException(nameof(i_Factory));
            }

            return new Logger<T>(i_Factory);
        }

        /// <summary>
        /// Creates a new ILogger instance using the full name of the given type.
        /// </summary>
        /// <param name="i_Factory">The factory.</param>
        /// <param name="i_Type">The type.</param>
        public static ILogger CreateLogger(this ILoggerFactory i_Factory, Type i_Type)
        {
            if (i_Factory == null)
            {
                throw new ArgumentNullException(nameof(i_Factory));
            }

            if (i_Type == null)
            {
                throw new ArgumentNullException(nameof(i_Type));
            }

            return i_Factory.CreateLogger(TypeNameHelper.GetTypeDisplayName(i_Type));
        }
    }
}
