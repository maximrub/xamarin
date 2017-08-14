using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Extensions.Logging.Abstractions.Entities;
using Xamarin.Extensions.Logging.Abstractions.Interfaces;

namespace Xamarin.Extensions.Logging.Abstractions.Services
{
    /// <summary>
    /// Delegates to a new <see cref="ILogger{TCategoryName}"/> instance using the full name of the given type, created by the
    /// provided <see cref="ILoggerFactory"/>.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class Logger<T> : ILogger<T>
    {
        private readonly ILogger r_Logger;

        /// <summary>
        /// Creates a new <see cref="Logger{T}"/>.
        /// </summary>
        /// <param name="i_Factory">The factory.</param>
        public Logger(ILoggerFactory i_Factory)
        {
            if (i_Factory == null)
            {
                throw new ArgumentNullException(nameof(i_Factory));
            }

            r_Logger = i_Factory.CreateLogger(TypeNameHelper.GetTypeDisplayName(typeof(T)));
        }

        bool ILogger.IsEnabled(eLogLevel i_LogLevel)
        {
            return r_Logger.IsEnabled(i_LogLevel);
        }

        void ILogger.Log<TState>(eLogLevel i_LogLevel, TState i_State, Exception i_Exception, Func<TState, Exception, string> i_Formatter)
        {
            r_Logger.Log(i_LogLevel, i_State, i_Exception, i_Formatter);
        }
    }
}
