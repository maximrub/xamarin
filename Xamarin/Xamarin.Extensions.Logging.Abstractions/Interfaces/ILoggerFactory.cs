using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Extensions.Logging.Abstractions.Interfaces
{
    /// <summary>
    /// Represents a type used to configure the logging system and create instances of <see cref="ILogger"/> 
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Creates a new <see cref="ILogger"/> instance.
        /// </summary>
        /// <param name="i_CategoryName">The category name for messages produced by the logger.</param>
        /// <returns>The <see cref="ILogger"/>.</returns>
        ILogger CreateLogger(string i_CategoryName);
    }
}
