using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Extensions.Logging.Abstractions.Entities;
using Xamarin.Extensions.Logging.Abstractions.Interfaces;

namespace Xamarin.Extensions.Logging.Abstractions.Services
{
    /// <summary>
    /// ILogger extension methods for common scenarios.
    /// </summary>
    public static class LoggerExtensions
    {
        private static readonly Func<object, Exception, string> sr_MessageFormatter = messageFormatter;

        //------------------------------------------DEBUG------------------------------------------//

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Exception">The exception to log.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogDebug(this ILogger i_Logger, Exception i_Exception, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Debug, new FormattedLogValues(i_Message, i_Args), i_Exception, sr_MessageFormatter);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogDebug(this ILogger i_Logger, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Debug, new FormattedLogValues(i_Message, i_Args), null, sr_MessageFormatter);
        }

        //------------------------------------------TRACE------------------------------------------//

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Exception">The exception to log.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogTrace(this ILogger i_Logger, Exception i_Exception, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Trace, new FormattedLogValues(i_Message, i_Args), i_Exception, sr_MessageFormatter);
        }

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogTrace(this ILogger i_Logger, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Trace, new FormattedLogValues(i_Message, i_Args), null, sr_MessageFormatter);
        }

        //------------------------------------------INFORMATION------------------------------------------//

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Exception">The exception to log.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogInformation(this ILogger i_Logger, Exception i_Exception, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Information, new FormattedLogValues(i_Message, i_Args), i_Exception, sr_MessageFormatter);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogInformation(this ILogger i_Logger, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Information, new FormattedLogValues(i_Message, i_Args), null, sr_MessageFormatter);
        }

        //------------------------------------------WARNING------------------------------------------//

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Exception">The exception to log.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogWarning(this ILogger i_Logger, Exception i_Exception, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Warning, new FormattedLogValues(i_Message, i_Args), i_Exception, sr_MessageFormatter);
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogWarning(this ILogger i_Logger, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Warning, new FormattedLogValues(i_Message, i_Args), null, sr_MessageFormatter);
        }

        //------------------------------------------ERROR------------------------------------------//

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Exception">The exception to log.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogError(this ILogger i_Logger, Exception i_Exception, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Error, new FormattedLogValues(i_Message, i_Args), i_Exception, sr_MessageFormatter);
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogError(this ILogger i_Logger, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Error, new FormattedLogValues(i_Message, i_Args), null, sr_MessageFormatter);
        }
        
        //------------------------------------------CRITICAL------------------------------------------//

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Exception">The exception to log.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogCritical(this ILogger i_Logger, Exception i_Exception, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Critical, new FormattedLogValues(i_Message, i_Args), i_Exception, sr_MessageFormatter);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="i_Logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="i_Message">Format string of the log message.</param>
        /// <param name="i_Args">An object array that contains zero or more objects to format.</param>
        public static void LogCritical(this ILogger i_Logger, string i_Message, params object[] i_Args)
        {
            if (i_Logger == null)
            {
                throw new ArgumentNullException(nameof(i_Logger));
            }

            i_Logger.Log(eLogLevel.Critical, new FormattedLogValues(i_Message, i_Args), null, sr_MessageFormatter);
        }

        // ------------------------------------------HELPERS------------------------------------------ //
        private static string messageFormatter(object i_State, Exception i_Error)
        {
            return i_State.ToString();
        }
    }
}
