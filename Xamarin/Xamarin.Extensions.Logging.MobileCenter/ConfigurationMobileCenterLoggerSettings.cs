using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Xamarin.Extensions.Logging.MobileCenter
{
    public class ConfigurationMobileCenterLoggerSettings : IMobileCenterLoggerSettings
    {
        private readonly IConfiguration r_Configuration;

        public ConfigurationMobileCenterLoggerSettings(IConfiguration i_Configuration)
        {
            r_Configuration = i_Configuration;
            ChangeToken = i_Configuration.GetReloadToken();
        }

        public IChangeToken ChangeToken { get; private set; }

        /// <exception cref="InvalidOperationException">Configuration value is not supported.</exception>
        public bool TryGetSwitch(string i_Name, out LogLevel i_Level)
        {
            bool foundSwitch;

            IConfigurationSection switches = r_Configuration.GetSection("LogLevel");
            if(switches == null)
            {
                i_Level = LogLevel.None;
                foundSwitch = false;
            }
            else
            {
                string value = switches[i_Name];
                if(string.IsNullOrEmpty(value))
                {
                    i_Level = LogLevel.None;
                    foundSwitch = false;
                }
                else if(Enum.TryParse<LogLevel>(value, out i_Level))
                {
                    foundSwitch = true;
                }
                else
                {
                    string message = $"Configuration value '{value}' for category '{i_Name}' is not supported.";
                    throw new InvalidOperationException(message);
                }
            }

            return foundSwitch;
        }

        public IMobileCenterLoggerSettings Reload()
        {
            ChangeToken = null;

            return new ConfigurationMobileCenterLoggerSettings(r_Configuration);
        }
    }
}
