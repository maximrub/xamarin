using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Extensions.Logging;

namespace Xamarin.Extensions.Logging.MobileCenter
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
        private Func<string, LogLevel, bool> m_Filter;

        public MobileCenterLogger(string i_Name, Func<string, LogLevel, bool> i_Filter = null)
        {
            r_Name = i_Name;
            Filter = i_Filter ?? ((i_Category, i_LogLevel) => true);
        }

        /// <exception cref="ArgumentNullException" accessor="set"><paramref name="value"/> is <see langword="null"/></exception>
        public Func<string, LogLevel, bool> Filter
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
        /// <param name="i_EventId">Id of the event.</param>
        /// <param name="i_State">The entry to be written. Can be also an object.</param>
        /// <param name="i_Exception">The exception related to this entry.</param>
        /// <param name="i_Formatter">Function to create a <c>string</c> message of the <paramref name="i_State" /> and <paramref name="i_Exception" />.</param>
        /// <exception cref="ArgumentNullException"><paramref name="i_Formatter"/> is <see langword="null"/></exception>
        public void Log<TState>(
            LogLevel i_LogLevel,
            EventId i_EventId,
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
                    writeMessage(i_LogLevel, r_Name, i_EventId.Id, message, i_Exception);
                }
            }
        }

        /// <summary>
        /// Checks if the given <paramref name="i_LogLevel" /> is enabled.
        /// </summary>
        /// <param name="i_LogLevel">level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        public bool IsEnabled(LogLevel i_LogLevel)
        {
            return Filter.Invoke(r_Name, i_LogLevel);
        }

        /// <summary>Begins a logical operation scope.</summary>
        /// <param name="i_State">The identifier for the scope.</param>
        /// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="i_State"/> is <see langword="null"/></exception>
        public IDisposable BeginScope<TState>(TState i_State)
        {
            if(i_State == null)
            {
                throw new ArgumentNullException(nameof(i_State));
            }

            return MobileCenterLogScope.Push(r_Name, i_State);
        }

        private void writeMessage(LogLevel i_LogLevel, string i_LogName, int i_EventId, string i_Message, Exception i_Exception)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            string eventName = i_LogName.Truncate(k_EventNameMaxLength);

            properties.Add(k_TimeProperty, DateTime.UtcNow.ToString("s") + "Z");
            properties.Add(k_EventIdProperty, i_EventId.ToString());
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