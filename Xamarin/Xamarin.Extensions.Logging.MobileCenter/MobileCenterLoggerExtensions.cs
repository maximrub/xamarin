using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Xamarin.Extensions.Logging.MobileCenter
{
    public static class MobileCenterLoggerExtensions
    {
        /// <summary>
        /// Adds a MobileCenter logger that is enabled for <see cref="LogLevel"/>.Information or higher.
        /// </summary>
        /// <param name="i_Factory"></param>
        public static ILoggerFactory AddMobileCenter(this ILoggerFactory i_Factory)
        {
            i_Factory.AddMobileCenter((i_Name, i_LogLevel) => i_LogLevel >= LogLevel.Information);

            return i_Factory;
        }

        /// <summary>
        /// Adds a MobileCenter logger that is enabled for <see cref="LogLevel"/>s of i_MinLevel or higher.
        /// </summary>
        /// <param name="i_Factory"></param>
        /// <param name="i_MinLevel">The minimum <see cref="LogLevel"/> to be logged</param>
        public static ILoggerFactory AddMobileCenter(this ILoggerFactory i_Factory, LogLevel i_MinLevel)
        {
            i_Factory.AddMobileCenter((i_Name, i_LogLevel) => i_LogLevel >= i_MinLevel);

            return i_Factory;
        }

        /// <summary>
        /// Adds a MobileCenter logger that is enabled as defined by the i_Filter function.
        /// </summary>
        /// <param name="i_Factory"></param>
        /// <param name="i_Filter"></param>
        public static ILoggerFactory AddMobileCenter(
            this ILoggerFactory i_Factory,
            Func<string, LogLevel, bool> i_Filter)
        {
            i_Factory.AddProvider(new MobileCenterLoggerProvider(i_Filter));

            return i_Factory;
        }

        public static ILoggerFactory AddMobileCenter(
            this ILoggerFactory i_Factory,
            IMobileCenterLoggerSettings i_Settings)
        {
            i_Factory.AddProvider(new MobileCenterLoggerProvider(i_Settings));

            return i_Factory;
        }

        public static ILoggerFactory AddMobileCenter(this ILoggerFactory i_Factory, IConfiguration i_Configuration)
        {
            ConfigurationMobileCenterLoggerSettings settings =
                new ConfigurationMobileCenterLoggerSettings(i_Configuration);

            return i_Factory.AddMobileCenter(settings);
        }
    }
}