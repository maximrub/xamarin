using Xamarin.Extensions.Configuration.Abstractions.Interfaces;
using Xamarin.Extensions.Logging.Abstractions.Interfaces;
using Xamarin.Extensions.Logging.Abstractions.Services;

namespace Xamarin.Extensions.Logging.MobileCenter.Services
{
    public class MobileCenterLoggerFactory : LoggerFactory
    {
        public MobileCenterLoggerFactory(IConfiguration i_Configuration)
            : base(i_Configuration)
        {
        }

        public override ILogger CreateLogger(string i_CategoryName)
        {
            return new MobileCenterLogger(i_CategoryName, GetFilter(i_CategoryName));
        }
    }
}
