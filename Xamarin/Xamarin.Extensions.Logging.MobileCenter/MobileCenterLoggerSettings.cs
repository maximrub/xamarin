using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Xamarin.Extensions.Logging.MobileCenter
{
    public class MobileCenterLoggerSettings : IMobileCenterLoggerSettings
    {
        public MobileCenterLoggerSettings()
        {
            Switches = new Dictionary<string, LogLevel>();
        }

        public IDictionary<string, LogLevel> Switches { get; set; }

        public IChangeToken ChangeToken { get; set; }

        public IMobileCenterLoggerSettings Reload()
        {
            return this;
        }

        public bool TryGetSwitch(string i_Name, out LogLevel i_Level)
        {
            return Switches.TryGetValue(i_Name, out i_Level);
        }
    }
}
