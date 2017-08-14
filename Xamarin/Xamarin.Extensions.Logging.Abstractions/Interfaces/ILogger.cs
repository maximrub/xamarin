using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Extensions.Logging.Abstractions.Entities;

namespace Xamarin.Extensions.Logging.Abstractions.Interfaces
{
    /// <summary>
    /// Represents a type used to perform logging.
    /// </summary>
    /// <remarks>Aggregates most logging patterns to a single method.</remarks>
    public interface ILogger
    {
        /// <summary>
        /// Writes a log entry.
        /// </summary>
        /// <param name="i_LogLevel">Entry will be written on this level.</param>
        /// <param name="i_State">The entry to be written. Can be also an object.</param>
        /// <param name="i_Exception">The exception related to this entry.</param>
        /// <param name="i_Formatter">Function to create a <c>string</c> message of the <paramref name="i_State"/> and <paramref name="i_Exception"/>.</param>
        void Log<TState>(eLogLevel i_LogLevel, TState i_State, Exception i_Exception, Func<TState, Exception, string> i_Formatter);

        /// <summary>
        /// Checks if the given <paramref name="i_LogLevel"/> is enabled.
        /// </summary>
        /// <param name="i_LogLevel">level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        bool IsEnabled(eLogLevel i_LogLevel);
    }
}
