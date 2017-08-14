using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Extensions.Configuration.Abstractions.Interfaces;
using Xamarin.Extensions.Logging.Abstractions.Entities;
using Xamarin.Extensions.Logging.Abstractions.Interfaces;

namespace Xamarin.Extensions.Logging.Abstractions.Services
{
    public abstract class LoggerFactory : ILoggerFactory
    {     
        private const string k_SwitchDefaultName = "Default";
        private readonly IConfiguration r_Configuration;

        protected LoggerFactory(IConfiguration i_Configuration)
        {
            r_Configuration = i_Configuration;      
        }

        public abstract ILogger CreateLogger(string i_CategoryName);

        protected Func<string, eLogLevel, bool> GetFilter(string i_Name)
        {
            Func<string, eLogLevel, bool> filter = null;

            foreach (string prefix in getKeyPrefixes(i_Name))
            {
                eLogLevel level;
                if (tryGetSwitch(prefix, out level))
                {
                    filter = (i_LogName, i_LogLevel) => i_LogLevel >= level;
                    break;
                }
            }


            return filter;
        }

        private bool tryGetSwitch(string i_Name, out eLogLevel i_Level)
        {
            bool foundSwitch = false;

            IConfigurationSection switches = r_Configuration.GetSection("LogLevel");
            if (switches == null)
            {
                i_Level = eLogLevel.None;
                foundSwitch = false;
            }
            else
            {
                string value = switches[i_Name];
                if (string.IsNullOrEmpty(value))
                {
                    i_Level = eLogLevel.None;
                    foundSwitch = false;
                }
                else if (Enum.TryParse<eLogLevel>(value, out i_Level))
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

        private IEnumerable<string> getKeyPrefixes(string i_Name)
        {
            while (!string.IsNullOrEmpty(i_Name))
            {
                yield return i_Name;
                int lastIndexOfDot = i_Name.LastIndexOf('.');
                if (lastIndexOfDot == -1)
                {
                    yield return k_SwitchDefaultName;
                    break;
                }

                i_Name = i_Name.Substring(0, lastIndexOfDot);
            }
        }
    }
}
