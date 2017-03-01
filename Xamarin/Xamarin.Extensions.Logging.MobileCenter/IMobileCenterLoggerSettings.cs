using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Xamarin.Extensions.Logging.MobileCenter
{
    public interface IMobileCenterLoggerSettings
    {
        IChangeToken ChangeToken { get; }

        bool TryGetSwitch(string i_Name, out LogLevel i_Level);

        IMobileCenterLoggerSettings Reload();
    }
}