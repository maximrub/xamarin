using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Xamarin.Extensions.Logging.MobileCenter
{
    public class MobileCenterLoggerProvider : ILoggerProvider, IDisposable
    {
        private readonly ConcurrentDictionary<string, MobileCenterLogger> r_Loggers = new ConcurrentDictionary<string, MobileCenterLogger>();
        private readonly Func<string, LogLevel, bool> r_Filter;
        private IMobileCenterLoggerSettings m_Settings;

        /// <exception cref="ArgumentNullException"><paramref name="i_Filter"/> is <see langword="null"/></exception>
        public MobileCenterLoggerProvider(Func<string, LogLevel, bool> i_Filter)
        {
            if (i_Filter == null)
            {
                throw new ArgumentNullException(nameof(i_Filter));
            }

            r_Filter = i_Filter;
            m_Settings = new MobileCenterLoggerSettings();
        }

        /// <exception cref="ArgumentNullException"><paramref name="i_Settings"/> is <see langword="null"/></exception>
        public MobileCenterLoggerProvider(IMobileCenterLoggerSettings i_Settings)
        {
            if (i_Settings == null)
            {
                throw new ArgumentNullException(nameof(i_Settings));
            }

            m_Settings = i_Settings;
            if (m_Settings.ChangeToken != null)
            {
                m_Settings.ChangeToken.RegisterChangeCallback(onConfigurationReload, null);
            }
        }

        public ILogger CreateLogger(string i_Name)
        {
            return r_Loggers.GetOrAdd(i_Name, createLogger);
        }

        public void Dispose()
        {
        }

        private void onConfigurationReload(object i_State)
        {
            try
            {
                // The settings object needs to change here, because the old one is probably holding on
                // to an old change token.
                m_Settings = m_Settings.Reload();
                foreach (MobileCenterLogger logger in r_Loggers.Values)
                {
                    logger.Filter = getFilter(logger.Name, m_Settings);
                }
            }
            finally
            {
                // The token will change each time it reloads, so we need to register again.
                if (m_Settings?.ChangeToken != null)
                {
                    m_Settings.ChangeToken.RegisterChangeCallback(onConfigurationReload, null);
                }
            }
        }

        private MobileCenterLogger createLogger(string i_Name)
        {
            return new MobileCenterLogger(i_Name, getFilter(i_Name, m_Settings));
        }

        private Func<string, LogLevel, bool> getFilter(string i_Name, IMobileCenterLoggerSettings i_Settings)
        {
            Func<string, LogLevel, bool> filter = null;

            if (r_Filter != null)
            {
                filter = r_Filter;
            }
            else if (i_Settings != null)
            {
                foreach (string prefix in GetKeyPrefixes(i_Name))
                {
                    LogLevel level;
                    if (i_Settings.TryGetSwitch(prefix, out level))
                    {
                        filter = (i_LogName, i_LogLevel) => i_LogLevel >= level;
                        break;
                    }
                }
            }
            else
            {
                filter = (i_LogName, i_LogLevel) => false;
            }

            return filter;
        }

        private IEnumerable<string> GetKeyPrefixes(string i_Name)
        {
            while (!string.IsNullOrEmpty(i_Name))
            {
                yield return i_Name;
                int lastIndexOfDot = i_Name.LastIndexOf('.');
                if (lastIndexOfDot == -1)
                {
                    yield return "Default";
                    break;
                }

                i_Name = i_Name.Substring(0, lastIndexOfDot);
            }
        }
    }
}
