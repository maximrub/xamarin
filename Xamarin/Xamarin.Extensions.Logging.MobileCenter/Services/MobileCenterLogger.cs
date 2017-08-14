using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Extensions.Logging.Abstractions.Entities;
using Xamarin.Extensions.Logging.Abstractions.Interfaces;

namespace Xamarin.Extensions.Logging.MobileCenter.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MobileCenterLogger : ILogger
    {
        private const string k_TimeProperty = "Time";
        private const string k_EventIdProperty = "EventId";
        private const string k_LevelProperty = "Level";
        private const string k_MessageProperty = "Message";
        private const string k_ExceptionProperty = "Exception";
        private const int k_EventNameMaxLength = 256;
        private const int k_PropertyMaxLength = 64;
        private readonly string r_Name;
        private Func<string, eLogLevel, bool> m_Filter;

        public MobileCenterLogger(string i_Name, Func<string, eLogLevel, bool> i_Filter = null)
        {
            r_Name = i_Name;
            Filter = i_Filter ?? ((i_Category, i_LogLevel) => true);
        }

        /// <exception cref="ArgumentNullException" accessor="set"><paramref name="value"/> is <see langword="null"/></exception>
        public Func<string, eLogLevel, bool> Filter
        {
            get
            {
                return m_Filter;
            }

            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                m_Filter = value;
            }
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        /// <summary>Writes a log entry.</summary>
        /// <param name="i_LogLevel">Entry will be written on this level.</param>
        /// <param name="i_State">The entry to be written. Can be also an object.</param>
        /// <param name="i_Exception">The exception related to this entry.</param>
        /// <param name="i_Formatter">Function to create a <c>string</c> message of the <paramref name="i_State" /> and <paramref name="i_Exception" />.</param>
        /// <exception cref="ArgumentNullException"><paramref name="i_Formatter"/> is <see langword="null"/></exception>
        public void Log<TState>(
            eLogLevel i_LogLevel,
            TState i_State,
            Exception i_Exception,
            Func<TState, Exception, string> i_Formatter)
        {
            if(IsEnabled(i_LogLevel))
            {
                if(i_Formatter == null)
                {
                    throw new ArgumentNullException(nameof(i_Formatter));
                }

                string message = i_Formatter(i_State, i_Exception);
                if(!string.IsNullOrEmpty(message) || i_Exception != null)
                {
                    writeMessage(i_LogLevel, r_Name, message, i_Exception);
                }
            }
        }

        /// <summary>
        /// Checks if the given <paramref name="i_LogLevel" /> is enabled.
        /// </summary>
        /// <param name="i_LogLevel">level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        public bool IsEnabled(eLogLevel i_LogLevel)
        {
            return Filter.Invoke(r_Name, i_LogLevel);
        }

        private void writeMessage(eLogLevel i_LogLevel, string i_LogName, string i_Message, Exception i_Exception)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            string eventName = i_LogName.Truncate(k_EventNameMaxLength);

            properties.Add(k_TimeProperty, DateTime.UtcNow.ToString("s") + "Z");
            properties.Add(k_LevelProperty, i_LogLevel.ToString().ToUpper());
            if (!string.IsNullOrEmpty(i_Message))
            {
                properties.Add(k_MessageProperty, i_Message.Truncate(k_PropertyMaxLength));
            }

            if (i_Exception != null)
            {
                properties.Add(k_ExceptionProperty, i_Exception.Message.Truncate(k_PropertyMaxLength));
            }
            
            Analytics.TrackEvent(eventName, properties);
        }
    }
}